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
            this.dgvCurrencies = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrencies)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCurrencies
            // 
            this.dgvCurrencies.AllowUserToAddRows = false;
            this.dgvCurrencies.AllowUserToDeleteRows = false;
            this.dgvCurrencies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCurrencies.Location = new System.Drawing.Point(12, 50);
            this.dgvCurrencies.Name = "dgvCurrencies";
            this.dgvCurrencies.ReadOnly = true;
            this.dgvCurrencies.RowTemplate.Height = 25;
            this.dgvCurrencies.Size = new System.Drawing.Size(760, 380);
            this.dgvCurrencies.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(680, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(92, 32);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Yenile";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(12, 15);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.PlaceholderText = "Döviz kodu girin (örn. USD)";
            this.searchTextBox.Size = new System.Drawing.Size(240, 23);
            this.searchTextBox.TabIndex = 2;
            this.searchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchTextBox_KeyDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 441);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvCurrencies);
            this.Name = "MainForm";
            this.Text = "Döviz Takip Uygulaması";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurrencies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCurrencies;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox searchTextBox;
    }
}
