using System;
using System.Windows.Forms;
using ClassLibrary;
using System.Drawing;
using System.Linq;

public partial class RegisterForm : Form
{
    private readonly Database _database;

    public RegisterForm()
    {
        InitializeComponent();
        _database = new Database(@"Server=.\SQLEXPRESS;Database=ForexWise;Trusted_Connection=True;TrustServerCertificate=True");
        
        // ID TextBox'ına açıklama ekle
        Label lblIdInfo = new Label
        {
            Text = "ID 9 haneli rakamlardan oluşmalıdır",
            Location = new Point(100, 10),
            AutoSize = true
        };
        this.Controls.Add(lblIdInfo);

        // ID doğrulama için event ekle
        txtUserId.TextChanged += (s, e) =>
        {
            if (!string.IsNullOrEmpty(txtUserId.Text) && 
                (!txtUserId.Text.All(char.IsDigit) || txtUserId.Text.Length != 9))
            {
                txtUserId.BackColor = Color.LightPink;
                btnRegister.Enabled = false;
            }
            else
            {
                txtUserId.BackColor = SystemColors.Window;
                btnRegister.Enabled = true;
            }
        };
    }

    private async void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtUserId.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Kullanıcı ID ve şifre zorunludur!", "Hata");
                return;
            }

            if (!txtUserId.Text.All(char.IsDigit) || txtUserId.Text.Length != 9)
            {
                MessageBox.Show("ID 9 haneli rakamlardan oluşmalıdır!", "Hata");
                return;
            }

            await _database.AddUserAsync(
                txtUserId.Text,
                txtFullName.Text,
                dtBirthDate.Value,
                txtPassword.Text
            );

            MessageBox.Show("Kayıt başarılı! Giriş yapabilirsiniz.", "Başarılı");
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Kayıt hatası: {ex.Message}", "Hata");
        }
    }
} 