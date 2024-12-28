partial class LoginForm
{
    private void InitializeComponent()
    {
        txtUserId = new TextBox();
        txtPassword = new TextBox();
        btnLogin = new Button();
        btnRegister = new Button();
        SuspendLayout();
        // 
        // txtUserId
        // 
        txtUserId.Location = new Point(100, 50);
        txtUserId.Name = "txtUserId";
        txtUserId.PlaceholderText = "Kullanıcı ID";
        txtUserId.Size = new Size(200, 27);
        txtUserId.TabIndex = 0;
        // 
        // txtPassword
        // 
        txtPassword.Location = new Point(100, 90);
        txtPassword.Name = "txtPassword";
        txtPassword.PasswordChar = '*';
        txtPassword.PlaceholderText = "Şifre";
        txtPassword.Size = new Size(200, 27);
        txtPassword.TabIndex = 1;
        // 
        // btnLogin
        // 
        btnLogin.Location = new Point(150, 130);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(100, 30);
        btnLogin.TabIndex = 2;
        btnLogin.Text = "Giriş";
        btnLogin.Click += btnLogin_Click;
        // 
        // btnRegister
        // 
        btnRegister.Location = new Point(150, 170);
        btnRegister.Name = "btnRegister";
        btnRegister.Size = new Size(100, 30);
        btnRegister.TabIndex = 3;
        btnRegister.Text = "Kayıt Ol";
        btnRegister.Click += btnRegister_Click;
        // 
        // LoginForm
        // 
        ClientSize = new Size(400, 223);
        Controls.Add(txtUserId);
        Controls.Add(txtPassword);
        Controls.Add(btnLogin);
        Controls.Add(btnRegister);
        Name = "LoginForm";
        Text = "ForexWise - Giriş";
        ResumeLayout(false);
        PerformLayout();
    }

    private TextBox txtUserId;
    private TextBox txtPassword;
    private Button btnLogin;
    private Button btnRegister;
} 