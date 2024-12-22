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
        private readonly CurrencyApiService _currencyApiService;
        private readonly List<string> _importantCurrencies = new List<string>
        {
            "USD", "EUR", "GBP", "JPY", "AUD", "CAD", "CHF", "CNY", "HKD", "NZD", "TRY"
        };

        public MainForm()
        {
            InitializeComponent();
            _currencyApiService = new CurrencyApiService();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            dgvCurrencies.ColumnCount = 4;
            dgvCurrencies.Columns[0].Name = "Currency";
            dgvCurrencies.Columns[1].Name = "Alýþ (Buy)";
            dgvCurrencies.Columns[2].Name = "Satýþ (Sell)";
            dgvCurrencies.Columns[3].Name = "Saat (Time)";
        }

        private async void BtnRefresh_Click(object sender, EventArgs e)
        {
            await RefreshCurrencyDataAsync();
        }

        private async Task RefreshCurrencyDataAsync(string searchCurrency = null)
        {
            try
            {
                var rates = await _currencyApiService.GetExchangeRatesAsync();

                dgvCurrencies.Rows.Clear();

                var filteredRates = rates
                    .Where(rate => _importantCurrencies.Contains(rate.Key) || rate.Key.Equals(searchCurrency, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                foreach (var rate in filteredRates)
                {
                    dgvCurrencies.Rows.Add(rate.Key, rate.Value, rate.Value * 1.01m, DateTime.Now.ToString("HH:mm:ss"));
                }

                if (!filteredRates.Any())
                {
                    MessageBox.Show("Aradýðýnýz döviz kodu bulunamadý.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string searchCurrency = searchTextBox.Text.Trim().ToUpper();

                if (!string.IsNullOrEmpty(searchCurrency))
                {
                    await RefreshCurrencyDataAsync(searchCurrency);
                }
            }
        }
    }
}
