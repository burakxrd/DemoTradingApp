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
            lblTitle.Text = "Kay�t Ol";
            ShowPanel(pnlRegister);
        }
        private void linkLogin_LinkClicked(object sender, EventArgs e)
        {
            lblTitle.Text = "Kullan�c� Giri�i";
            ShowPanel(pnlLogin);
        }
        private void linkBackToLogin_LinkClicked(object sender, EventArgs e)
        {
            lblTitle.Text = "Kullan�c� Giri�i";
            ShowPanel(pnlLogin);
        }
        private void linkForgotPassword_LinkClicked(object sender, EventArgs e)
        {
            lblTitle.Text = "�ifremi Unuttum";
            pnlResetPassword.Visible = false;
            ShowPanel(pnlForgotPassword);
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLoginUsername.Text) || string.IsNullOrWhiteSpace(txtLoginPassword.Text))
            {
                KryptonMessageBox.Show("Kullan�c� ad� ve �ifre bo� b�rak�lamaz.", "Hata");
                return;
            }
            try
            {
                User? authenticatedUser = DatabaseHelper.AuthenticateUser(txtLoginUsername.Text, txtLoginPassword.Text);

                if (authenticatedUser != null)
                {
                    // Giri� ba�ar�l�ysa, kullan�c�y� sakla, sonucu OK yap ve formu kapat.
                    this.LoggedInUser = authenticatedUser;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    KryptonMessageBox.Show("Kullan�c� ad� veya �ifre hatal�.", "Hata");
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Veritaban� hatas�: " + ex.Message, "Kritik Hata");
            }
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRegisterUsername.Text) || string.IsNullOrWhiteSpace(txtRegisterEmail.Text) || string.IsNullOrWhiteSpace(txtRegisterPassword.Text))
            {
                KryptonMessageBox.Show("T�m alanlar doldurulmal�d�r.", "Eksik Bilgi");
                return;
            }
            if (txtRegisterPassword.Text != txtRegisterConfirmPassword.Text)
            {
                KryptonMessageBox.Show("Girdi�iniz �ifreler uyu�muyor.", "�ifre Hatas�");
                return;
            }
            try
            {
                if (DatabaseHelper.RegisterUser(txtRegisterUsername.Text.Trim(), txtRegisterEmail.Text.Trim(), txtRegisterPassword.Text))
                {
                    KryptonMessageBox.Show("Kay�t ba�ar�l�! L�tfen giri� yap�n�z.", "Ba�ar�l�");
                    lblTitle.Text = "Kullan�c� Giri�i";
                    ShowPanel(pnlLogin);
                }
                else
                {
                    KryptonMessageBox.Show("Bu kullan�c� ad� veya e-posta adresi zaten kullan�l�yor.", "Kay�t Hatas�");
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Kay�t s�ras�nda bir veritaban� hatas� olu�tu: " + ex.Message, "Kritik Hata");
            }
        }
        private void btnSendCode_Click(object sender, EventArgs e)
        {
            string email = txtForgotEmail.Text;
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("L�tfen e-posta adresinizi girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // ===== BA�LANGI�: YEN� VER�TABANI KONTROL� =====
            try
            {
                if (!DatabaseHelper.EmailExists(email))
                {
                    KryptonMessageBox.Show("Bu e-posta adresi sistemde kay�tl� de�il.", "Ge�ersiz E-posta", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                    return; // E-posta bulunamad�ysa metottan ��k, kod g�nderme.
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Veritaban� kontrol� s�ras�nda bir hata olu�tu: " + ex.Message, "Veritaban� Hatas�", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                return;
            }
            // ===== B�T��: YEN� VER�TABANI KONTROL� =====
            // 6 haneli rastgele bir kod olu�tur
            Random random = new Random();
            _verificationCode = random.Next(100000, 999999).ToString();
            _emailForPasswordReset = email;
            // E-posta g�nderimini ayarla (MailKit)
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Demo Trading App", "coinans.contact@gmail.com"));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "�ifre S�f�rlama Kodu";
                message.Body = new TextPart("plain")
                {
                    Text = $"�ifrenizi s�f�rlamak i�in do�rulama kodunuz: {_verificationCode}"
                };
                // SMTP B�LG�LER�N�Z� G�R�N
                using (var client = new SmtpClient())
                {
                     client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                     client.Authenticate("coinans.contact@gmail.com", "dmsf piap zbpu nsnt");
                     client.Send(message);
                     client.Disconnect(true);
                }
                KryptonMessageBox.Show("Do�rulama kodu e-posta adresinize g�nderildi.", "Bilgi", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                pnlResetPassword.Visible = true; // Kod ve yeni �ifre girme panelini g�ster
            }
            catch (Exception ex)
            {
                MessageBox.Show("E-posta g�nderilirken bir hata olu�tu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (txtVerificationCode.Text != _verificationCode)
            {
                KryptonMessageBox.Show("Do�rulama kodu hatal�!", "Do�rulama Hatas�");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text) || txtNewPassword.Text != txtNewPasswordConfirm.Text)
            {
                KryptonMessageBox.Show("Yeni �ifreler uyu�muyor veya bo� b�rak�lamaz.", "�ifre Hatas�");
                return;
            }
            if (_emailForPasswordReset != null)
            {
                try
                {
                    if (DatabaseHelper.UpdatePasswordByEmail(_emailForPasswordReset, txtNewPassword.Text))
                    {
                        KryptonMessageBox.Show("�ifreniz ba�ar�yla g�ncellendi. L�tfen yeni �ifrenizle giri� yap�n.", "Ba�ar�l�");
                        lblTitle.Text = "Kullan�c� Giri�i";
                        ShowPanel(pnlLogin);
                    }
                    else
                    {
                        KryptonMessageBox.Show("�ifre g�ncellenirken bir hata olu�tu.", "Veritaban� Hatas�");
                    }
                }
                catch (Exception ex)
                {
                    KryptonMessageBox.Show("Veritaban� hatas�: " + ex.Message, "Kritik Hata");
                }
            }
            else
            {
                KryptonMessageBox.Show("�ifresi s�f�rlanacak e-posta adresi bulunamad�. L�tfen i�lemi yeniden ba�lat�n.", "��lem Hatas�");
            }
        }
        private void txtLoginUsername_KeyDown(object sender, KeyEventArgs e)
        {
            // E�er bas�lan tu� Enter ise
            if (e.KeyCode == Keys.Enter)
            {
                // Oda�� �ifre kutusuna ge�ir
                txtLoginPassword.Focus();
                // Windows'un "ding" sesini ��karmas�n� engelle
                e.SuppressKeyPress = true;
            }
        }
        private void chkShowLoginPassword_CheckedChanged(object sender, EventArgs e)
        {
            // Giri� yap panelindeki �ifre kutusunun g�r�n�rl���n� ayarla
            txtLoginPassword.PasswordChar = chkShowLoginPassword.Checked ? '\0' : '*';
        }

        private void chkShowRegisterPassword_CheckedChanged(object sender, EventArgs e)
        {
            // Kay�t ol panelindeki her iki �ifre kutusunun g�r�n�rl���n� ayarla
            char passwordChar = chkShowRegisterPassword.Checked ? '\0' : '*';
            txtRegisterPassword.PasswordChar = passwordChar;
            txtRegisterConfirmPassword.PasswordChar = passwordChar;
        }

        private void chkShowNewPassword_CheckedChanged(object sender, EventArgs e)
        {
            // �ifre s�f�rlama panelindeki her iki �ifre kutusunun g�r�n�rl���n� ayarla
            char passwordChar = chkShowNewPassword.Checked ? '\0' : '*';
            txtNewPassword.PasswordChar = passwordChar;
            txtNewPasswordConfirm.PasswordChar = passwordChar;
        }
        private void txtLoginPassword_KeyDown(object sender, KeyEventArgs e)
        {
            // E�er bas�lan tu� Enter ise
            if (e.KeyCode == Keys.Enter)
            {
                // Giri� Yap butonunun Click olay�n� programatik olarak tetikle
                btnLogin.PerformClick();

                // Windows'un "ding" sesini ��karmas�n� engelle
                e.SuppressKeyPress = true;
            }
        }
    }
}