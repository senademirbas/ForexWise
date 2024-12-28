namespace ForexWise
{
    partial class UserDashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        
        // DataGridViews
        public DataGridView dgvDebts;
        public DataGridView dgvBalances;
        public DataGridView dgvRates;
        
        // TextBoxes
        public TextBox txtBorrowerUserId;
        public TextBox txtAmount;
        public TextBox txtDepositAmount;
        public TextBox txtPaymentAmount;
        public TextBox txtExchangeAmount;
        public TextBox txtDepositSearch;
        public TextBox txtExchangeSearch;
        public TextBox txtSearchRate;
        public TextBox txtDebtSearch;
        
        // ComboBoxes
        public ComboBox cboCurrency;
        public ComboBox cboDepositCurrency;
        public ComboBox cboPaymentCurrency;
        public ComboBox cboFromCurrency;
        public ComboBox cboToCurrency;
        
        // Buttons
        public Button btnAddDebt;
        public Button btnDeposit;
        public Button btnPayDebt;
        public Button btnExchange;
        public Button btnRefreshRates;
        
        // Labels
        public Label lblExchangeRate;

        private void InitializeComponent()
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
            txtSearchRate = new TextBox();
            txtDepositSearch = new TextBox();
            txtExchangeSearch = new TextBox();
            txtDebtSearch = new TextBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dgvDebts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvBalances).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRates).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
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
            dgvRates.AllowUserToDeleteRows = false;
            dgvRates.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRates.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRates.Location = new Point(1009, 92);
            dgvRates.Name = "dgvRates";
            dgvRates.ReadOnly = true;
            dgvRates.RowHeadersWidth = 51;
            dgvRates.Size = new Size(348, 364);
            dgvRates.TabIndex = 2;
            // 
            // txtDepositAmount
            // 
            txtDepositAmount.Location = new Point(337, 80);
            txtDepositAmount.Name = "txtDepositAmount";
            txtDepositAmount.PlaceholderText = "Yüklenecek Miktar";
            txtDepositAmount.Size = new Size(200, 27);
            txtDepositAmount.TabIndex = 7;
            // 
            // cboDepositCurrency
            // 
            cboDepositCurrency.Location = new Point(337, 109);
            cboDepositCurrency.Name = "cboDepositCurrency";
            cboDepositCurrency.Size = new Size(200, 28);
            cboDepositCurrency.TabIndex = 21;
            // 
            // btnDeposit
            // 
            btnDeposit.Location = new Point(337, 138);
            btnDeposit.Name = "btnDeposit";
            btnDeposit.Size = new Size(200, 30);
            btnDeposit.TabIndex = 9;
            btnDeposit.Text = "Para Yükle";
            // 
            // txtBorrowerUserId
            // 
            txtBorrowerUserId.Location = new Point(254, 51);
            txtBorrowerUserId.Name = "txtBorrowerUserId";
            txtBorrowerUserId.PlaceholderText = "Borçlu ID";
            txtBorrowerUserId.Size = new Size(200, 27);
            txtBorrowerUserId.TabIndex = 3;
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(254, 80);
            txtAmount.Name = "txtAmount";
            txtAmount.PlaceholderText = "Miktar";
            txtAmount.Size = new Size(200, 27);
            txtAmount.TabIndex = 4;
            // 
            // cboCurrency
            // 
            cboCurrency.Items.AddRange(new object[] { "USD", "EUR", "TRY" });
            cboCurrency.Location = new Point(254, 109);
            cboCurrency.Name = "cboCurrency";
            cboCurrency.Size = new Size(200, 28);
            cboCurrency.TabIndex = 5;
            // 
            // btnAddDebt
            // 
            btnAddDebt.Location = new Point(254, 138);
            btnAddDebt.Name = "btnAddDebt";
            btnAddDebt.Size = new Size(200, 30);
            btnAddDebt.TabIndex = 6;
            btnAddDebt.Text = "Borç Ekle";
            btnAddDebt.Click += btnAddDebt_Click_1;
            // 
            // btnRefreshRates
            // 
            btnRefreshRates.Location = new Point(1009, 12);
            btnRefreshRates.Name = "btnRefreshRates";
            btnRefreshRates.Size = new Size(151, 30);
            btnRefreshRates.TabIndex = 10;
            btnRefreshRates.Text = "Sayfayı Yenile";
            // 
            // txtPaymentAmount
            // 
            txtPaymentAmount.Location = new Point(7, 80);
            txtPaymentAmount.Name = "txtPaymentAmount";
            txtPaymentAmount.PlaceholderText = "Ödenecek Miktar";
            txtPaymentAmount.Size = new Size(200, 27);
            txtPaymentAmount.TabIndex = 11;
            // 
            // cboPaymentCurrency
            // 
            cboPaymentCurrency.Items.AddRange(new object[] { "USD", "EUR", "TRY" });
            cboPaymentCurrency.Location = new Point(7, 109);
            cboPaymentCurrency.Name = "cboPaymentCurrency";
            cboPaymentCurrency.Size = new Size(200, 28);
            cboPaymentCurrency.TabIndex = 12;
            // 
            // btnPayDebt
            // 
            btnPayDebt.Location = new Point(7, 138);
            btnPayDebt.Name = "btnPayDebt";
            btnPayDebt.Size = new Size(200, 30);
            btnPayDebt.TabIndex = 13;
            btnPayDebt.Text = "Borç Öde";
            // 
            // cboFromCurrency
            // 
            cboFromCurrency.Items.AddRange(new object[] { "USD", "EUR", "TRY" });
            cboFromCurrency.Location = new Point(8, 108);
            cboFromCurrency.Name = "cboFromCurrency";
            cboFromCurrency.Size = new Size(120, 28);
            cboFromCurrency.TabIndex = 15;
            // 
            // cboToCurrency
            // 
            cboToCurrency.Items.AddRange(new object[] { "USD", "EUR", "TRY" });
            cboToCurrency.Location = new Point(179, 108);
            cboToCurrency.Name = "cboToCurrency";
            cboToCurrency.Size = new Size(120, 28);
            cboToCurrency.TabIndex = 16;
            // 
            // txtExchangeAmount
            // 
            txtExchangeAmount.Location = new Point(8, 138);
            txtExchangeAmount.Name = "txtExchangeAmount";
            txtExchangeAmount.PlaceholderText = "Miktar";
            txtExchangeAmount.Size = new Size(120, 27);
            txtExchangeAmount.TabIndex = 17;
            // 
            // btnExchange
            // 
            btnExchange.Location = new Point(179, 138);
            btnExchange.Name = "btnExchange";
            btnExchange.Size = new Size(120, 30);
            btnExchange.TabIndex = 18;
            btnExchange.Text = "Döviz Değiştir";
            // 
            // lblExchangeRate
            // 
            lblExchangeRate.AutoSize = true;
            lblExchangeRate.Location = new Point(136, 111);
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
            // txtSearchRate
            // 
            txtSearchRate.Location = new Point(1157, 59);
            txtSearchRate.Name = "txtSearchRate";
            txtSearchRate.PlaceholderText = "Döviz kodu girin (örn. USD)";
            txtSearchRate.Size = new Size(200, 27);
            txtSearchRate.TabIndex = 20;
            txtSearchRate.KeyDown += TxtSearchRate_KeyDown;
            // 
            // txtDepositSearch
            // 
            txtDepositSearch.Location = new Point(372, 47);
            txtDepositSearch.Name = "txtDepositSearch";
            txtDepositSearch.PlaceholderText = "Döviz kodu ara...";
            txtDepositSearch.Size = new Size(127, 27);
            txtDepositSearch.TabIndex = 22;
            // 
            // txtExchangeSearch
            // 
            txtExchangeSearch.Location = new Point(179, 73);
            txtExchangeSearch.Name = "txtExchangeSearch";
            txtExchangeSearch.PlaceholderText = "Döviz kodu ara...";
            txtExchangeSearch.Size = new Size(120, 27);
            txtExchangeSearch.TabIndex = 23;
            // 
            // txtDebtSearch
            // 
            txtDebtSearch.Location = new Point(284, 18);
            txtDebtSearch.Name = "txtDebtSearch";
            txtDebtSearch.PlaceholderText = "Döviz kodu ara...";
            txtDebtSearch.Size = new Size(119, 27);
            txtDebtSearch.TabIndex = 24;
            txtDebtSearch.TextChanged += TxtDebtSearch_TextChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cboPaymentCurrency);
            groupBox1.Controls.Add(btnPayDebt);
            groupBox1.Controls.Add(txtPaymentAmount);
            groupBox1.Controls.Add(txtBorrowerUserId);
            groupBox1.Controls.Add(btnAddDebt);
            groupBox1.Controls.Add(cboCurrency);
            groupBox1.Controls.Add(txtAmount);
            groupBox1.Controls.Add(txtDebtSearch);
            groupBox1.Location = new Point(715, 462);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(468, 174);
            groupBox1.TabIndex = 25;
            groupBox1.TabStop = false;
            groupBox1.Text = "Defter";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtExchangeSearch);
            groupBox2.Controls.Add(lblExchangeRate);
            groupBox2.Controls.Add(btnExchange);
            groupBox2.Controls.Add(txtExchangeAmount);
            groupBox2.Controls.Add(cboToCurrency);
            groupBox2.Controls.Add(cboFromCurrency);
            groupBox2.Controls.Add(txtDepositSearch);
            groupBox2.Controls.Add(btnDeposit);
            groupBox2.Controls.Add(txtDepositAmount);
            groupBox2.Controls.Add(cboDepositCurrency);
            groupBox2.Location = new Point(166, 462);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(543, 174);
            groupBox2.TabIndex = 26;
            groupBox2.TabStop = false;
            groupBox2.Text = "Bakiye İşlemleri";
            // 
            // UserDashboardForm
            // 
            ClientSize = new Size(1378, 671);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Controls.Add(dgvDebts);
            Controls.Add(dgvBalances);
            Controls.Add(dgvRates);
            Controls.Add(btnRefreshRates);
            Controls.Add(exchangeLabel);
            Controls.Add(depositLabel);
            Controls.Add(txtSearchRate);
            Name = "UserDashboardForm";
            Text = "ForexWise - Cüzdan";
            Load += UserDashboardForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDebts).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvBalances).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRates).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
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
        private GroupBox groupBox1;
        private GroupBox groupBox2;
    }
}