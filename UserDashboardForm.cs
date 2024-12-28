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
        private List<string> _allCurrencies = new List<string>();
        private List<string> _userCurrencies = new List<string>();
        public TextBox txtDepositSearch = new TextBox();
        public TextBox txtExchangeSearch = new TextBox();

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
            txtDepositSearch.TextChanged += TxtDepositSearch_TextChanged;
            txtExchangeSearch.TextChanged += TxtExchangeSearch_TextChanged;
            cboFromCurrency.SelectedIndexChanged += CboFromCurrency_SelectedIndexChanged;

            LoadInitialData();
        }

        private async void LoadInitialData()
        {
            try 
            {
                await LoadUserDebts();
                await LoadUserBalances();
                await LoadCurrentRates();
                await LoadCurrencyTypes(); // Kur tiplerini yükle
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veri yükleme hatası: {ex.Message}", "Hata");
            }
        }

        private async Task LoadUserDebts()
        {
            var debts = await _database.GetUserDebtsAsync(_userId);
            dgvDebts.Rows.Clear();
            foreach (var debt in debts)
            {
                var rowIndex = dgvDebts.Rows.Add(
                    debt.Id,
                    debt.LenderUserId,
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

        private async Task LoadCurrentRates(string? searchCurrency = null)
        {
            try
            {
                var rates = await _currencyApiService.GetExchangeRatesAsync();
                dgvRates.Rows.Clear();

                var filteredRates = rates;
                if (!string.IsNullOrEmpty(searchCurrency))
                {
                    filteredRates = rates.Where(rate =>
                        rate.Key.Contains(searchCurrency, StringComparison.OrdinalIgnoreCase))
                        .ToDictionary(x => x.Key, x => x.Value);
                }

                foreach (var rate in filteredRates)
                {
                    dgvRates.Rows.Add(
                        rate.Key,
                        rate.Value,
                        rate.Value * 1.01m, // Satış kuru için %1 fark ekledik
                        DateTime.Now.ToString("HH:mm:ss")
                    );
                }

                if (!filteredRates.Any() && !string.IsNullOrEmpty(searchCurrency))
                {
                    MessageBox.Show("Aradığınız döviz kodu bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var debtId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                var debtCurrency = selectedRow.Cells["CurrencyType"].Value?.ToString()
                    ?? throw new Exception("Para birimi bilgisi bulunamadı");
                var remainingAmount = Convert.ToDecimal(selectedRow.Cells["RemainingAmount"].Value);
                
                // Ödenmiş borç kontrolü
                var isPaid = selectedRow.DefaultCellStyle.ForeColor == Color.Red;
                if (isPaid)
                {
                    MessageBox.Show("Bu borç zaten ödenmiş.", "Uyarı");
                    return;
                }

                if (!decimal.TryParse(txtPaymentAmount.Text, out decimal paymentAmount))
                {
                    MessageBox.Show("Lütfen geçerli bir ödeme miktarı girin.", "Uyarı");
                    return;
                }

                // Kullanıcının seçilen para biriminde yeterli bakiyesi var mı kontrol et
                var balances = await _database.GetUserBalancesAsync(_userId);
                var currentBalance = balances.FirstOrDefault(b => b.CurrencyType == debtCurrency)?.Amount ?? 0;

                if (currentBalance < paymentAmount)
                {
                    MessageBox.Show($"Yetersiz bakiye. {debtCurrency} bakiyeniz: {currentBalance:N2}", "Uyarı");
                    return;
                }

                // Ödeme miktarı kalan borçtan fazla olmamalı
                var rates = await _currencyApiService.GetExchangeRatesAsync();
                if (!rates.ContainsKey(debtCurrency))
                {
                    throw new Exception("Kur bilgisi bulunamadı");
                }

                // Ödeme miktarını borç para birimine çevir
                decimal debtRate = rates[debtCurrency];
                decimal exchangeRate = debtRate / paymentAmount;
                decimal convertedPaymentAmount = paymentAmount * exchangeRate;

                if (convertedPaymentAmount > remainingAmount)
                {
                    MessageBox.Show($"Ödeme miktarı kalan borç miktarından fazla olamaz. Maksimum ödeyebileceğiniz miktar: {remainingAmount / exchangeRate:N2} {debtCurrency}", "Uyarı");
                    return;
                }

                await _database.PayDebtAsync(debtId, debtCurrency, paymentAmount, exchangeRate);

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
            dgvRates.Columns.Add("SellRate", "Satış");
            dgvRates.Columns.Add("Time", "Saat");

            // Bakiyeler için sütunlar
            dgvBalances.Columns.Add("Currency", "Para Birimi");
            dgvBalances.Columns.Add("Amount", "Miktar");

            // Borçlar için sütunlar
            dgvDebts.Columns.Add("Id", "İşlem No");
            dgvDebts.Columns.Add("LenderUserId", "Alacaklı ID");
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

            // DataGridView seçim event'ini ekle
            dgvDebts.SelectionChanged += DgvDebts_SelectionChanged;
        }

        private async void DgvDebts_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvDebts.SelectedRows.Count > 0)
            {
                var selectedRow = dgvDebts.SelectedRows[0];
                var debtCurrency = selectedRow.Cells["CurrencyType"].Value?.ToString()
                    ?? throw new Exception("Para birimi bilgisi bulunamadı");
                
                if (!string.IsNullOrEmpty(debtCurrency))
                {
                    // Seçilen borcun para birimini ComboBox'a set et
                    cboPaymentCurrency.SelectedItem = debtCurrency;
                    cboPaymentCurrency.Enabled = false; // Değiştirilemez yap
                    
                    // Kullanıcının bu para birimindeki bakiyesini göster
                    var balances = await _database.GetUserBalancesAsync(_userId);
                    var currentBalance = balances.FirstOrDefault(b => b.CurrencyType == debtCurrency)?.Amount ?? 0;
                    
                    txtPaymentAmount.PlaceholderText = $"Mevcut {debtCurrency} bakiyeniz: {currentBalance:N2}";
                }
            }
        }

        private void btnAddDebt_Click_1(object sender, EventArgs e)
        {

        }

        private void UserDashboardForm_Load(object sender, EventArgs e)
        {

        }

        private async void TxtSearchRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string searchCurrency = txtSearchRate.Text.Trim().ToUpper();
                await LoadCurrentRates(searchCurrency);
            }
        }

        private async Task LoadCurrencyTypes()
        {
            try
            {
                // API'den tüm kurları al
                var rates = await _currencyApiService.GetExchangeRatesAsync();
                _allCurrencies = rates.Keys.ToList();
                
                // Kullanıcının sahip olduğu kurları al
                var balances = await _database.GetUserBalancesAsync(_userId);
                _userCurrencies = balances.Select(b => b.CurrencyType).ToList();
                
                // From ComboBox'a sadece kullanıcının sahip olduğu kurları ekle
                cboFromCurrency.Items.Clear();
                foreach (var currency in _userCurrencies)
                {
                    cboFromCurrency.Items.Add(currency);
                }
                
                // Para yükleme için ComboBox'ı güncelle
                UpdateDepositCurrencyList(string.Empty);
                
                // Döviz değişimi için hedef para birimlerini güncelle
                if (cboFromCurrency.SelectedItem != null)
                {
                    UpdateExchangeToCurrencyList(string.Empty);
                }
                
                // Varsayılan seçimler
                if (cboFromCurrency.Items.Count > 0)
                    cboFromCurrency.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Para birimleri yüklenirken hata oluştu: {ex.Message}", "Hata");
            }
        }

        private void TxtDepositSearch_TextChanged(object? sender, EventArgs e)
        {
            string searchText = txtDepositSearch.Text.Trim().ToUpper();
            UpdateDepositCurrencyList(searchText);
        }

        private void UpdateDepositCurrencyList(string searchText)
        {
            cboDepositCurrency.Items.Clear();
            
            var filteredCurrencies = string.IsNullOrEmpty(searchText) 
                ? _allCurrencies 
                : _allCurrencies.Where(c => c.Contains(searchText, StringComparison.OrdinalIgnoreCase));
            
            foreach (var currency in filteredCurrencies)
            {
                cboDepositCurrency.Items.Add(currency);
            }
            
            if (cboDepositCurrency.Items.Count > 0)
            {
                cboDepositCurrency.SelectedIndex = 0;
            }
        }

        private void TxtExchangeSearch_TextChanged(object? sender, EventArgs e)
        {
            string searchText = txtExchangeSearch.Text.Trim().ToUpper();
            UpdateExchangeToCurrencyList(searchText);
        }

        private void UpdateExchangeToCurrencyList(string searchText)
        {
            cboToCurrency.Items.Clear();
            
            var selectedFromCurrency = cboFromCurrency.SelectedItem?.ToString();
            if (selectedFromCurrency == null) return;
            
            var filteredCurrencies = string.IsNullOrEmpty(searchText) 
                ? _allCurrencies 
                : _allCurrencies.Where(c => c.Contains(searchText, StringComparison.OrdinalIgnoreCase));
            
            foreach (var currency in filteredCurrencies)
            {
                // Seçili olan para birimini hedef listesinden çıkar
                // Diğer tüm kurları (kullanıcının sahip olduğu veya olmadığı) göster
                if (currency != selectedFromCurrency)
                {
                    cboToCurrency.Items.Add(currency);
                }
            }
            
            if (cboToCurrency.Items.Count > 0)
            {
                cboToCurrency.SelectedIndex = 0;
            }
        }

        // From Currency değiştiğinde To Currency listesini güncelle
        private void CboFromCurrency_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateExchangeToCurrencyList(txtExchangeSearch.Text.Trim());
        }
    }
}