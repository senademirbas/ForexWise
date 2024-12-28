using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using ClassLibrary;

namespace ForexWise
{
    public partial class MainForm : Form
    {
        private readonly string? _userId;
        private readonly CurrencyApiService _currencyApiService;
        private readonly List<string> _importantCurrencies = new List<string>
        {
            "USD", "EUR", "GBP", "JPY", "AUD", "CAD", "CHF", "CNY", "HKD", "NZD", "TRY"
        };
        private readonly Database _database;

        public MainForm(string? userId = null)
        {
            InitializeComponent();
            _userId = userId;
            _database = new Database(@"Server=.\SQLEXPRESS;Database=ForexWise;Trusted_Connection=True;TrustServerCertificate=True");
            _currencyApiService = new CurrencyApiService("ca7c3b8783524fff993e544fbf9a44a8");
            InitializeDataGridView();

            if (!string.IsNullOrEmpty(_userId))
            {
                btnProfile.Visible = true;
                LoadUserInfo();
            }
        }

        private void InitializeDataGridView()
        {
            dgvCurrencies.ColumnCount = 4;
            dgvCurrencies.Columns[0].Name = "Kur";
            dgvCurrencies.Columns[1].Name = "Alış";
            dgvCurrencies.Columns[2].Name = "Satış";
            dgvCurrencies.Columns[3].Name = "Saat";
        }

        private async void BtnRefresh_Click(object sender, EventArgs e)
        {
            await RefreshCurrencyDataAsync();
        }

        private async Task RefreshCurrencyDataAsync(string? searchCurrency = null)
        {
            try
            {
                var rates = await _currencyApiService.GetExchangeRatesAsync();
                dgvCurrencies.Rows.Clear();

                var filteredRates = rates;
                if (!string.IsNullOrEmpty(searchCurrency))
                {
                    filteredRates = rates.Where(rate => 
                        rate.Key.Contains(searchCurrency, StringComparison.OrdinalIgnoreCase))
                        .ToDictionary(x => x.Key, x => x.Value);
                }
                else
                {
                    filteredRates = rates.Where(rate => _importantCurrencies.Contains(rate.Key))
                        .ToDictionary(x => x.Key, x => x.Value);
                }

                foreach (var rate in filteredRates)
                {
                    dgvCurrencies.Rows.Add(
                        rate.Key, 
                        rate.Value, 
                        rate.Value * 1.01m, 
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

        private async void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string searchCurrency = searchTextBox.Text.Trim().ToUpper();
                await RefreshCurrencyDataAsync(searchCurrency);
            }
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            if (_userId != null)
            {
                var dashboardForm = new UserDashboardForm(_userId, this);
                dashboardForm.Show();
            }
        }

        public async void LoadUserInfo()
        {
            try
            {
                if (_userId != null)
                {
                    var userInfo = await _database.GetUserInfoAsync(_userId);
                    
                    string formattedName = System.Globalization.CultureInfo.CurrentCulture.TextInfo
                        .ToTitleCase(userInfo.FullName.ToLower());
                    
                    lblUserInfo.Text = $"Hoş Geldiniz {formattedName}!";
                    lblUserInfo.Visible = true;

                    var balances = await _database.GetUserBalancesAsync(_userId);
                    UpdateBalancesPanel(balances);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bilgi yükleme hatası: {ex.Message}", "Hata");
            }
        }

        private void UpdateBalancesPanel(List<UserBalance> balances)
        {
            pnlUserBalances.Controls.Clear();
            int y = 10;
            
            foreach (var balance in balances)
            {
                Label lblBalance = new Label
                {
                    Text = $"{balance.CurrencyType}: {balance.Amount:N2}",
                    Font = new Font("Segoe UI", 9),
                    Location = new Point(10, y),
                    AutoSize = true,
                    ForeColor = Color.FromArgb(0, 102, 204)
                };
                pnlUserBalances.Controls.Add(lblBalance);
                y += 25;
            }
        }
    }
}
