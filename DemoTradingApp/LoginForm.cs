using System;
using System.Windows.Forms;
using Krypton.Toolkit;
using MailKit.Net.Smtp;
using MimeKit;

namespace DemoTradingApp
{
    public partial class LoginForm : KryptonForm
    {
        public User? LoggedInUser { get; private set; }
        private string? _verificationCode;
        private string? _emailForPasswordReset;
        public LoginForm()
        {
            InitializeComponent();
            kryptonManager1.GlobalPaletteMode = Krypton.Toolkit.PaletteMode.SparkleBlue;
            ShowPanel(pnlLogin);
        }
        private void ShowPanel(Panel panelToShow)
        {
            pnlLogin.Visible = false;
            pnlRegister.Visible = false;
            pnlForgotPassword.Visible = false;
            panelToShow.Visible = true;
            panelToShow.Location = new System.Drawing.Point(
                (kryptonPanel.ClientSize.Width - panelToShow.Width) / 2,
                lblTitle.Bottom + 10);
        }
        private void linkRegister_LinkClicked(object sender, EventArgs e)
        {
            lblTitle.Text = "Kayýt Ol";
            ShowPanel(pnlRegister);
        }
        private void linkLogin_LinkClicked(object sender, EventArgs e)
        {
            lblTitle.Text = "Kullanýcý Giriþi";
            ShowPanel(pnlLogin);
        }
        private void linkBackToLogin_LinkClicked(object sender, EventArgs e)
        {
            lblTitle.Text = "Kullanýcý Giriþi";
            ShowPanel(pnlLogin);
        }
        private void linkForgotPassword_LinkClicked(object sender, EventArgs e)
        {
            lblTitle.Text = "Þifremi Unuttum";
            pnlResetPassword.Visible = false;
            ShowPanel(pnlForgotPassword);
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLoginUsername.Text) || string.IsNullOrWhiteSpace(txtLoginPassword.Text))
            {
                KryptonMessageBox.Show("Kullanýcý adý ve þifre boþ býrakýlamaz.", "Hata");
                return;
            }
            try
            {
                User? authenticatedUser = DatabaseHelper.AuthenticateUser(txtLoginUsername.Text, txtLoginPassword.Text);

                if (authenticatedUser != null)
                {
                    // Giriþ baþarýlýysa, kullanýcýyý sakla, sonucu OK yap ve formu kapat.
                    this.LoggedInUser = authenticatedUser;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    KryptonMessageBox.Show("Kullanýcý adý veya þifre hatalý.", "Hata");
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Veritabaný hatasý: " + ex.Message, "Kritik Hata");
            }
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRegisterUsername.Text) || string.IsNullOrWhiteSpace(txtRegisterEmail.Text) || string.IsNullOrWhiteSpace(txtRegisterPassword.Text))
            {
                KryptonMessageBox.Show("Tüm alanlar doldurulmalýdýr.", "Eksik Bilgi");
                return;
            }
            if (txtRegisterPassword.Text != txtRegisterConfirmPassword.Text)
            {
                KryptonMessageBox.Show("Girdiðiniz þifreler uyuþmuyor.", "Þifre Hatasý");
                return;
            }
            try
            {
                if (DatabaseHelper.RegisterUser(txtRegisterUsername.Text.Trim(), txtRegisterEmail.Text.Trim(), txtRegisterPassword.Text))
                {
                    KryptonMessageBox.Show("Kayýt baþarýlý! Lütfen giriþ yapýnýz.", "Baþarýlý");
                    lblTitle.Text = "Kullanýcý Giriþi";
                    ShowPanel(pnlLogin);
                }
                else
                {
                    KryptonMessageBox.Show("Bu kullanýcý adý veya e-posta adresi zaten kullanýlýyor.", "Kayýt Hatasý");
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Kayýt sýrasýnda bir veritabaný hatasý oluþtu: " + ex.Message, "Kritik Hata");
            }
        }
        private void btnSendCode_Click(object sender, EventArgs e)
        {
            string email = txtForgotEmail.Text;
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Lütfen e-posta adresinizi girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // ===== BAÞLANGIÇ: YENÝ VERÝTABANI KONTROLÜ =====
            try
            {
                if (!DatabaseHelper.EmailExists(email))
                {
                    KryptonMessageBox.Show("Bu e-posta adresi sistemde kayýtlý deðil.", "Geçersiz E-posta", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                    return; // E-posta bulunamadýysa metottan çýk, kod gönderme.
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Veritabaný kontrolü sýrasýnda bir hata oluþtu: " + ex.Message, "Veritabaný Hatasý", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                return;
            }
            // ===== BÝTÝÞ: YENÝ VERÝTABANI KONTROLÜ =====
            // 6 haneli rastgele bir kod oluþtur
            Random random = new Random();
            _verificationCode = random.Next(100000, 999999).ToString();
            _emailForPasswordReset = email;
            // E-posta gönderimini ayarla (MailKit)
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Demo Trading App", "coinans.contact@gmail.com"));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Þifre Sýfýrlama Kodu";
                message.Body = new TextPart("plain")
                {
                    Text = $"Þifrenizi sýfýrlamak için doðrulama kodunuz: {_verificationCode}"
                };
                // SMTP BÝLGÝLERÝNÝZÝ GÝRÝN
                using (var client = new SmtpClient())
                {
                     client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                     client.Authenticate("coinans.contact@gmail.com", "dmsf piap zbpu nsnt");
                     client.Send(message);
                     client.Disconnect(true);
                }
                KryptonMessageBox.Show("Doðrulama kodu e-posta adresinize gönderildi.", "Bilgi", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                pnlResetPassword.Visible = true; // Kod ve yeni þifre girme panelini göster
            }
            catch (Exception ex)
            {
                MessageBox.Show("E-posta gönderilirken bir hata oluþtu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (txtVerificationCode.Text != _verificationCode)
            {
                KryptonMessageBox.Show("Doðrulama kodu hatalý!", "Doðrulama Hatasý");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text) || txtNewPassword.Text != txtNewPasswordConfirm.Text)
            {
                KryptonMessageBox.Show("Yeni þifreler uyuþmuyor veya boþ býrakýlamaz.", "Þifre Hatasý");
                return;
            }
            if (_emailForPasswordReset != null)
            {
                try
                {
                    if (DatabaseHelper.UpdatePasswordByEmail(_emailForPasswordReset, txtNewPassword.Text))
                    {
                        KryptonMessageBox.Show("Þifreniz baþarýyla güncellendi. Lütfen yeni þifrenizle giriþ yapýn.", "Baþarýlý");
                        lblTitle.Text = "Kullanýcý Giriþi";
                        ShowPanel(pnlLogin);
                    }
                    else
                    {
                        KryptonMessageBox.Show("Þifre güncellenirken bir hata oluþtu.", "Veritabaný Hatasý");
                    }
                }
                catch (Exception ex)
                {
                    KryptonMessageBox.Show("Veritabaný hatasý: " + ex.Message, "Kritik Hata");
                }
            }
            else
            {
                KryptonMessageBox.Show("Þifresi sýfýrlanacak e-posta adresi bulunamadý. Lütfen iþlemi yeniden baþlatýn.", "Ýþlem Hatasý");
            }
        }
        private void txtLoginUsername_KeyDown(object sender, KeyEventArgs e)
        {
            // Eðer basýlan tuþ Enter ise
            if (e.KeyCode == Keys.Enter)
            {
                // Odaðý þifre kutusuna geçir
                txtLoginPassword.Focus();
                // Windows'un "ding" sesini çýkarmasýný engelle
                e.SuppressKeyPress = true;
            }
        }
        private void chkShowLoginPassword_CheckedChanged(object sender, EventArgs e)
        {
            // Giriþ yap panelindeki þifre kutusunun görünürlüðünü ayarla
            txtLoginPassword.PasswordChar = chkShowLoginPassword.Checked ? '\0' : '*';
        }

        private void chkShowRegisterPassword_CheckedChanged(object sender, EventArgs e)
        {
            // Kayýt ol panelindeki her iki þifre kutusunun görünürlüðünü ayarla
            char passwordChar = chkShowRegisterPassword.Checked ? '\0' : '*';
            txtRegisterPassword.PasswordChar = passwordChar;
            txtRegisterConfirmPassword.PasswordChar = passwordChar;
        }

        private void chkShowNewPassword_CheckedChanged(object sender, EventArgs e)
        {
            // Þifre sýfýrlama panelindeki her iki þifre kutusunun görünürlüðünü ayarla
            char passwordChar = chkShowNewPassword.Checked ? '\0' : '*';
            txtNewPassword.PasswordChar = passwordChar;
            txtNewPasswordConfirm.PasswordChar = passwordChar;
        }
        private void txtLoginPassword_KeyDown(object sender, KeyEventArgs e)
        {
            // Eðer basýlan tuþ Enter ise
            if (e.KeyCode == Keys.Enter)
            {
                // Giriþ Yap butonunun Click olayýný programatik olarak tetikle
                btnLogin.PerformClick();

                // Windows'un "ding" sesini çýkarmasýný engelle
                e.SuppressKeyPress = true;
            }
        }
    }
}