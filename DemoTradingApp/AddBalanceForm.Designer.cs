namespace DemoTradingApp
{
    partial class AddBalanceForm
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
            groupRemoveBalance = new Krypton.Toolkit.KryptonGroupBox();
            btnRemoveBalance = new Krypton.Toolkit.KryptonButton();
            numAmountToRemove = new Krypton.Toolkit.KryptonNumericUpDown();
            kryptonLabel5 = new Krypton.Toolkit.KryptonLabel();
            cmbAssetToRemove = new Krypton.Toolkit.KryptonComboBox();
            kryptonLabel4 = new Krypton.Toolkit.KryptonLabel();
            groupAddBalance = new Krypton.Toolkit.KryptonGroupBox();
            btnConfirm = new Krypton.Toolkit.KryptonButton();
            cmbCurrency = new Krypton.Toolkit.KryptonComboBox();
            kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            numAmount = new Krypton.Toolkit.KryptonNumericUpDown();
            kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            cmbWallets = new Krypton.Toolkit.KryptonComboBox();
            kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            btnCancel = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).BeginInit();
            kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)groupRemoveBalance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupRemoveBalance.Panel).BeginInit();
            groupRemoveBalance.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cmbAssetToRemove).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupAddBalance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupAddBalance.Panel).BeginInit();
            groupAddBalance.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cmbCurrency).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbWallets).BeginInit();
            SuspendLayout();
            // 
            // kryptonPanel1
            // 
            kryptonPanel1.Controls.Add(groupRemoveBalance);
            kryptonPanel1.Controls.Add(groupAddBalance);
            kryptonPanel1.Controls.Add(btnCancel);
            kryptonPanel1.Dock = DockStyle.Fill;
            kryptonPanel1.Location = new Point(0, 0);
            kryptonPanel1.Margin = new Padding(4, 5, 4, 5);
            kryptonPanel1.Name = "kryptonPanel1";
            kryptonPanel1.Padding = new Padding(13, 15, 13, 15);
            kryptonPanel1.Size = new Size(669, 624);
            kryptonPanel1.TabIndex = 0;
            // 
            // groupRemoveBalance
            // 
            groupRemoveBalance.CaptionStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            groupRemoveBalance.Location = new Point(17, 330);
            groupRemoveBalance.Margin = new Padding(4, 5, 4, 5);
            // 
            // 
            // 
            groupRemoveBalance.Panel.Controls.Add(btnRemoveBalance);
            groupRemoveBalance.Panel.Controls.Add(numAmountToRemove);
            groupRemoveBalance.Panel.Controls.Add(kryptonLabel5);
            groupRemoveBalance.Panel.Controls.Add(cmbAssetToRemove);
            groupRemoveBalance.Panel.Controls.Add(kryptonLabel4);
            groupRemoveBalance.Size = new Size(611, 231);
            groupRemoveBalance.TabIndex = 8;
            groupRemoveBalance.Values.Heading = "Bakiye Çıkar";
            // 
            // btnRemoveBalance
            // 
            btnRemoveBalance.Location = new Point(250, 125);
            btnRemoveBalance.Margin = new Padding(4, 5, 4, 5);
            btnRemoveBalance.Name = "btnRemoveBalance";
            btnRemoveBalance.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Black;
            btnRemoveBalance.Size = new Size(160, 62);
            btnRemoveBalance.TabIndex = 4;
            btnRemoveBalance.Values.DropDownArrowColor = Color.Empty;
            btnRemoveBalance.Values.Text = "Bakiyeyi Çıkar";
            btnRemoveBalance.Click += btnRemoveBalance_Click;
            // 
            // numAmountToRemove
            // 
            numAmountToRemove.AllowDecimals = true;
            numAmountToRemove.DecimalPlaces = 6;
            numAmountToRemove.Increment = new decimal(new int[] { 1, 0, 0, 0 });
            numAmountToRemove.Location = new Point(200, 85);
            numAmountToRemove.Margin = new Padding(4, 5, 4, 5);
            numAmountToRemove.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            numAmountToRemove.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            numAmountToRemove.Name = "numAmountToRemove";
            numAmountToRemove.Size = new Size(289, 26);
            numAmountToRemove.TabIndex = 3;
            numAmountToRemove.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // kryptonLabel5
            // 
            kryptonLabel5.Location = new Point(67, 85);
            kryptonLabel5.Margin = new Padding(4, 5, 4, 5);
            kryptonLabel5.Name = "kryptonLabel5";
            kryptonLabel5.Size = new Size(135, 24);
            kryptonLabel5.TabIndex = 2;
            kryptonLabel5.Values.Text = "Çıkarılacak Miktar:";
            // 
            // cmbAssetToRemove
            // 
            cmbAssetToRemove.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAssetToRemove.DropDownWidth = 217;
            cmbAssetToRemove.IntegralHeight = false;
            cmbAssetToRemove.Location = new Point(200, 31);
            cmbAssetToRemove.Margin = new Padding(4, 5, 4, 5);
            cmbAssetToRemove.Name = "cmbAssetToRemove";
            cmbAssetToRemove.Size = new Size(289, 26);
            cmbAssetToRemove.TabIndex = 1;
            // 
            // kryptonLabel4
            // 
            kryptonLabel4.Location = new Point(10, 31);
            kryptonLabel4.Margin = new Padding(4, 5, 4, 5);
            kryptonLabel4.Name = "kryptonLabel4";
            kryptonLabel4.Size = new Size(195, 24);
            kryptonLabel4.TabIndex = 0;
            kryptonLabel4.Values.Text = "Çıkarılacak Cüzdan - Varlık:";
            // 
            // groupAddBalance
            // 
            groupAddBalance.CaptionStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            groupAddBalance.Location = new Point(17, 20);
            groupAddBalance.Margin = new Padding(4, 5, 4, 5);
            // 
            // 
            // 
            groupAddBalance.Panel.Controls.Add(btnConfirm);
            groupAddBalance.Panel.Controls.Add(cmbCurrency);
            groupAddBalance.Panel.Controls.Add(kryptonLabel3);
            groupAddBalance.Panel.Controls.Add(numAmount);
            groupAddBalance.Panel.Controls.Add(kryptonLabel2);
            groupAddBalance.Panel.Controls.Add(cmbWallets);
            groupAddBalance.Panel.Controls.Add(kryptonLabel1);
            groupAddBalance.Size = new Size(611, 308);
            groupAddBalance.TabIndex = 7;
            groupAddBalance.Values.Heading = "Bakiye Ekle";
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(250, 200);
            btnConfirm.Margin = new Padding(4, 5, 4, 5);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(160, 62);
            btnConfirm.TabIndex = 10;
            btnConfirm.Values.DropDownArrowColor = Color.Empty;
            btnConfirm.Values.Text = "Bakiyeyi Ekle";
            btnConfirm.Click += btnConfirm_Click;
            // 
            // cmbCurrency
            // 
            cmbCurrency.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCurrency.DropDownWidth = 217;
            cmbCurrency.IntegralHeight = false;
            cmbCurrency.Location = new Point(200, 162);
            cmbCurrency.Margin = new Padding(4, 5, 4, 5);
            cmbCurrency.Name = "cmbCurrency";
            cmbCurrency.Size = new Size(289, 26);
            cmbCurrency.TabIndex = 16;
            // 
            // kryptonLabel3
            // 
            kryptonLabel3.Location = new Point(101, 162);
            kryptonLabel3.Margin = new Padding(4, 5, 4, 5);
            kryptonLabel3.Name = "kryptonLabel3";
            kryptonLabel3.Size = new Size(88, 24);
            kryptonLabel3.TabIndex = 15;
            kryptonLabel3.Values.Text = "Para Birimi:";
            // 
            // numAmount
            // 
            numAmount.AllowDecimals = true;
            numAmount.DecimalPlaces = 2;
            numAmount.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numAmount.Location = new Point(200, 95);
            numAmount.Margin = new Padding(4, 5, 4, 5);
            numAmount.Maximum = 999999999999.999999m;
            numAmount.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            numAmount.Name = "numAmount";
            numAmount.Size = new Size(289, 26);
            numAmount.TabIndex = 14;
            numAmount.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // kryptonLabel2
            // 
            kryptonLabel2.Location = new Point(131, 95);
            kryptonLabel2.Margin = new Padding(4, 5, 4, 5);
            kryptonLabel2.Name = "kryptonLabel2";
            kryptonLabel2.Size = new Size(59, 24);
            kryptonLabel2.TabIndex = 13;
            kryptonLabel2.Values.Text = "Miktar:";
            // 
            // cmbWallets
            // 
            cmbWallets.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbWallets.DropDownWidth = 217;
            cmbWallets.IntegralHeight = false;
            cmbWallets.Location = new Point(200, 31);
            cmbWallets.Margin = new Padding(4, 5, 4, 5);
            cmbWallets.Name = "cmbWallets";
            cmbWallets.Size = new Size(289, 26);
            cmbWallets.TabIndex = 12;
            // 
            // kryptonLabel1
            // 
            kryptonLabel1.Location = new Point(59, 31);
            kryptonLabel1.Margin = new Padding(4, 5, 4, 5);
            kryptonLabel1.Name = "kryptonLabel1";
            kryptonLabel1.Size = new Size(129, 24);
            kryptonLabel1.TabIndex = 11;
            kryptonLabel1.Values.Text = "               Cüzdan:";
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(270, 570);
            btnCancel.Margin = new Padding(4, 5, 4, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(160, 52);
            btnCancel.TabIndex = 6;
            btnCancel.Values.DropDownArrowColor = Color.Empty;
            btnCancel.Values.Text = "Kapat";
            btnCancel.Click += btnCancel_Click;
            // 
            // AddBalanceForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(669, 624);
            Controls.Add(kryptonPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddBalanceForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bakiye Yönetimi";
            Load += AddBalanceForm_Load;
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).EndInit();
            kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)groupRemoveBalance.Panel).EndInit();
            groupRemoveBalance.Panel.ResumeLayout(false);
            groupRemoveBalance.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)groupRemoveBalance).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbAssetToRemove).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupAddBalance.Panel).EndInit();
            groupAddBalance.Panel.ResumeLayout(false);
            groupAddBalance.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)groupAddBalance).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbCurrency).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbWallets).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Krypton.Toolkit.KryptonGroupBox groupAddBalance;
        private Krypton.Toolkit.KryptonButton btnConfirm;
        private Krypton.Toolkit.KryptonComboBox cmbCurrency;
        private Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private Krypton.Toolkit.KryptonNumericUpDown numAmount;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonComboBox cmbWallets;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonGroupBox groupRemoveBalance;
        private Krypton.Toolkit.KryptonButton btnRemoveBalance;
        private Krypton.Toolkit.KryptonNumericUpDown numAmountToRemove;
        private Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private Krypton.Toolkit.KryptonComboBox cmbAssetToRemove;
        private Krypton.Toolkit.KryptonLabel kryptonLabel4;
    }
}