using System;
using System.Windows.Forms;
using ClassLibrary;
using ForexWise;

public partial class LoginForm : Form
{
    private readonly Database _database;

    public LoginForm()
    {
        InitializeComponent();
        
        // SQL Express için:
        _database = new Database(@"Server=.\SQLEXPRESS;Database=ForexWise;Trusted_Connection=True;TrustServerCertificate=True");
        
        // Veya yerel varsayılan instance için:
        //_database = new Database(@"Server=(localdb)\MSSQLLocalDB;Database=ForexWise;Trusted_Connection=True;TrustServerCertificate=True");
    }

    private async void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            string userId = txtUserId.Text;
            string password = txtPassword.Text;

            if (await _database.ValidateUserAsync(userId, password))
            {
                var mainForm = new MainForm(userId);
                this.Hide();
                mainForm.FormClosed += (s, args) => this.Close();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Geçersiz kullanıcı ID veya şifre!", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Giriş hatası: {ex.Message}", "Hata", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnRegister_Click(object sender, EventArgs e)
    {
        var registerForm = new RegisterForm();
        registerForm.ShowDialog();
    }
} 