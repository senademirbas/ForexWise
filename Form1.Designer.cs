namespace ForexWise
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // Dispose metodu
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dgvCurrencies = new DataGridView();
            btnRefresh = new Button();
            searchTextBox = new TextBox();
            btnProfile = new Button();
            lblUserInfo = new Label();
            pnlUserBalances = new Panel();
            welcomePanel = new Panel();
            lblWelcome = new Label();
            lblUserName = new Label();
            lblDateTime = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvCurrencies).BeginInit();
            SuspendLayout();
            // 
            // dgvCurrencies
            // 
            dgvCurrencies.AllowUserToAddRows = false;
            dgvCurrencies.AllowUserToDeleteRows = false;
            dgvCurrencies.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCurrencies.Location = new Point(14, 67);
            dgvCurrencies.Margin = new Padding(3, 4, 3, 4);
            dgvCurrencies.Name = "dgvCurrencies";
            dgvCurrencies.ReadOnly = true;
            dgvCurrencies.RowHeadersWidth = 51;
            dgvCurrencies.RowTemplate.Height = 25;
            dgvCurrencies.Size = new Size(600, 277);
            dgvCurrencies.TabIndex = 0;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(631, 72);
            btnRefresh.Margin = new Padding(3, 4, 3, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(105, 43);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Yenile";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // searchTextBox
            // 
            searchTextBox.Location = new Point(14, 32);
            searchTextBox.Margin = new Padding(3, 4, 3, 4);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "Döviz kodu girin (örn. USD)";
            searchTextBox.Size = new Size(274, 27);
            searchTextBox.TabIndex = 2;
            searchTextBox.KeyDown += SearchTextBox_KeyDown;
            // 
            // btnProfile
            // 
            btnProfile.Location = new Point(777, 12);
            btnProfile.Name = "btnProfile";
            btnProfile.Size = new Size(210, 99);
            btnProfile.TabIndex = 3;
            btnProfile.Text = "Profil";
            btnProfile.Visible = false;
            btnProfile.Click += btnProfile_Click;
            // 
            // lblUserInfo
            // 
            lblUserInfo.AutoSize = true;
            lblUserInfo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblUserInfo.ForeColor = Color.FromArgb(0, 102, 204);
            lblUserInfo.Location = new Point(777, 236);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(0, 28);
            lblUserInfo.TabIndex = 6;
            lblUserInfo.Visible = false;
            // 
            // pnlUserBalances
            // 
            pnlUserBalances.BorderStyle = BorderStyle.FixedSingle;
            pnlUserBalances.Location = new Point(14, 364);
            pnlUserBalances.Name = "pnlUserBalances";
            pnlUserBalances.Size = new Size(300, 100);
            pnlUserBalances.TabIndex = 5;
            // 
            // welcomePanel
            // 
            welcomePanel.BackColor = Color.FromArgb(240, 240, 240);
            welcomePanel.BorderStyle = BorderStyle.FixedSingle;
            welcomePanel.Location = new Point(319, 12);
            welcomePanel.Name = "welcomePanel";
            welcomePanel.Padding = new Padding(10);
            welcomePanel.Size = new Size(295, 48);
            welcomePanel.TabIndex = 6;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(64, 64, 64);
            lblWelcome.Location = new Point(10, 10);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(100, 23);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Hoş Geldiniz";
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Segoe UI", 10F);
            lblUserName.ForeColor = Color.FromArgb(0, 102, 204);
            lblUserName.Location = new Point(10, 35);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(100, 23);
            lblUserName.TabIndex = 0;
            // 
            // lblDateTime
            // 
            lblDateTime.AutoSize = true;
            lblDateTime.Font = new Font("Segoe UI", 8F);
            lblDateTime.ForeColor = Color.Gray;
            lblDateTime.Location = new Point(10, 55);
            lblDateTime.Name = "lblDateTime";
            lblDateTime.Size = new Size(100, 23);
            lblDateTime.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1161, 664);
            Controls.Add(searchTextBox);
            Controls.Add(btnRefresh);
            Controls.Add(dgvCurrencies);
            Controls.Add(btnProfile);
            Controls.Add(lblUserInfo);
            Controls.Add(pnlUserBalances);
            Controls.Add(welcomePanel);
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            Text = "ForexWise - Döviz Takip ";
            ((System.ComponentModel.ISupportInitialize)dgvCurrencies).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCurrencies;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button btnProfile;
        private Label lblUserInfo;
        private Panel pnlUserBalances;
        private Panel welcomePanel;
        private Label lblWelcome;
        private Label lblUserName;
        private Label lblDateTime;
    }
}
