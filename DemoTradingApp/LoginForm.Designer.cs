namespace DemoTradingApp
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new Krypton.Toolkit.KryptonPanel();
            this.pnlForgotPassword = new System.Windows.Forms.Panel();
            this.linkBackToLogin = new Krypton.Toolkit.KryptonLinkLabel();
            this.pnlResetPassword = new System.Windows.Forms.Panel();
            this.chkShowNewPassword = new Krypton.Toolkit.KryptonCheckBox();
            this.btnResetPassword = new Krypton.Toolkit.KryptonButton();
            this.txtNewPasswordConfirm = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel4 = new Krypton.Toolkit.KryptonLabel();
            this.txtNewPassword = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            this.txtVerificationCode = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            this.btnSendCode = new Krypton.Toolkit.KryptonButton();
            this.txtForgotEmail = new Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.pnlRegister = new System.Windows.Forms.Panel();
            this.chkShowRegisterPassword = new Krypton.Toolkit.KryptonCheckBox();
            this.linkLogin = new Krypton.Toolkit.KryptonLinkLabel();
            this.btnRegister = new Krypton.Toolkit.KryptonButton();
            this.txtRegisterConfirmPassword = new Krypton.Toolkit.KryptonTextBox();
            this.label5 = new Krypton.Toolkit.KryptonLabel();
            this.txtRegisterPassword = new Krypton.Toolkit.KryptonTextBox();
            this.label6 = new Krypton.Toolkit.KryptonLabel();
            this.txtRegisterEmail = new Krypton.Toolkit.KryptonTextBox();
            this.label4 = new Krypton.Toolkit.KryptonLabel();
            this.txtRegisterUsername = new Krypton.Toolkit.KryptonTextBox();
            this.label3 = new Krypton.Toolkit.KryptonLabel();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.chkShowLoginPassword = new Krypton.Toolkit.KryptonCheckBox();
            this.linkForgotPassword = new Krypton.Toolkit.KryptonLinkLabel();
            this.linkRegister = new Krypton.Toolkit.KryptonLinkLabel();
            this.btnLogin = new Krypton.Toolkit.KryptonButton();
            this.txtLoginPassword = new Krypton.Toolkit.KryptonTextBox();
            this.label2 = new Krypton.Toolkit.KryptonLabel();
            this.txtLoginUsername = new Krypton.Toolkit.KryptonTextBox();
            this.label1 = new Krypton.Toolkit.KryptonLabel();
            this.lblTitle = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.pnlForgotPassword.SuspendLayout();
            this.pnlResetPassword.SuspendLayout();
            this.pnlRegister.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.GlobalPaletteMode = Krypton.Toolkit.PaletteMode.SparkleBlue;
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.pnlForgotPassword);
            this.kryptonPanel.Controls.Add(this.pnlRegister);
            this.kryptonPanel.Controls.Add(this.pnlLogin);
            this.kryptonPanel.Controls.Add(this.lblTitle);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(498, 635);
            this.kryptonPanel.TabIndex = 0;
            // 
            // pnlForgotPassword
            // 
            this.pnlForgotPassword.BackColor = System.Drawing.Color.Transparent;
            this.pnlForgotPassword.Controls.Add(this.linkBackToLogin);
            this.pnlForgotPassword.Controls.Add(this.pnlResetPassword);
            this.pnlForgotPassword.Controls.Add(this.btnSendCode);
            this.pnlForgotPassword.Controls.Add(this.txtForgotEmail);
            this.pnlForgotPassword.Controls.Add(this.kryptonLabel1);
            this.pnlForgotPassword.Location = new System.Drawing.Point(69, 74);
            this.pnlForgotPassword.Name = "pnlForgotPassword";
            this.pnlForgotPassword.Size = new System.Drawing.Size(360, 525);
            this.pnlForgotPassword.TabIndex = 4;
            // 
            // linkBackToLogin
            // 
            this.linkBackToLogin.Location = new System.Drawing.Point(7, 480);
            this.linkBackToLogin.Name = "linkBackToLogin";
            this.linkBackToLogin.Size = new System.Drawing.Size(91, 24);
            this.linkBackToLogin.TabIndex = 4;
            this.linkBackToLogin.Values.Text = "< Giriş Ekranı";
            this.linkBackToLogin.Click += new System.EventHandler(this.linkBackToLogin_LinkClicked);
            // 
            // pnlResetPassword
            // 
            this.pnlResetPassword.BackColor = System.Drawing.Color.Transparent;
            this.pnlResetPassword.Controls.Add(this.chkShowNewPassword);
            this.pnlResetPassword.Controls.Add(this.btnResetPassword);
            this.pnlResetPassword.Controls.Add(this.txtNewPasswordConfirm);
            this.pnlResetPassword.Controls.Add(this.kryptonLabel4);
            this.pnlResetPassword.Controls.Add(this.txtNewPassword);
            this.pnlResetPassword.Controls.Add(this.kryptonLabel3);
            this.pnlResetPassword.Controls.Add(this.txtVerificationCode);
            this.pnlResetPassword.Controls.Add(this.kryptonLabel2);
            this.pnlResetPassword.Location = new System.Drawing.Point(0, 150);
            this.pnlResetPassword.Name = "pnlResetPassword";
            this.pnlResetPassword.Size = new System.Drawing.Size(360, 312);
            this.pnlResetPassword.TabIndex = 3;
            this.pnlResetPassword.Visible = false;
            // 
            // chkShowNewPassword
            // 
            this.chkShowNewPassword.Location = new System.Drawing.Point(7, 204);
            this.chkShowNewPassword.Name = "chkShowNewPassword";
            this.chkShowNewPassword.Size = new System.Drawing.Size(125, 24);
            this.chkShowNewPassword.TabIndex = 13;
            this.chkShowNewPassword.Values.Text = "Şifreleri Göster";
            this.chkShowNewPassword.CheckedChanged += new System.EventHandler(this.chkShowNewPassword_CheckedChanged);
            // 
            // btnResetPassword
            // 
            this.btnResetPassword.Location = new System.Drawing.Point(7, 240);
            this.btnResetPassword.Name = "btnResetPassword";
            this.btnResetPassword.Size = new System.Drawing.Size(347, 42);
            this.btnResetPassword.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnResetPassword.TabIndex = 3;
            this.btnResetPassword.Values.Text = "Şifreyi Sıfırla";
            this.btnResetPassword.Click += new System.EventHandler(this.btnResetPassword_Click);
            // 
            // txtNewPasswordConfirm
            // 
            this.txtNewPasswordConfirm.Location = new System.Drawing.Point(3, 165);
            this.txtNewPasswordConfirm.Name = "txtNewPasswordConfirm";
            this.txtNewPasswordConfirm.PasswordChar = '●';
            this.txtNewPasswordConfirm.Size = new System.Drawing.Size(347, 31);
            this.txtNewPasswordConfirm.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtNewPasswordConfirm.TabIndex = 2;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(7, 135);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(122, 24);
            this.kryptonLabel4.TabIndex = 12;
            this.kryptonLabel4.Values.Text = "Yeni Şifre Tekrar";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(3, 95);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '●';
            this.txtNewPassword.Size = new System.Drawing.Size(347, 31);
            this.txtNewPassword.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtNewPassword.TabIndex = 1;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(7, 65);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(75, 24);
            this.kryptonLabel3.TabIndex = 10;
            this.kryptonLabel3.Values.Text = "Yeni Şifre";
            // 
            // txtVerificationCode
            // 
            this.txtVerificationCode.Location = new System.Drawing.Point(3, 25);
            this.txtVerificationCode.Name = "txtVerificationCode";
            this.txtVerificationCode.Size = new System.Drawing.Size(347, 31);
            this.txtVerificationCode.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtVerificationCode.TabIndex = 0;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(7, -5);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(127, 24);
            this.kryptonLabel2.TabIndex = 8;
            this.kryptonLabel2.Values.Text = "Doğrulama Kodu";
            // 
            // btnSendCode
            // 
            this.btnSendCode.Location = new System.Drawing.Point(7, 75);
            this.btnSendCode.Name = "btnSendCode";
            this.btnSendCode.Size = new System.Drawing.Size(347, 42);
            this.btnSendCode.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnSendCode.TabIndex = 1;
            this.btnSendCode.Values.Text = "Doğrulama Kodu Gönder";
            this.btnSendCode.Click += new System.EventHandler(this.btnSendCode_Click);
            // 
            // txtForgotEmail
            // 
            this.txtForgotEmail.Location = new System.Drawing.Point(7, 34);
            this.txtForgotEmail.Name = "txtForgotEmail";
            this.txtForgotEmail.Size = new System.Drawing.Size(347, 31);
            this.txtForgotEmail.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtForgotEmail.TabIndex = 0;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(7, 4);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(174, 24);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Kayıtlı E-Posta Adresiniz";
            // 
            // pnlRegister
            // 
            this.pnlRegister.BackColor = System.Drawing.Color.Transparent;
            this.pnlRegister.Controls.Add(this.chkShowRegisterPassword);
            this.pnlRegister.Controls.Add(this.linkLogin);
            this.pnlRegister.Controls.Add(this.btnRegister);
            this.pnlRegister.Controls.Add(this.txtRegisterConfirmPassword);
            this.pnlRegister.Controls.Add(this.label5);
            this.pnlRegister.Controls.Add(this.txtRegisterPassword);
            this.pnlRegister.Controls.Add(this.label6);
            this.pnlRegister.Controls.Add(this.txtRegisterEmail);
            this.pnlRegister.Controls.Add(this.label4);
            this.pnlRegister.Controls.Add(this.txtRegisterUsername);
            this.pnlRegister.Controls.Add(this.label3);
            this.pnlRegister.Location = new System.Drawing.Point(69, 74);
            this.pnlRegister.Name = "pnlRegister";
            this.pnlRegister.Size = new System.Drawing.Size(360, 450);
            this.pnlRegister.TabIndex = 3;
            // 
            // chkShowRegisterPassword
            // 
            this.chkShowRegisterPassword.Location = new System.Drawing.Point(7, 318);
            this.chkShowRegisterPassword.Name = "chkShowRegisterPassword";
            this.chkShowRegisterPassword.Size = new System.Drawing.Size(125, 24);
            this.chkShowRegisterPassword.TabIndex = 9;
            this.chkShowRegisterPassword.Values.Text = "Şifreleri Göster";
            this.chkShowRegisterPassword.CheckedChanged += new System.EventHandler(this.chkShowRegisterPassword_CheckedChanged);
            // 
            // linkLogin
            // 
            this.linkLogin.Location = new System.Drawing.Point(7, 407);
            this.linkLogin.Name = "linkLogin";
            this.linkLogin.Size = new System.Drawing.Size(247, 24);
            this.linkLogin.TabIndex = 5;
            this.linkLogin.Values.Text = "Zaten bir hesabın var mı? Giriş Yap";
            this.linkLogin.Click += new System.EventHandler(this.linkLogin_LinkClicked);
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(7, 355);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(347, 42);
            this.btnRegister.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnRegister.TabIndex = 4;
            this.btnRegister.Values.Text = "Kayıt Ol";
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // txtRegisterConfirmPassword
            // 
            this.txtRegisterConfirmPassword.Location = new System.Drawing.Point(7, 279);
            this.txtRegisterConfirmPassword.Name = "txtRegisterConfirmPassword";
            this.txtRegisterConfirmPassword.PasswordChar = '●';
            this.txtRegisterConfirmPassword.Size = new System.Drawing.Size(347, 31);
            this.txtRegisterConfirmPassword.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtRegisterConfirmPassword.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(7, 249);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 24);
            this.label5.TabIndex = 8;
            this.label5.Values.Text = "Şifre Tekrar";
            // 
            // txtRegisterPassword
            // 
            this.txtRegisterPassword.Location = new System.Drawing.Point(7, 202);
            this.txtRegisterPassword.Name = "txtRegisterPassword";
            this.txtRegisterPassword.PasswordChar = '●';
            this.txtRegisterPassword.Size = new System.Drawing.Size(347, 31);
            this.txtRegisterPassword.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtRegisterPassword.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(7, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 24);
            this.label6.TabIndex = 6;
            this.label6.Values.Text = "Şifre";
            // 
            // txtRegisterEmail
            // 
            this.txtRegisterEmail.Location = new System.Drawing.Point(7, 122);
            this.txtRegisterEmail.Name = "txtRegisterEmail";
            this.txtRegisterEmail.Size = new System.Drawing.Size(347, 31);
            this.txtRegisterEmail.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtRegisterEmail.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(7, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 24);
            this.label4.TabIndex = 4;
            this.label4.Values.Text = "E-Posta";
            // 
            // txtRegisterUsername
            // 
            this.txtRegisterUsername.Location = new System.Drawing.Point(7, 42);
            this.txtRegisterUsername.Name = "txtRegisterUsername";
            this.txtRegisterUsername.Size = new System.Drawing.Size(347, 31);
            this.txtRegisterUsername.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtRegisterUsername.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 24);
            this.label3.TabIndex = 2;
            this.label3.Values.Text = "Kullanıcı Adı";
            // 
            // pnlLogin
            // 
            this.pnlLogin.BackColor = System.Drawing.Color.Transparent;
            this.pnlLogin.Controls.Add(this.chkShowLoginPassword);
            this.pnlLogin.Controls.Add(this.linkForgotPassword);
            this.pnlLogin.Controls.Add(this.linkRegister);
            this.pnlLogin.Controls.Add(this.btnLogin);
            this.pnlLogin.Controls.Add(this.txtLoginPassword);
            this.pnlLogin.Controls.Add(this.label2);
            this.pnlLogin.Controls.Add(this.txtLoginUsername);
            this.pnlLogin.Controls.Add(this.label1);
            this.pnlLogin.Location = new System.Drawing.Point(69, 74);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(360, 325);
            this.pnlLogin.TabIndex = 2;
            // 
            // chkShowLoginPassword
            // 
            this.chkShowLoginPassword.Location = new System.Drawing.Point(7, 156);
            this.chkShowLoginPassword.Name = "chkShowLoginPassword";
            this.chkShowLoginPassword.Size = new System.Drawing.Size(125, 24);
            this.chkShowLoginPassword.TabIndex = 5;
            this.chkShowLoginPassword.Values.Text = "Şifreyi Göster";
            this.chkShowLoginPassword.CheckedChanged += new System.EventHandler(this.chkShowLoginPassword_CheckedChanged);
            // 
            // linkForgotPassword
            // 
            this.linkForgotPassword.Location = new System.Drawing.Point(228, 287);
            this.linkForgotPassword.Name = "linkForgotPassword";
            this.linkForgotPassword.Size = new System.Drawing.Size(124, 24);
            this.linkForgotPassword.TabIndex = 4;
            this.linkForgotPassword.Values.Text = "Şifremi Unuttum";
            this.linkForgotPassword.Click += new System.EventHandler(this.linkForgotPassword_LinkClicked);
            // 
            // linkRegister
            // 
            this.linkRegister.Location = new System.Drawing.Point(7, 287);
            this.linkRegister.Name = "linkRegister";
            this.linkRegister.Size = new System.Drawing.Size(185, 24);
            this.linkRegister.TabIndex = 3;
            this.linkRegister.Values.Text = "Hesabın yok mu? Kayıt Ol";
            this.linkRegister.Click += new System.EventHandler(this.linkRegister_LinkClicked);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(7, 198);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(347, 42);
            this.btnLogin.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Values.Text = "Giriş Yap";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtLoginPassword
            // 
            this.txtLoginPassword.Location = new System.Drawing.Point(7, 119);
            this.txtLoginPassword.Name = "txtLoginPassword";
            this.txtLoginPassword.PasswordChar = '●';
            this.txtLoginPassword.Size = new System.Drawing.Size(347, 31);
            this.txtLoginPassword.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtLoginPassword.TabIndex = 1;
            this.txtLoginPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLoginPassword_KeyDown);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 24);
            this.label2.TabIndex = 2;
            this.label2.Values.Text = "Şifre";
            // 
            // txtLoginUsername
            // 
            this.txtLoginUsername.Location = new System.Drawing.Point(7, 42);
            this.txtLoginUsername.Name = "txtLoginUsername";
            this.txtLoginUsername.Size = new System.Drawing.Size(347, 31);
            this.txtLoginUsername.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtLoginUsername.TabIndex = 0;
            this.txtLoginUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLoginUsername_KeyDown);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 24);
            this.label1.TabIndex = 0;
            this.label1.Values.Text = "Kullanıcı Adı";
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(474, 46);
            this.lblTitle.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTitle.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Values.Text = "Kullanıcı Girişi";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 635);
            this.Controls.Add(this.kryptonPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Demo Trading - Giriş";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            this.pnlForgotPassword.ResumeLayout(false);
            this.pnlForgotPassword.PerformLayout();
            this.pnlResetPassword.ResumeLayout(false);
            this.pnlResetPassword.PerformLayout();
            this.pnlRegister.ResumeLayout(false);
            this.pnlRegister.PerformLayout();
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.ResumeLayout(false);
        }
        #endregion

        private Krypton.Toolkit.KryptonManager kryptonManager1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel;
        private Krypton.Toolkit.KryptonLabel lblTitle;
        private System.Windows.Forms.Panel pnlLogin;
        private Krypton.Toolkit.KryptonLinkLabel linkForgotPassword;
        private Krypton.Toolkit.KryptonLinkLabel linkRegister;
        private Krypton.Toolkit.KryptonButton btnLogin;
        private Krypton.Toolkit.KryptonTextBox txtLoginPassword;
        private Krypton.Toolkit.KryptonLabel label2;
        private Krypton.Toolkit.KryptonTextBox txtLoginUsername;
        private Krypton.Toolkit.KryptonLabel label1;
        private System.Windows.Forms.Panel pnlRegister;
        private Krypton.Toolkit.KryptonLinkLabel linkLogin;
        private Krypton.Toolkit.KryptonButton btnRegister;
        private Krypton.Toolkit.KryptonTextBox txtRegisterConfirmPassword;
        private Krypton.Toolkit.KryptonLabel label5;
        private Krypton.Toolkit.KryptonTextBox txtRegisterPassword;
        private Krypton.Toolkit.KryptonLabel label6;
        private Krypton.Toolkit.KryptonTextBox txtRegisterEmail;
        private Krypton.Toolkit.KryptonLabel label4;
        private Krypton.Toolkit.KryptonTextBox txtRegisterUsername;
        private Krypton.Toolkit.KryptonLabel label3;
        private System.Windows.Forms.Panel pnlForgotPassword;
        private Krypton.Toolkit.KryptonLinkLabel linkBackToLogin;
        private System.Windows.Forms.Panel pnlResetPassword;
        private Krypton.Toolkit.KryptonButton btnResetPassword;
        private Krypton.Toolkit.KryptonTextBox txtNewPasswordConfirm;
        private Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private Krypton.Toolkit.KryptonTextBox txtNewPassword;
        private Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private Krypton.Toolkit.KryptonTextBox txtVerificationCode;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonButton btnSendCode;
        private Krypton.Toolkit.KryptonTextBox txtForgotEmail;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonCheckBox chkShowLoginPassword;
        private Krypton.Toolkit.KryptonCheckBox chkShowRegisterPassword;
        private Krypton.Toolkit.KryptonCheckBox chkShowNewPassword;
    }
}