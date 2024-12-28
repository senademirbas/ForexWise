namespace ForexWise
{
    public partial class UserDashboardForm : Form
    {
        private System.ComponentModel.IContainer components = null;
        
        // DataGridViews
        public DataGridView dgvDebts;
        public DataGridView dgvBalances;
        public DataGridView dgvRates;
        
        // Borç Ekleme Kontrolleri
        public TextBox txtBorrowerUserId;
        public TextBox txtAmount;
        public ComboBox cboCurrency;
        public Button btnAddDebt;
        
        // Para Yükleme Kontrolleri
        public TextBox txtDepositAmount;
        public ComboBox cboDepositCurrency;
        public Button btnDeposit;
        
        // Borç Ödeme Kontrolleri
        public TextBox txtPaymentAmount;
        public ComboBox cboPaymentCurrency;
        public Button btnPayDebt;
        
        // Döviz Değişim Kontrolleri
        public ComboBox cboFromCurrency;
        public ComboBox cboToCurrency;
        public TextBox txtExchangeAmount;
        public Button btnExchange;
        public Label lblExchangeRate;
        
        // Diğer Kontroller
        public Button btnRefreshRates;

        public void InitializeComponent()
        {
            dgvDebts = new DataGridView();
            dgvBalances = new DataGridView();
            dgvRates = new DataGridView();
            txtDepositAmount = new TextBox();
            cboDepositCurrency = new ComboBox();
            btnDeposit = new Button();
            txtBorrowerUserId = new TextBox();
            txtAmount = new TextBox();
            cboCurrency = new ComboBox();
            btnAddDebt = new Button();
            btnRefreshRates = new Button();
            txtPaymentAmount = new TextBox();
            cboPaymentCurrency = new ComboBox();
            btnPayDebt = new Button();
            cboFromCurrency = new ComboBox();
            cboToCurrency = new ComboBox();
            txtExchangeAmount = new TextBox();
            btnExchange = new Button();
            lblExchangeRate = new Label();
            exchangeLabel = new Label();
            depositLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvDebts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvBalances).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRates).BeginInit();
            SuspendLayout();
            // 
            // dgvDebts
            // 
            dgvDebts.AllowUserToAddRows = false;
            dgvDebts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDebts.ColumnHeadersHeight = 29;
            dgvDebts.Location = new Point(12, 12);
            dgvDebts.MultiSelect = false;
            dgvDebts.Name = "dgvDebts";
            dgvDebts.ReadOnly = true;
            dgvDebts.RowHeadersWidth = 51;
            dgvDebts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDebts.Size = new Size(991, 212);
            dgvDebts.TabIndex = 0;
            // 
            // dgvBalances
            // 
            dgvBalances.AllowUserToAddRows = false;
            dgvBalances.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBalances.ColumnHeadersHeight = 29;
            dgvBalances.Location = new Point(12, 220);
            dgvBalances.Name = "dgvBalances";
            dgvBalances.ReadOnly = true;
            dgvBalances.RowHeadersWidth = 51;
            dgvBalances.Size = new Size(991, 236);
            dgvBalances.TabIndex = 1;
            // 
            // dgvRates
            // 
            dgvRates.AllowUserToAddRows = false;
            dgvRates.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRates.ColumnHeadersHeight = 29;
            dgvRates.Location = new Point(1009, 56);
            dgvRates.Name = "dgvRates";
            dgvRates.ReadOnly = true;
            dgvRates.RowHeadersWidth = 51;
            dgvRates.Size = new Size(348, 290);
            dgvRates.TabIndex = 2;
            // 
            // txtDepositAmount
            // 
            txtDepositAmount.Location = new Point(357, 471);
            txtDepositAmount.Name = "txtDepositAmount";
            txtDepositAmount.PlaceholderText = "Yüklenecek Miktar";
            txtDepositAmount.Size = new Size(200, 27);
            txtDepositAmount.TabIndex = 7;
            // 
            // cboDepositCurrency
            // 
            cboDepositCurrency.Items.AddRange(new object[] { "USD", "EUR", "TRY", "USD", "EUR", "TRY" });
            cboDepositCurrency.Location = new Point(357, 500);
            cboDepositCurrency.Name = "cboDepositCurrency";
            cboDepositCurrency.Size = new Size(200, 28);
            cboDepositCurrency.TabIndex = 8;
            // 
            // btnDeposit
            // 
            btnDeposit.Location = new Point(357, 529);
            btnDeposit.Name = "btnDeposit";
            btnDeposit.Size = new Size(200, 30);
            btnDeposit.TabIndex = 9;
            btnDeposit.Text = "Para Yükle";
            // 
            // txtBorrowerUserId
            // 
            txtBorrowerUserId.Location = new Point(1114, 369);
            txtBorrowerUserId.Name = "txtBorrowerUserId";
            txtBorrowerUserId.PlaceholderText = "Borçlu ID";
            txtBorrowerUserId.Size = new Size(200, 27);
            txtBorrowerUserId.TabIndex = 3;
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(1114, 398);
            txtAmount.Name = "txtAmount";
            txtAmount.PlaceholderText = "Miktar";
            txtAmount.Size = new Size(200, 27);
            txtAmount.TabIndex = 4;
            // 
            // cboCurrency
            // 
            cboCurrency.Items.AddRange(new object[] { "USD", "EUR", "TRY" });
            cboCurrency.Location = new Point(1114, 427);
            cboCurrency.Name = "cboCurrency";
            cboCurrency.Size = new Size(200, 28);
            cboCurrency.TabIndex = 5;
            // 
            // btnAddDebt
            // 
            btnAddDebt.Location = new Point(1114, 456);
            btnAddDebt.Name = "btnAddDebt";
            btnAddDebt.Size = new Size(200, 30);
            btnAddDebt.TabIndex = 6;
            btnAddDebt.Text = "Borç Ekle";
            btnAddDebt.Click += btnAddDebt_Click_1;
            // 
            // btnRefreshRates
            // 
            btnRefreshRates.Location = new Point(1157, 10);
            btnRefreshRates.Name = "btnRefreshRates";
            btnRefreshRates.Size = new Size(200, 30);
            btnRefreshRates.TabIndex = 10;
            btnRefreshRates.Text = "Kurları Yenile";
            // 
            // txtPaymentAmount
            // 
            txtPaymentAmount.Location = new Point(1114, 506);
            txtPaymentAmount.Name = "txtPaymentAmount";
            txtPaymentAmount.PlaceholderText = "Ödenecek Miktar";
            txtPaymentAmount.Size = new Size(200, 27);
            txtPaymentAmount.TabIndex = 11;
            // 
            // cboPaymentCurrency
            // 
            cboPaymentCurrency.Items.AddRange(new object[] { "USD", "EUR", "TRY" });
            cboPaymentCurrency.Location = new Point(1114, 535);
            cboPaymentCurrency.Name = "cboPaymentCurrency";
            cboPaymentCurrency.Size = new Size(200, 28);
            cboPaymentCurrency.TabIndex = 12;
            // 
            // btnPayDebt
            // 
            btnPayDebt.Location = new Point(1114, 564);
            btnPayDebt.Name = "btnPayDebt";
            btnPayDebt.Size = new Size(200, 30);
            btnPayDebt.TabIndex = 13;
            btnPayDebt.Text = "Borç Öde";
            // 
            // cboFromCurrency
            // 
            cboFromCurrency.Items.AddRange(new object[] { "USD", "EUR", "TRY" });
            cboFromCurrency.Location = new Point(29, 486);
            cboFromCurrency.Name = "cboFromCurrency";
            cboFromCurrency.Size = new Size(120, 28);
            cboFromCurrency.TabIndex = 15;
            // 
            // cboToCurrency
            // 
            cboToCurrency.Items.AddRange(new object[] { "USD", "EUR", "TRY" });
            cboToCurrency.Location = new Point(187, 486);
            cboToCurrency.Name = "cboToCurrency";
            cboToCurrency.Size = new Size(120, 28);
            cboToCurrency.TabIndex = 16;
            // 
            // txtExchangeAmount
            // 
            txtExchangeAmount.Location = new Point(29, 516);
            txtExchangeAmount.Name = "txtExchangeAmount";
            txtExchangeAmount.PlaceholderText = "Miktar";
            txtExchangeAmount.Size = new Size(120, 27);
            txtExchangeAmount.TabIndex = 17;
            // 
            // btnExchange
            // 
            btnExchange.Location = new Point(187, 516);
            btnExchange.Name = "btnExchange";
            btnExchange.Size = new Size(120, 30);
            btnExchange.TabIndex = 18;
            btnExchange.Text = "Döviz Değiştir";
            // 
            // lblExchangeRate
            // 
            lblExchangeRate.AutoSize = true;
            lblExchangeRate.Location = new Point(157, 489);
            lblExchangeRate.Name = "lblExchangeRate";
            lblExchangeRate.Size = new Size(22, 20);
            lblExchangeRate.TabIndex = 19;
            lblExchangeRate.Text = "→";
            // 
            // exchangeLabel
            // 
            exchangeLabel.Location = new Point(0, 0);
            exchangeLabel.Name = "exchangeLabel";
            exchangeLabel.Size = new Size(100, 23);
            exchangeLabel.TabIndex = 14;
            // 
            // depositLabel
            // 
            depositLabel.Location = new Point(0, 0);
            depositLabel.Name = "depositLabel";
            depositLabel.Size = new Size(100, 23);
            depositLabel.TabIndex = 20;
            // 
            // UserDashboardForm
            // 
            ClientSize = new Size(1378, 614);
            Controls.Add(dgvDebts);
            Controls.Add(dgvBalances);
            Controls.Add(dgvRates);
            Controls.Add(txtBorrowerUserId);
            Controls.Add(txtAmount);
            Controls.Add(cboCurrency);
            Controls.Add(btnAddDebt);
            Controls.Add(btnRefreshRates);
            Controls.Add(txtPaymentAmount);
            Controls.Add(cboPaymentCurrency);
            Controls.Add(btnPayDebt);
            Controls.Add(exchangeLabel);
            Controls.Add(cboFromCurrency);
            Controls.Add(cboToCurrency);
            Controls.Add(txtExchangeAmount);
            Controls.Add(btnExchange);
            Controls.Add(lblExchangeRate);
            Controls.Add(depositLabel);
            Controls.Add(txtDepositAmount);
            Controls.Add(cboDepositCurrency);
            Controls.Add(btnDeposit);
            Name = "UserDashboardForm";
            Text = "ForexWise - Cüzdan";
            Load += UserDashboardForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDebts).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvBalances).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRates).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Label depositLabel;
        private Label exchangeLabel;
    }
}