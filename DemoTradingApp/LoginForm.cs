using System;
using System.Windows.Forms;
using Krypton.Toolkit;
using MailKit.Net.Smtp;
using MimeKit;

namespace DemoTradingApp
{
    public partial class LoginForm : KryptonForm
    {
        private readonly Random _random = new Random();

        /// <summary>
        /// Gets the currently logged in user after successful authentication.
        /// </summary>
        public User? LoggedInUser { get; private set; }
        private string? _verificationCode;
        private string? _emailForPasswordReset;

        /// <summary>
        /// Initializes a new instance of the LoginForm class.
        /// </summary>
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
            lblTitle.Text = Properties.Resources.Register;
            ShowPanel(pnlRegister);
        }

        private void linkLogin_LinkClicked(object sender, EventArgs e)
        {
            lblTitle.Text = Properties.Resources.UserLogin;
            ShowPanel(pnlLogin);
        }

        private void linkBackToLogin_LinkClicked(object sender, EventArgs e)
        {
            lblTitle.Text = Properties.Resources.UserLogin;
            ShowPanel(pnlLogin);
        }

        private void linkForgotPassword_LinkClicked(object sender, EventArgs e)
        {
            lblTitle.Text = Properties.Resources.ForgotPassword;
            pnlResetPassword.Visible = false;
            ShowPanel(pnlForgotPassword);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLoginUsername.Text) || string.IsNullOrWhiteSpace(txtLoginPassword.Text))
            {
                KryptonMessageBox.Show(Properties.Resources.UsernamePasswordRequired, Properties.Resources.Error);
                return;
            }
            try
            {
                User? authenticatedUser = DatabaseHelper.AuthenticateUser(txtLoginUsername.Text, txtLoginPassword.Text);

                if (authenticatedUser != null)
                {
                    this.LoggedInUser = authenticatedUser;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    KryptonMessageBox.Show(Properties.Resources.InvalidUsernamePassword, Properties.Resources.Error);
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(string.Format(Properties.Resources.DatabaseErrorWithMessage, ex.Message), Properties.Resources.CriticalError);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRegisterUsername.Text) || string.IsNullOrWhiteSpace(txtRegisterEmail.Text) || string.IsNullOrWhiteSpace(txtRegisterPassword.Text))
            {
                KryptonMessageBox.Show(Properties.Resources.AllFieldsRequired, Properties.Resources.MissingInfo);
                return;
            }
            if (txtRegisterPassword.Text != txtRegisterConfirmPassword.Text)
            {
                KryptonMessageBox.Show(Properties.Resources.PasswordMismatch, Properties.Resources.PasswordError);
                return;
            }
            try
            {
                if (DatabaseHelper.RegisterUser(txtRegisterUsername.Text.Trim(), txtRegisterEmail.Text.Trim(), txtRegisterPassword.Text))
                {
                    KryptonMessageBox.Show(Properties.Resources.RegistrationSuccessful, Properties.Resources.Success);
                    lblTitle.Text = Properties.Resources.UserLogin;
                    ShowPanel(pnlLogin);
                }
                else
                {
                    KryptonMessageBox.Show(Properties.Resources.UsernameEmailAlreadyExists, Properties.Resources.RegistrationError);
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(string.Format(Properties.Resources.RegistrationDatabaseError, ex.Message), Properties.Resources.CriticalError);
            }
        }

        private void btnSendCode_Click(object sender, EventArgs e)
        {
            string email = txtForgotEmail.Text;
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show(Properties.Resources.PleaseEnterEmail, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (!DatabaseHelper.EmailExists(email))
                {
                    KryptonMessageBox.Show(Properties.Resources.EmailNotRegistered, Properties.Resources.InvalidEmail, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(string.Format(Properties.Resources.DatabaseCheckError, ex.Message), Properties.Resources.DatabaseError, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                return;
            }
            _verificationCode = _random.Next(100000, 999999).ToString();
            _emailForPasswordReset = email;
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Demo Trading App", "YourEmailAddress"));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = Properties.Resources.PasswordResetCode;
                message.Body = new TextPart("plain")
                {
                    Text = string.Format(Properties.Resources.PasswordResetCodeMessage, _verificationCode)
                };
                using (var client = new SmtpClient())
                {
                     client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                     client.Authenticate("YourEmailAddress", "YourAppPassword");
                     client.Send(message);
                     client.Disconnect(true);
                }
                KryptonMessageBox.Show(Properties.Resources.VerificationCodeSent, Properties.Resources.Information, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                pnlResetPassword.Visible = true;
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(string.Format(Properties.Resources.EmailSendError, ex.Message), Properties.Resources.Error, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (txtVerificationCode.Text != _verificationCode)
            {
                KryptonMessageBox.Show(Properties.Resources.InvalidVerificationCode, Properties.Resources.VerificationError);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text) || txtNewPassword.Text != txtNewPasswordConfirm.Text)
            {
                KryptonMessageBox.Show(Properties.Resources.NewPasswordMismatch, Properties.Resources.PasswordError);
                return;
            }
            if (_emailForPasswordReset != null)
            {
                try
                {
                    if (DatabaseHelper.UpdatePasswordByEmail(_emailForPasswordReset, txtNewPassword.Text))
                    {
                        KryptonMessageBox.Show(Properties.Resources.PasswordUpdatedSuccessfully, Properties.Resources.Success);
                        lblTitle.Text = Properties.Resources.UserLogin;
                        ShowPanel(pnlLogin);
                    }
                    else
                    {
                        KryptonMessageBox.Show(Properties.Resources.PasswordUpdateError, Properties.Resources.DatabaseError);
                    }
                }
                catch (Exception ex)
                {
                    KryptonMessageBox.Show(string.Format(Properties.Resources.DatabaseErrorWithMessage, ex.Message), Properties.Resources.CriticalError);
                }
            }
            else
            {
                KryptonMessageBox.Show(Properties.Resources.EmailForResetNotFound, Properties.Resources.OperationError);
            }
        }
        private void txtLoginUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtLoginPassword.Focus();
                e.SuppressKeyPress = true;
            }
        }
        private void txtLoginPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
        private void chkShowLoginPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtLoginPassword.PasswordChar = chkShowLoginPassword.Checked ? '\0' : '*';
        }

        private void chkShowRegisterPassword_CheckedChanged(object sender, EventArgs e)
        {
            char passwordChar = chkShowRegisterPassword.Checked ? '\0' : '*';
            txtRegisterPassword.PasswordChar = passwordChar;
            txtRegisterConfirmPassword.PasswordChar = passwordChar;
        }

        private void chkShowNewPassword_CheckedChanged(object sender, EventArgs e)
        {
            char passwordChar = chkShowNewPassword.Checked ? '\0' : '*';
            txtNewPassword.PasswordChar = passwordChar;
            txtNewPasswordConfirm.PasswordChar = passwordChar;
        }
    }
}