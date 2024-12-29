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
            groupBox1 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dgvCurrencies).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvCurrencies
            // 
            dgvCurrencies.AllowUserToAddRows = false;
            dgvCurrencies.AllowUserToDeleteRows = false;
            dgvCurrencies.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCurrencies.Location = new Point(12, 48);
            dgvCurrencies.Margin = new Padding(3, 4, 3, 4);
            dgvCurrencies.Name = "dgvCurrencies";
            dgvCurrencies.ReadOnly = true;
            dgvCurrencies.RowHeadersWidth = 51;
            dgvCurrencies.RowTemplate.Height = 25;
            dgvCurrencies.Size = new Size(600, 395);
            dgvCurrencies.TabIndex = 0;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(618, 48);
            btnRefresh.Margin = new Padding(3, 4, 3, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(134, 36);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Yenile";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // searchTextBox
            // 
            searchTextBox.Location = new Point(12, 13);
            searchTextBox.Margin = new Padding(3, 4, 3, 4);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "Döviz kodu girin (örn. USD)";
            searchTextBox.Size = new Size(274, 27);
            searchTextBox.TabIndex = 2;
            searchTextBox.KeyDown += SearchTextBox_KeyDown;
            // 
            // btnProfile
            // 
            btnProfile.Location = new Point(767, 372);
            btnProfile.Name = "btnProfile";
            btnProfile.Size = new Size(156, 71);
            btnProfile.TabIndex = 3;
            btnProfile.Text = "Cüzdan İşlemleri";
            btnProfile.Visible = false;
            btnProfile.Click += btnProfile_Click;
            // 
            // lblUserInfo
            // 
            lblUserInfo.AutoSize = true;
            lblUserInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUserInfo.ForeColor = Color.FromArgb(0, 102, 204);
            lblUserInfo.Location = new Point(292, 16);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(0, 23);
            lblUserInfo.TabIndex = 6;
            lblUserInfo.Visible = false;
            // 
            // pnlUserBalances
            // 
            pnlUserBalances.BorderStyle = BorderStyle.FixedSingle;
            pnlUserBalances.Location = new Point(45, 26);
            pnlUserBalances.Name = "pnlUserBalances";
            pnlUserBalances.Size = new Size(227, 243);
            pnlUserBalances.TabIndex = 5;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(pnlUserBalances);
            groupBox1.Location = new Point(648, 91);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(278, 275);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Cüzdanım";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(938, 470);
            Controls.Add(groupBox1);
            Controls.Add(searchTextBox);
            Controls.Add(btnRefresh);
            Controls.Add(dgvCurrencies);
            Controls.Add(btnProfile);
            Controls.Add(lblUserInfo);
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            Text = "ForexWise - Döviz Takip ";
            ((System.ComponentModel.ISupportInitialize)dgvCurrencies).EndInit();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

            // Form ana özellikleri
            this.BackColor = Color.FromArgb(250, 250, 250); // Beyaza yakın arka plan
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);

            // DataGridView stil ayarları
            dgvCurrencies.BackgroundColor = Color.White;
            dgvCurrencies.GridColor = Color.FromArgb(224, 224, 224); // Açık gri
            dgvCurrencies.DefaultCellStyle.BackColor = Color.White;
            dgvCurrencies.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215); // Windows mavi
            dgvCurrencies.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvCurrencies.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvCurrencies.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(70, 70, 70);
            dgvCurrencies.EnableHeadersVisualStyles = false;
            dgvCurrencies.BorderStyle = BorderStyle.None;

            // Butonlar için stil
            btnRefresh.BackColor = Color.FromArgb(0, 120, 215);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            btnRefresh.Cursor = Cursors.Hand;

            btnProfile.BackColor = Color.FromArgb(0, 120, 215);
            btnProfile.ForeColor = Color.White;
            btnProfile.FlatStyle = FlatStyle.Flat;
            btnProfile.FlatAppearance.BorderSize = 0;
            btnProfile.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            btnProfile.Cursor = Cursors.Hand;

            // Arama kutusu stil ayarları
            searchTextBox.BackColor = Color.White;
            searchTextBox.BorderStyle = BorderStyle.FixedSingle;
            searchTextBox.Font = new Font("Segoe UI", 10F);

            // GroupBox stil ayarları
            groupBox1.BackColor = Color.White;
            groupBox1.ForeColor = Color.FromArgb(70, 70, 70);
            groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            // Panel stil ayarları
            pnlUserBalances.BackColor = Color.White;
            pnlUserBalances.BorderStyle = BorderStyle.FixedSingle;

            // Kullanıcı bilgi etiketi stil ayarları
            lblUserInfo.ForeColor = Color.FromArgb(0, 120, 215);
            lblUserInfo.Font = new Font("Segoe UI", 11F, FontStyle.Regular);

            // Buton hover efektleri
            btnRefresh.MouseEnter += (s, e) => {
                btnRefresh.BackColor = Color.FromArgb(0, 102, 204);
            };
            btnRefresh.MouseLeave += (s, e) => {
                btnRefresh.BackColor = Color.FromArgb(0, 120, 215);
            };

            btnProfile.MouseEnter += (s, e) => {
                btnProfile.BackColor = Color.FromArgb(0, 102, 204);
            };
            btnProfile.MouseLeave += (s, e) => {
                btnProfile.BackColor = Color.FromArgb(0, 120, 215);
            };

            // Form boyut ve konum ayarları
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCurrencies;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button btnProfile;
        private Label lblUserInfo;
        private Panel pnlUserBalances;
        private GroupBox groupBox1;
    }
}
