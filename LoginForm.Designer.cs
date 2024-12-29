partial class LoginForm
{
    private void InitializeComponent()
    {
        // Başlık Label'ı ekle
        Label lblTitle = new Label
        {
            Text = "ForexWise",
            Location = new Point(75, 20),
            Size = new Size(250, 35),
            Font = new Font("Segoe UI", 20F, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 120, 215),
            TextAlign = ContentAlignment.MiddleCenter
        };
        this.Controls.Add(lblTitle);

        txtUserId = new TextBox();
        txtPassword = new TextBox();
        btnLogin = new Button();
        btnRegister = new Button();
        SuspendLayout();
        // 
        // txtUserId
        // 
        txtUserId.Location = new Point(75, 80);
        txtUserId.Name = "txtUserId";
        txtUserId.PlaceholderText = "Kullanıcı ID";
        txtUserId.Size = new Size(200, 27);
        txtUserId.TabIndex = 0;
        // 
        // txtPassword
        // 
        txtPassword.Location = new Point(75, 130);
        txtPassword.Name = "txtPassword";
        txtPassword.PasswordChar = '*';
        txtPassword.PlaceholderText = "Şifre";
        txtPassword.Size = new Size(200, 27);
        txtPassword.TabIndex = 1;
        // 
        // btnLogin
        // 
        btnLogin.Location = new Point(75, 180);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(100, 30);
        btnLogin.TabIndex = 2;
        btnLogin.Text = "Giriş";
        btnLogin.Click += btnLogin_Click;
        // 
        // btnRegister
        // 
        btnRegister.Location = new Point(75, 230);
        btnRegister.Name = "btnRegister";
        btnRegister.Size = new Size(100, 30);
        btnRegister.TabIndex = 3;
        btnRegister.Text = "Kayıt Ol";
        btnRegister.Click += btnRegister_Click;
        // 
        // LoginForm
        // 
        ClientSize = new Size(400, 320);
        Controls.Add(txtUserId);
        Controls.Add(txtPassword);
        Controls.Add(btnLogin);
        Controls.Add(btnRegister);
        Name = "LoginForm";
        Text = "ForexWise - Giriş";
        ResumeLayout(false);
        PerformLayout();

        // Form stil ayarları
        this.BackColor = Color.FromArgb(250, 250, 250);
        this.Font = new Font("Segoe UI", 10F);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.StartPosition = FormStartPosition.CenterScreen;

        // TextBox stil ayarları
        var textBoxes = new[] { txtUserId, txtPassword };
        foreach (var textBox in textBoxes)
        {
            textBox.BackColor = Color.White;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = new Font("Segoe UI", 11F);
            textBox.Size = new Size(250, 30);
        }

        // Button stil ayarları
        var buttons = new[] { btnLogin, btnRegister };
        foreach (var button in buttons)
        {
            button.BackColor = Color.FromArgb(0, 120, 215);
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = new Font("Segoe UI", 10F);
            button.Size = new Size(250, 40);
            button.Cursor = Cursors.Hand;

            // Hover efektleri
            button.MouseEnter += (s, e) => {
                button.BackColor = Color.FromArgb(0, 102, 204);
            };
            button.MouseLeave += (s, e) => {
                button.BackColor = Color.FromArgb(0, 120, 215);
            };
        }
    }

    private TextBox txtUserId;
    private TextBox txtPassword;
    private Button btnLogin;
    private Button btnRegister;
} 