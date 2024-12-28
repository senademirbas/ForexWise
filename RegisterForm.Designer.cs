partial class RegisterForm
{
    private void InitializeComponent()
    {
        txtUserId = new TextBox();
        txtFullName = new TextBox();
        txtPassword = new TextBox();
        dtBirthDate = new DateTimePicker();
        btnRegister = new Button();
        SuspendLayout();
        // 
        // txtUserId
        // 
        txtUserId.Location = new Point(100, 30);
        txtUserId.Name = "txtUserId";
        txtUserId.PlaceholderText = "Kullanıcı ID";
        txtUserId.Size = new Size(200, 27);
        txtUserId.TabIndex = 0;
        // 
        // txtFullName
        // 
        txtFullName.Location = new Point(100, 70);
        txtFullName.Name = "txtFullName";
        txtFullName.PlaceholderText = "Ad Soyad";
        txtFullName.Size = new Size(200, 27);
        txtFullName.TabIndex = 1;
        // 
        // txtPassword
        // 
        txtPassword.Location = new Point(100, 110);
        txtPassword.Name = "txtPassword";
        txtPassword.PasswordChar = '*';
        txtPassword.PlaceholderText = "Şifre";
        txtPassword.Size = new Size(200, 27);
        txtPassword.TabIndex = 2;
        // 
        // dtBirthDate
        // 
        dtBirthDate.Location = new Point(100, 150);
        dtBirthDate.Name = "dtBirthDate";
        dtBirthDate.Size = new Size(200, 27);
        dtBirthDate.TabIndex = 3;
        // 
        // btnRegister
        // 
        btnRegister.Location = new Point(150, 190);
        btnRegister.Name = "btnRegister";
        btnRegister.Size = new Size(100, 30);
        btnRegister.TabIndex = 4;
        btnRegister.Text = "Kayıt Ol";
        btnRegister.Click += btnRegister_Click;
        // 
        // RegisterForm
        // 
        ClientSize = new Size(400, 250);
        Controls.Add(txtUserId);
        Controls.Add(txtFullName);
        Controls.Add(txtPassword);
        Controls.Add(dtBirthDate);
        Controls.Add(btnRegister);
        Name = "RegisterForm";
        Text = "ForexWise - Kayıt";
        ResumeLayout(false);
        PerformLayout();
    }

    private TextBox txtUserId;
    private TextBox txtFullName;
    private TextBox txtPassword;
    private DateTimePicker dtBirthDate;
    private Button btnRegister;
} 