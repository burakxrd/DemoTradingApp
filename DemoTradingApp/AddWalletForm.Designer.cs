namespace DemoTradingApp
{
    partial class AddWalletForm
    {
        private System.ComponentModel.IContainer components = null;

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
            kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            groupDeleteWallet = new Krypton.Toolkit.KryptonGroupBox();
            btnDelete = new Krypton.Toolkit.KryptonButton();
            cmbExistingWallets = new Krypton.Toolkit.KryptonComboBox();
            groupAddWallet = new Krypton.Toolkit.KryptonGroupBox();
            btnConfirm = new Krypton.Toolkit.KryptonButton();
            cmbWalletType = new Krypton.Toolkit.KryptonComboBox();
            kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            txtWalletName = new Krypton.Toolkit.KryptonTextBox();
            kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            btnCancel = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).BeginInit();
            kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)groupDeleteWallet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupDeleteWallet.Panel).BeginInit();
            groupDeleteWallet.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cmbExistingWallets).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupAddWallet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupAddWallet.Panel).BeginInit();
            groupAddWallet.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cmbWalletType).BeginInit();
            SuspendLayout();
            // 
            // kryptonPanel1
            // 
            kryptonPanel1.Controls.Add(groupDeleteWallet);
            kryptonPanel1.Controls.Add(groupAddWallet);
            kryptonPanel1.Controls.Add(btnCancel);
            kryptonPanel1.Dock = DockStyle.Fill;
            kryptonPanel1.Location = new Point(0, 0);
            kryptonPanel1.Margin = new Padding(4, 5, 4, 5);
            kryptonPanel1.Name = "kryptonPanel1";
            kryptonPanel1.Padding = new Padding(13, 15, 13, 15);
            kryptonPanel1.Size = new Size(653, 527);
            kryptonPanel1.TabIndex = 0;
            // 
            // groupDeleteWallet
            // 
            groupDeleteWallet.CaptionStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            groupDeleteWallet.Location = new Point(17, 292);
            groupDeleteWallet.Margin = new Padding(4, 5, 4, 5);
            // 
            // 
            // 
            groupDeleteWallet.Panel.Controls.Add(btnDelete);
            groupDeleteWallet.Panel.Controls.Add(cmbExistingWallets);
            groupDeleteWallet.Size = new Size(611, 154);
            groupDeleteWallet.TabIndex = 8;
            groupDeleteWallet.Values.Heading = "Mevcut Cüzdanı Sil";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(433, 31);
            btnDelete.Margin = new Padding(4, 5, 4, 5);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(147, 46);
            btnDelete.TabIndex = 1;
            btnDelete.Values.DropDownArrowColor = Color.Empty;
            btnDelete.Values.Text = "Seçili Cüzdanı Sil";
            btnDelete.Click += btnDelete_Click;
            // 
            // cmbExistingWallets
            // 
            cmbExistingWallets.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbExistingWallets.DropDownWidth = 290;
            cmbExistingWallets.IntegralHeight = false;
            cmbExistingWallets.Location = new Point(27, 38);
            cmbExistingWallets.Margin = new Padding(4, 5, 4, 5);
            cmbExistingWallets.Name = "cmbExistingWallets";
            cmbExistingWallets.Size = new Size(387, 26);
            cmbExistingWallets.TabIndex = 0;
            // 
            // groupAddWallet
            // 
            groupAddWallet.CaptionStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            groupAddWallet.Location = new Point(17, 20);
            groupAddWallet.Margin = new Padding(4, 5, 4, 5);
            // 
            // 
            // 
            groupAddWallet.Panel.Controls.Add(btnConfirm);
            groupAddWallet.Panel.Controls.Add(cmbWalletType);
            groupAddWallet.Panel.Controls.Add(kryptonLabel2);
            groupAddWallet.Panel.Controls.Add(txtWalletName);
            groupAddWallet.Panel.Controls.Add(kryptonLabel1);
            groupAddWallet.Size = new Size(611, 246);
            groupAddWallet.TabIndex = 7;
            groupAddWallet.Values.Heading = "Yeni Cüzdan Oluştur";
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(250, 130);
            btnConfirm.Margin = new Padding(4, 5, 4, 5);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(160, 62);
            btnConfirm.TabIndex = 9;
            btnConfirm.Values.DropDownArrowColor = Color.Empty;
            btnConfirm.Values.Text = "Cüzdan Oluştur";
            btnConfirm.Click += btnConfirm_Click;
            // 
            // cmbWalletType
            // 
            cmbWalletType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbWalletType.DropDownWidth = 217;
            cmbWalletType.IntegralHeight = false;
            cmbWalletType.Location = new Point(200, 92);
            cmbWalletType.Margin = new Padding(4, 5, 4, 5);
            cmbWalletType.Name = "cmbWalletType";
            cmbWalletType.Size = new Size(289, 26);
            cmbWalletType.TabIndex = 8;
            // 
            // kryptonLabel2
            // 
            kryptonLabel2.Location = new Point(80, 92);
            kryptonLabel2.Margin = new Padding(4, 5, 4, 5);
            kryptonLabel2.Name = "kryptonLabel2";
            kryptonLabel2.Size = new Size(101, 24);
            kryptonLabel2.TabIndex = 7;
            kryptonLabel2.Values.Text = "Cüzdan Türü:";
            // 
            // txtWalletName
            // 
            txtWalletName.Location = new Point(200, 31);
            txtWalletName.Margin = new Padding(4, 5, 4, 5);
            txtWalletName.Name = "txtWalletName";
            txtWalletName.Size = new Size(289, 27);
            txtWalletName.TabIndex = 6;
            // 
            // kryptonLabel1
            // 
            kryptonLabel1.Location = new Point(97, 31);
            kryptonLabel1.Margin = new Padding(4, 5, 4, 5);
            kryptonLabel1.Name = "kryptonLabel1";
            kryptonLabel1.Size = new Size(93, 24);
            kryptonLabel1.TabIndex = 5;
            kryptonLabel1.Values.Text = "Cüzdan Adı:";
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(244, 460);
            btnCancel.Margin = new Padding(4, 5, 4, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(160, 62);
            btnCancel.TabIndex = 6;
            btnCancel.Values.DropDownArrowColor = Color.Empty;
            btnCancel.Values.Text = "Kapat";
            btnCancel.Click += btnCancel_Click;
            // 
            // AddWalletForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(653, 527);
            Controls.Add(kryptonPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddWalletForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cüzdan Yönetimi";
            Load += AddWalletForm_Load;
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).EndInit();
            kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)groupDeleteWallet.Panel).EndInit();
            groupDeleteWallet.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)groupDeleteWallet).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbExistingWallets).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupAddWallet.Panel).EndInit();
            groupAddWallet.Panel.ResumeLayout(false);
            groupAddWallet.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)groupAddWallet).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbWalletType).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Krypton.Toolkit.KryptonGroupBox groupAddWallet;
        private Krypton.Toolkit.KryptonButton btnConfirm;
        private Krypton.Toolkit.KryptonComboBox cmbWalletType;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonTextBox txtWalletName;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonGroupBox groupDeleteWallet;
        private Krypton.Toolkit.KryptonButton btnDelete;
        private Krypton.Toolkit.KryptonComboBox cmbExistingWallets;
    }
}