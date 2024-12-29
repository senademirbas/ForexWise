using System;
using System.Windows.Forms;
using ClassLibrary;
using System.Linq;
using System.Drawing;

namespace ForexWise
{
    public partial class UserDashboardForm : Form
    {
        private readonly DashboardManager _dashboardManager;
        private readonly MainForm _mainForm;

        public UserDashboardForm(string userId, MainForm mainForm)
        {
            InitializeComponent();
            InitializeGridColumns();

            _mainForm = mainForm;
            _dashboardManager = new DashboardManager(userId, @"Server=.\SQLEXPRESS;Database=ForexWise;Trusted_Connection=True;TrustServerCertificate=True");

            // Event dinleyicileri
            _dashboardManager.OnDebtsUpdated += UpdateDebtsGrid;
            _dashboardManager.OnBalancesUpdated += UpdateBalancesGrid;
            _dashboardManager.OnRatesUpdated += UpdateRatesGrid;

            AttachEventHandlers();
            this.Load += async (s, e) =>
            {
                await _dashboardManager.LoadInitialData();
                await UpdateCurrencyComboBoxes();
            };
        }

        private void AttachEventHandlers()
        {
            btnDeposit.Click += async (s, e) => await HandleDeposit();
            btnRefreshRates.Click += async (s, e) => await HandleRefresh();
            btnPayDebt.Click += async (s, e) => await HandleDebtPayment();
            btnExchange.Click += async (s, e) => await HandleExchange();
            btnAddDebt.Click += async (s, e) => await HandleAddDebt();

            cboFromCurrency.SelectedIndexChanged += UpdateExchangeRate;
            cboToCurrency.SelectedIndexChanged += UpdateExchangeRate;

            // Kurlar için arama işlevselliğini koru
            txtSearchRate.TextChanged += async (s, e) =>
            {
                try 
                {
                    var searchText = txtSearchRate.Text.Trim().ToUpper();
                    var rates = await _dashboardManager.GetCurrentRates(searchText);
                    
                    dgvRates.Rows.Clear();
                    foreach (var rate in rates)
                    {
                        dgvRates.Rows.Add(
                            rate.Key, 
                            rate.Value, 
                            rate.Value * 1.01m, 
                            DateTime.Now.ToShortTimeString()
                        );
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Kur araması sırasında hata: {ex.Message}", "Hata");
                }
            };

            // ComboBox'lar için Windows Forms klasik özellikleri
            var comboBoxes = new[] { cboCurrency, cboDepositCurrency, cboPaymentCurrency, 
                                    cboFromCurrency, cboToCurrency };
            foreach (var cbo in comboBoxes)
            {
                cbo.DropDownStyle = ComboBoxStyle.DropDownList;
                cbo.FlatStyle = FlatStyle.System;
                cbo.BackColor = SystemColors.Window;
                cbo.Font = new Font("Segoe UI", 11F);
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

            // Event handlers
            dgvDebts.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0 && dgvDebts.Rows[e.RowIndex].DefaultCellStyle.ForeColor == Color.Red)
                {
                    dgvDebts.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "Bu borç ödenmiş";
                }
            };

            dgvDebts.SelectionChanged += DgvDebts_SelectionChanged;
        }

        private async Task HandleRefresh()
        {
            try
            {
                await _dashboardManager.LoadInitialData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata");
            }
        }

        private async Task HandleDeposit()
        {
            try
            {
                var currency = cboDepositCurrency.Text;
                if (decimal.TryParse(txtDepositAmount.Text, out decimal amount))
                {
                    await _dashboardManager.AddBalance(currency, amount);
                    await _dashboardManager.LoadInitialData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata");
            }
        }

        private async Task HandleDebtPayment()
        {
            try
            {
                if (dgvDebts.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Lütfen ödenecek borcu seçin", "Uyarı");
                    return;
                }

                var selectedRow = dgvDebts.SelectedRows[0];
                var debtId = Convert.ToInt32(selectedRow.Cells["Id"].Value);
                var lenderUserId = selectedRow.Cells["LenderUserId"].Value.ToString();
                var remainingAmount = Convert.ToDecimal(selectedRow.Cells["RemainingAmount"].Value);
                
                // Borç ödenmiş mi kontrolü
                if (remainingAmount <= 0)
                {
                    MessageBox.Show("Bu borç zaten ödenmiş.", "Uyarı");
                    return;
                }

                // Alacaklı kontrolü
                if (lenderUserId == _dashboardManager.UserId)
                {
                    MessageBox.Show("Bu kayıt sizin alacağınız, borç ödeme işlemi yapamazsınız.", "Uyarı");
                    return;
                }
                
                if (decimal.TryParse(txtPaymentAmount.Text, out decimal amount))
                {
                    var (success, message) = await _dashboardManager.ProcessDebtPayment(
                        debtId, 
                        cboPaymentCurrency.Text, 
                        amount
                    );
                    
                    MessageBox.Show(message, success ? "Başarılı" : "Hata");
                    if (success)
                    {
                        txtPaymentAmount.Clear();
                        cboPaymentCurrency.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata");
            }
        }

        private async Task HandleExchange()
        {
            try
            {
                if (cboFromCurrency.SelectedItem == null || cboToCurrency.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen para birimlerini seçin", "Uyarı");
                    return;
                }

                if (!decimal.TryParse(txtExchangeAmount.Text, out decimal amount))
                {
                    MessageBox.Show("Lütfen geçerli bir miktar girin", "Uyarı");
                    return;
                }

                var fromCurrency = cboFromCurrency.SelectedItem.ToString()!;
                var toCurrency = cboToCurrency.SelectedItem.ToString()!;

                // Döviz değişimi öncesi doğrulama ve onay
                var (success, message) = await _dashboardManager.ValidateExchange(fromCurrency, toCurrency, amount);

                if (!success)
                {
                    MessageBox.Show(message, "Hata");
                    return;
                }

                // Kullanıcıdan onay al
                var result = MessageBox.Show(message, "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (await _dashboardManager.ProcessExchange(fromCurrency, toCurrency, amount))
                    {
                        await _dashboardManager.LoadInitialData();
                        MessageBox.Show("Döviz değişimi başarıyla gerçekleştirildi", "Başarılı");
                    }
                    else
                    {
                        MessageBox.Show("Döviz değişimi sırasında bir hata oluştu", "Hata");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata");
            }
        }

        private void DgvDebts_SelectionChanged(object? sender, EventArgs? e)
        {
            if (dgvDebts.SelectedRows.Count > 0)
            {
                var selectedRow = dgvDebts.SelectedRows[0];
                var lenderUserId = selectedRow.Cells["LenderUserId"].Value.ToString();
                var remainingAmount = Convert.ToDecimal(selectedRow.Cells["RemainingAmount"].Value);
                
                // Alacaklı kontrolü ve ödenmiş borç kontrolü
                bool isLender = lenderUserId == _dashboardManager.UserId;
                bool isPaid = remainingAmount <= 0;
                
                txtPaymentAmount.Enabled = !isLender && !isPaid;
                cboPaymentCurrency.Enabled = !isLender && !isPaid;
                btnPayDebt.Enabled = !isLender && !isPaid;
                
                if (!isLender && !isPaid)
                {
                    txtPaymentAmount.Text = remainingAmount.ToString();
                    cboPaymentCurrency.Text = selectedRow.Cells["CurrencyType"].Value?.ToString();
                }
                else
                {
                    txtPaymentAmount.Clear();
                    cboPaymentCurrency.SelectedIndex = -1;
                }
            }
        }

        private async Task HandleAddDebt()
        {
            try
            {
                var borrowerId = txtBorrowerUserId.Text;
                var currency = cboCurrency.Text;
                if (decimal.TryParse(txtAmount.Text, out decimal amount))
                {
                    var (success, message) = await _dashboardManager.ProcessDebtAddition(borrowerId, currency, amount);
                    MessageBox.Show(message, success ? "Başarılı" : "Hata");
                    
                    if (success)
                    {
                        txtBorrowerUserId.Clear();
                        txtAmount.Clear();
                        cboCurrency.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata");
            }
        }

        private async void UpdateExchangeRate(object? sender, EventArgs? e)
        {
            if (cboFromCurrency.SelectedItem != null && cboToCurrency.SelectedItem != null)
            {
                var fromCurrency = cboFromCurrency.SelectedItem.ToString();
                var toCurrency = cboToCurrency.SelectedItem.ToString();

                if (fromCurrency == toCurrency)
                {
                    lblExchangeRate.Text = "Aynı para birimi seçilemez";
                    return;
                }

                try
                {
                    var rates = await _dashboardManager.GetCurrentRates();
                    if (rates.ContainsKey(fromCurrency!) && rates.ContainsKey(toCurrency!))
                    {
                       
                    }
                }
                catch
                {
                    lblExchangeRate.Text = "Kur bilgisi alınamadı";
                }
            }
        }

        private void UpdateDebtsGrid(object? sender, List<DebtRecord> debts)
        {
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

                if (debt.RemainingAmount <= 0)
                {
                    dgvDebts.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Red;
                }
            }
        }

        private void UpdateBalancesGrid(object? sender, List<UserBalance> balances)
        {
            dgvBalances.Rows.Clear();
            foreach (var balance in balances)
            {
                dgvBalances.Rows.Add(balance.CurrencyType, balance.Amount);
            }
        }

        private void UpdateRatesGrid(object? sender, Dictionary<string, decimal> rates)
        {
            dgvRates.Rows.Clear();
            foreach (var rate in rates)
            {
                dgvRates.Rows.Add(rate.Key, rate.Value, rate.Value * 1.01m, DateTime.Now.ToShortTimeString());
            }
        }

        private async Task UpdateCurrencyComboBoxes()
        {
            var (allCurrencies, userCurrencies) = await _dashboardManager.GetCurrencyTypes();

            // Tüm ComboBox'ları temizle
            cboFromCurrency.Items.Clear();
            cboToCurrency.Items.Clear();
            cboDepositCurrency.Items.Clear();
            cboPaymentCurrency.Items.Clear();
            cboCurrency.Items.Clear();

            // FromCurrency sadece kullanıcının sahip olduğu para birimleri
            cboFromCurrency.Items.AddRange(userCurrencies.ToArray());

            // Diğer ComboBox'lar tüm para birimleri
            cboToCurrency.Items.AddRange(allCurrencies.ToArray());
            cboDepositCurrency.Items.AddRange(allCurrencies.ToArray());
            cboPaymentCurrency.Items.AddRange(allCurrencies.ToArray());
            cboCurrency.Items.AddRange(allCurrencies.ToArray());
        }

        
    }
}