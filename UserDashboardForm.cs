using System;
using System.Windows.Forms;
using ClassLibrary;
using System.Linq;
using System.Drawing;

namespace ForexWise
{
    public partial class UserDashboardForm : Form
    {
        private readonly string _userId;
        private readonly Database _database;
        private readonly CurrencyApiService _currencyApiService;
        private readonly MainForm _mainForm;

        public UserDashboardForm(string userId, MainForm mainForm)
        {
            InitializeComponent();
            InitializeGridColumns();

            _userId = userId;
            _mainForm = mainForm;
            _database = new Database(@"Server=.\SQLEXPRESS;Database=ForexWise;Trusted_Connection=True;TrustServerCertificate=True");
            _currencyApiService = new CurrencyApiService("");

            // Event handler'ları bağla
            btnDeposit.Click += btnDeposit_Click;
            btnRefreshRates.Click += btnRefreshRates_Click;
            btnPayDebt.Click += btnPayDebt_Click;
            btnExchange.Click += btnExchange_Click;
            btnAddDebt.Click += btnAddDebt_Click;
            cboFromCurrency.SelectedIndexChanged += UpdateExchangeRate;
            cboToCurrency.SelectedIndexChanged += UpdateExchangeRate;

            LoadInitialData();
        }

        private async void LoadInitialData()
        {
            await LoadUserDebts();
            await LoadUserBalances();
            await LoadCurrentRates();
        }

        private async Task LoadUserDebts()
        {
            var debts = await _database.GetUserDebtsAsync(_userId);
            dgvDebts.Rows.Clear();
            foreach (var debt in debts)
            {
                var rowIndex = dgvDebts.Rows.Add(
                    debt.Id,
                    debt.LenderName,
                    debt.BorrowerName,
                    debt.CurrencyType,
                    debt.OriginalAmount,
                    debt.RemainingAmount,
                    debt.ExchangeRateAtBorrowing,
                    debt.BorrowingDate.ToShortDateString()
                );

                // Ödenmiş borçları kırmızı yap
                if (debt.IsPaid)
                {
                    dgvDebts.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Red;
                }
            }
        }

        private async Task LoadUserBalances()
        {
            var balances = await _database.GetUserBalancesAsync(_userId);
            dgvBalances.Rows.Clear();
            foreach (var balance in balances)
            {
                dgvBalances.Rows.Add(
                    balance.CurrencyType,
                    balance.Amount
                );
            }
        }

        private async void btnAddDebt_Click(object? sender, EventArgs e)
        {
            try
            {
                var rates = await _currencyApiService.GetExchangeRatesAsync();
                decimal exchangeRate = rates[cboCurrency.Text];

                await _database.AddDebtRecordAsync(
                    _userId,
                    txtBorrowerUserId.Text,
                    cboCurrency.Text,
                    decimal.Parse(txtAmount.Text),
                    exchangeRate
                );

                MessageBox.Show("Borç kaydı başarıyla eklendi.", "Başarılı");
                await LoadUserDebts();
                _mainForm.LoadUserInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata");
            }
        }

        private async void btnDeposit_Click(object? sender, EventArgs e)
        {
            try
            {
                await _database.AddBalanceAsync(
                    _userId,
                    cboDepositCurrency.Text,
                    decimal.Parse(txtDepositAmount.Text)
                );

                MessageBox.Show("Para yükleme başarılı.", "Başarılı");
                await LoadUserBalances();
                _mainForm.LoadUserInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata");
            }
        }

        private async void btnRefreshRates_Click(object? sender, EventArgs e)
        {
            try
            {
                await LoadUserDebts();
                await LoadUserBalances();
                await LoadCurrentRates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata");
            }
        }

        private async Task LoadCurrentRates()
        {
            var rates = await _currencyApiService.GetExchangeRatesAsync();
            dgvRates.Rows.Clear();
            foreach (var rate in rates)
            {
                dgvRates.Rows.Add(
                    rate.Key,
                    rate.Value
                );
            }
        }

        private async void btnPayDebt_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgvDebts.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Lütfen ödenecek borcu seçin.", "Uyarı");
                    return;
                }

                var selectedRow = dgvDebts.SelectedRows[0];

                // Ödenmiş borç kontrolü
                var isPaid = selectedRow.DefaultCellStyle.ForeColor == Color.Red;
                if (isPaid)
                {
                    MessageBox.Show("Bu borç zaten ödenmiş.", "Uyarı");
                    return;
                }

                // Sütun indekslerini kullan
                var debtId = int.Parse(selectedRow.Cells[0].Value?.ToString() ?? throw new Exception("Borç ID bilgisi bulunamadı"));
                var debtCurrency = selectedRow.Cells[3].Value?.ToString() ?? throw new Exception("Para birimi bilgisi bulunamadı");
                var remainingAmount = decimal.Parse(selectedRow.Cells[5].Value?.ToString() ?? throw new Exception("Kalan miktar bilgisi bulunamadı"));

                if (!decimal.TryParse(txtPaymentAmount.Text, out decimal paymentAmount))
                {
                    MessageBox.Show("Lütfen geçerli bir ödeme miktarı girin.", "Uyarı");
                    return;
                }

                var paymentCurrency = cboPaymentCurrency.Text;

                // Kullanıcının seçilen para biriminde yeterli bakiyesi var mı kontrol et
                var balances = await _database.GetUserBalancesAsync(_userId);
                var currentBalance = balances.FirstOrDefault(b => b.CurrencyType == paymentCurrency)?.Amount ?? 0;

                if (currentBalance < paymentAmount)
                {
                    MessageBox.Show($"Yetersiz bakiye. {paymentCurrency} bakiyeniz: {currentBalance:N2}", "Uyarı");
                    return;
                }

                // Ödeme miktarı kalan borçtan fazla olmamalı
                var rates = await _currencyApiService.GetExchangeRatesAsync();
                if (!rates.ContainsKey(paymentCurrency) || !rates.ContainsKey(debtCurrency))
                {
                    throw new Exception("Kur bilgisi bulunamadı");
                }

                // Ödeme miktarını borç para birimine çevir
                decimal paymentRate = rates[paymentCurrency];
                decimal debtRate = rates[debtCurrency];
                decimal exchangeRate = debtRate / paymentRate;
                decimal convertedPaymentAmount = paymentAmount * exchangeRate;

                if (convertedPaymentAmount > remainingAmount)
                {
                    MessageBox.Show($"Ödeme miktarı kalan borç miktarından fazla olamaz. Maksimum ödeyebileceğiniz miktar: {remainingAmount / exchangeRate:N2} {paymentCurrency}", "Uyarı");
                    return;
                }

                await _database.PayDebtAsync(debtId, paymentCurrency, paymentAmount, exchangeRate);

                MessageBox.Show("Borç ödemesi başarıyla gerçekleşti.", "Başarılı");
                await LoadUserDebts();
                await LoadUserBalances();
                _mainForm.LoadUserInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata");
            }
        }

        private void UpdateExchangeRate(object? sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                try
                {
                    if (cboFromCurrency.SelectedItem != null && cboToCurrency.SelectedItem != null)
                    {
                        var rates = await _currencyApiService.GetExchangeRatesAsync();
                        var fromCurrency = cboFromCurrency.SelectedItem?.ToString()
                            ?? throw new Exception("Kaynak para birimi seçilmedi");
                        var toCurrency = cboToCurrency.SelectedItem?.ToString()
                            ?? throw new Exception("Hedef para birimi seçilmedi");

                        if (rates.ContainsKey(fromCurrency) && rates.ContainsKey(toCurrency))
                        {
                            decimal fromRate = rates[fromCurrency];
                            decimal toRate = rates[toCurrency];
                            decimal exchangeRate = toRate / fromRate;

                            this.Invoke(() => lblExchangeRate.Text = $"1 {fromCurrency} = {exchangeRate:N4} {toCurrency}");
                        }
                    }
                }
                catch
                {
                    this.Invoke(() => lblExchangeRate.Text = "Kur bilgisi alınamadı");
                }
            });
        }

        private async void btnExchange_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtExchangeAmount.Text))
                {
                    MessageBox.Show("Lütfen bir miktar girin.", "Uyarı");
                    return;
                }

                var fromCurrency = cboFromCurrency.SelectedItem?.ToString()
                    ?? throw new Exception("Kaynak para birimi seçilmedi");
                var toCurrency = cboToCurrency.SelectedItem?.ToString()
                    ?? throw new Exception("Hedef para birimi seçilmedi");
                var amount = decimal.Parse(txtExchangeAmount.Text);

                // Kullanıcının bakiyesini kontrol et
                var balances = await _database.GetUserBalancesAsync(_userId);
                var currentBalance = balances.FirstOrDefault(b => b.CurrencyType == fromCurrency)?.Amount ?? 0;

                if (currentBalance < amount)
                {
                    MessageBox.Show($"Yetersiz bakiye. Mevcut {fromCurrency} bakiyeniz: {currentBalance}", "Uyarı");
                    return;
                }

                // Güncel kurları al
                var rates = await _currencyApiService.GetExchangeRatesAsync();
                if (!rates.ContainsKey(fromCurrency) || !rates.ContainsKey(toCurrency))
                {
                    throw new Exception("Kur bilgisi bulunamadı");
                }

                // Doğru çevrim için EUR bazlı kurları kullan
                decimal fromRate = rates[fromCurrency];
                decimal toRate = rates[toCurrency];
                decimal exchangeRate = toRate / fromRate;
                decimal convertedAmount = amount * exchangeRate;

                // Kullanıcı onayı
                var result = MessageBox.Show(
                    $"{amount} {fromCurrency} = {convertedAmount:N2} {toCurrency}\n\nDöviz değişimini onaylıyor musunuz?",
                    "Döviz Değişimi Onayı",
                    MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    // Kaynak para birimini azalt
                    await _database.AddBalanceAsync(_userId, fromCurrency, -amount);
                    // Hedef para birimini artır
                    await _database.AddBalanceAsync(_userId, toCurrency, convertedAmount);

                    MessageBox.Show("Döviz değişimi başarıyla gerçekleşti.", "Başarılı");
                    await LoadUserBalances();
                    _mainForm.LoadUserInfo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata");
            }
        }

        private void InitializeGridColumns()
        {
            // Kurlar için sütunlar
            dgvRates.Columns.Add("Currency", "Para Birimi");
            dgvRates.Columns.Add("Rate", "Kur");

            // Bakiyeler için sütunlar
            dgvBalances.Columns.Add("Currency", "Para Birimi");
            dgvBalances.Columns.Add("Amount", "Miktar");

            // Borçlar için sütunlar
            dgvDebts.Columns.Add("Id", "ID");
            dgvDebts.Columns.Add("LenderName", "Alacaklı");
            dgvDebts.Columns.Add("BorrowerName", "Borçlu");
            dgvDebts.Columns.Add("CurrencyType", "Para Birimi");
            dgvDebts.Columns.Add("OriginalAmount", "Asıl Miktar");
            dgvDebts.Columns.Add("RemainingAmount", "Kalan Miktar");
            dgvDebts.Columns.Add("ExchangeRateAtBorrowing", "Alınan Kur");
            dgvDebts.Columns.Add("BorrowingDate", "Borç Tarihi");

            // Ödenmiş borçlar için araç ipucu ekle
            dgvDebts.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0 && dgvDebts.Rows[e.RowIndex].DefaultCellStyle.ForeColor == Color.Red)
                {
                    dgvDebts.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "Bu borç ödenmiş";
                }
            };
        }

        private void btnAddDebt_Click_1(object sender, EventArgs e)
        {

        }

        private void UserDashboardForm_Load(object sender, EventArgs e)
        {

        }
    }
}