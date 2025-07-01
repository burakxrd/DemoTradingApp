using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace DemoTradingApp
{
    partial class TradesForm
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
            components = new System.ComponentModel.Container();
            kryptonManager1 = new Krypton.Toolkit.KryptonManager(components);
            kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            groupTradeType = new Krypton.Toolkit.KryptonGroupBox();
            radioSell = new Krypton.Toolkit.KryptonRadioButton();
            radioBuy = new Krypton.Toolkit.KryptonRadioButton();
            btnExecuteTrade = new Krypton.Toolkit.KryptonButton();
            lblResult = new Krypton.Toolkit.KryptonLabel();
            lblRate = new Krypton.Toolkit.KryptonLabel();
            cmbQuoteAsset = new Krypton.Toolkit.KryptonComboBox();
            kryptonLabel4 = new Krypton.Toolkit.KryptonLabel();
            numAmount = new Krypton.Toolkit.KryptonNumericUpDown();
            lblAmount = new Krypton.Toolkit.KryptonLabel();
            lblBalance = new Krypton.Toolkit.KryptonLabel();
            cmbBaseCurrency = new Krypton.Toolkit.KryptonComboBox();
            kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            kryptonHeader1 = new Krypton.Toolkit.KryptonHeader();
            lblStatusMessage = new Krypton.Toolkit.KryptonLabel(); ;
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).BeginInit();
            kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)groupTradeType).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupTradeType.Panel).BeginInit();
            groupTradeType.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cmbQuoteAsset).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbBaseCurrency).BeginInit();
            SuspendLayout();
            // 
            // kryptonManager1
            // 
            kryptonManager1.GlobalPaletteMode = Krypton.Toolkit.PaletteMode.SparkleBlue;
            // 
            // kryptonPanel1
            // 
            kryptonPanel1.Controls.Add(groupTradeType);
            kryptonPanel1.Controls.Add(btnExecuteTrade);
            kryptonPanel1.Controls.Add(lblResult);
            kryptonPanel1.Controls.Add(lblRate);
            kryptonPanel1.Controls.Add(cmbQuoteAsset);
            kryptonPanel1.Controls.Add(kryptonLabel4);
            kryptonPanel1.Controls.Add(numAmount);
            kryptonPanel1.Controls.Add(lblAmount);
            kryptonPanel1.Controls.Add(lblBalance);
            kryptonPanel1.Controls.Add(cmbBaseCurrency);
            kryptonPanel1.Controls.Add(kryptonLabel1);
            kryptonPanel1.Controls.Add(kryptonHeader1);
            this.kryptonPanel1.Controls.Add(this.lblStatusMessage);
            kryptonPanel1.Dock = DockStyle.Fill;
            kryptonPanel1.Location = new Point(0, 0);
            kryptonPanel1.Margin = new Padding(3, 4, 3, 4);
            kryptonPanel1.Name = "kryptonPanel1";
            kryptonPanel1.Size = new Size(490, 663);
            kryptonPanel1.TabIndex = 0;
            // 
            // lblStatusMessage
            // 
            this.lblStatusMessage.Location = new System.Drawing.Point(135, 220);
            this.lblStatusMessage.Name = "lblStatusMessage";
            this.lblStatusMessage.Size = new System.Drawing.Size(212, 24);
            this.lblStatusMessage.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblStatusMessage.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            this.lblStatusMessage.TabIndex = 13;
            this.lblStatusMessage.Values.Text = "Durum Mesajı";
            this.lblStatusMessage.Visible = false; // Başlangıçta gizli
            // 
            // groupTradeType
            // 
            groupTradeType.Location = new Point(39, 44);
            groupTradeType.Margin = new Padding(3, 4, 3, 4);
            // 
            // 
            // 
            groupTradeType.Panel.Controls.Add(radioSell);
            groupTradeType.Panel.Controls.Add(radioBuy);
            groupTradeType.Size = new Size(404, 75);
            groupTradeType.TabIndex = 12;
            groupTradeType.Values.Heading = "İşlem Türü";
            // 
            // radioSell
            // 
            radioSell.Location = new Point(223, 5);
            radioSell.Margin = new Padding(3, 4, 3, 4);
            radioSell.Name = "radioSell";
            radioSell.Size = new Size(48, 24);
            radioSell.TabIndex = 1;
            radioSell.Values.Text = "Sat";
            radioSell.CheckedChanged += radioType_CheckedChanged;
            // 
            // radioBuy
            // 
            radioBuy.Checked = true;
            radioBuy.Location = new Point(125, 5);
            radioBuy.Margin = new Padding(3, 4, 3, 4);
            radioBuy.Name = "radioBuy";
            radioBuy.Size = new Size(40, 24);
            radioBuy.TabIndex = 0;
            radioBuy.Values.Text = "Al";
            radioBuy.CheckedChanged += radioType_CheckedChanged;
            // 
            // btnExecuteTrade
            // 
            btnExecuteTrade.Location = new Point(135, 431);
            btnExecuteTrade.Margin = new Padding(3, 4, 3, 4);
            btnExecuteTrade.Name = "btnExecuteTrade";
            btnExecuteTrade.Size = new Size(212, 69);
            btnExecuteTrade.StateCommon.Content.ShortText.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnExecuteTrade.TabIndex = 10;
            btnExecuteTrade.Values.DropDownArrowColor = Color.Empty;
            btnExecuteTrade.Values.Text = "İşlemi Onayla";
            btnExecuteTrade.Click += btnExecuteTrade_Click;
            // 
            // lblResult
            // 
            lblResult.Location = new Point(135, 379);
            lblResult.Margin = new Padding(3, 4, 3, 4);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(191, 24);
            lblResult.TabIndex = 9;
            lblResult.Values.Text = "~0,000000 XYZ alacaksınız";
            // 
            // lblRate
            // 
            lblRate.Location = new Point(135, 341);
            lblRate.Margin = new Padding(3, 4, 3, 4);
            lblRate.Name = "lblRate";
            lblRate.Size = new Size(126, 24);
            lblRate.TabIndex = 8;
            lblRate.Values.Text = "Anlık Fiyat: $0.00";
            // 
            // cmbQuoteAsset
            // 
            cmbQuoteAsset.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbQuoteAsset.DropDownWidth = 212;
            cmbQuoteAsset.IntegralHeight = false;
            cmbQuoteAsset.Location = new Point(135, 252);
            cmbQuoteAsset.Margin = new Padding(3, 4, 3, 4);
            cmbQuoteAsset.Name = "cmbQuoteAsset";
            cmbQuoteAsset.Size = new Size(212, 26);
            cmbQuoteAsset.TabIndex = 7;
            cmbQuoteAsset.SelectedIndexChanged += Cmb_SelectedIndexChanged;
            // 
            // kryptonLabel4
            // 
            kryptonLabel4.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            kryptonLabel4.Location = new Point(12, 252);
            kryptonLabel4.Margin = new Padding(3, 4, 3, 4);
            kryptonLabel4.Name = "kryptonLabel4";
            kryptonLabel4.Size = new Size(115, 24);
            kryptonLabel4.TabIndex = 6;
            kryptonLabel4.Values.Text = "Kripto / Hisse:";
            // 
            // numAmount
            // 
            numAmount.AllowDecimals = true;
            numAmount.DecimalPlaces = 6;
            numAmount.Increment = new decimal(new int[] { 1, 0, 0, 0 });
            numAmount.Location = new Point(135, 301);
            numAmount.Margin = new Padding(3, 4, 3, 4);
            numAmount.Maximum = 999999999999.999999m;
            numAmount.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            numAmount.Name = "numAmount";
            numAmount.Size = new Size(212, 26);
            numAmount.TabIndex = 5;
            numAmount.Value = new decimal(new int[] { 0, 0, 0, 0 });
            numAmount.ValueChanged += numAmount_ValueChanged;
            // 
            // lblAmount
            // 
            lblAmount.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            lblAmount.Location = new Point(67, 301);
            lblAmount.Margin = new Padding(3, 4, 3, 4);
            lblAmount.Name = "lblAmount";
            lblAmount.Size = new Size(64, 24);
            lblAmount.TabIndex = 4;
            lblAmount.Values.Text = "Miktar:";
            // 
            // lblBalance
            // 
            lblBalance.Location = new Point(135, 195);
            lblBalance.Margin = new Padding(3, 4, 3, 4);
            lblBalance.Name = "lblBalance";
            lblBalance.Size = new Size(125, 24);
            lblBalance.TabIndex = 3;
            lblBalance.Values.Text = "Bakiye: 0,00 USD";
            // 
            // cmbBaseCurrency
            // 
            cmbBaseCurrency.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBaseCurrency.DropDownWidth = 212;
            cmbBaseCurrency.IntegralHeight = false;
            cmbBaseCurrency.Location = new Point(135, 156);
            cmbBaseCurrency.Margin = new Padding(3, 4, 3, 4);
            cmbBaseCurrency.Name = "cmbBaseCurrency";
            cmbBaseCurrency.Size = new Size(212, 26);
            cmbBaseCurrency.TabIndex = 2;
            cmbBaseCurrency.SelectedIndexChanged += Cmb_SelectedIndexChanged;
            // 
            // kryptonLabel1
            // 
            kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            kryptonLabel1.Location = new Point(39, 156);
            kryptonLabel1.Margin = new Padding(3, 4, 3, 4);
            kryptonLabel1.Name = "kryptonLabel1";
            kryptonLabel1.Size = new Size(96, 24);
            kryptonLabel1.TabIndex = 1;
            kryptonLabel1.Values.Text = "Para Birimi:";
            // 
            // kryptonHeader1
            // 
            kryptonHeader1.Dock = DockStyle.Top;
            kryptonHeader1.Location = new Point(0, 0);
            kryptonHeader1.Margin = new Padding(3, 4, 3, 4);
            kryptonHeader1.Name = "kryptonHeader1";
            kryptonHeader1.Size = new Size(490, 36);
            kryptonHeader1.StateCommon.Content.ShortText.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            kryptonHeader1.TabIndex = 0;
            kryptonHeader1.Values.Description = "";
            kryptonHeader1.Values.Heading = "Alım / Satım Ekranı";
            kryptonHeader1.Values.Image = null;
            // 
            // TradesForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(490, 663);
            Controls.Add(kryptonPanel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(3, 4, 3, 4);
            Name = "TradesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Trade İşlemi";
            Load += TradesForm_Load;
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).EndInit();
            kryptonPanel1.ResumeLayout(false);
            kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)groupTradeType.Panel).EndInit();
            groupTradeType.Panel.ResumeLayout(false);
            groupTradeType.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)groupTradeType).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbQuoteAsset).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbBaseCurrency).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonManager kryptonManager1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonHeader kryptonHeader1;
        private Krypton.Toolkit.KryptonGroupBox groupTradeType;
        private Krypton.Toolkit.KryptonRadioButton radioSell;
        private Krypton.Toolkit.KryptonRadioButton radioBuy;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonComboBox cmbBaseCurrency;
        private Krypton.Toolkit.KryptonLabel lblBalance;
        private Krypton.Toolkit.KryptonLabel lblAmount;
        private Krypton.Toolkit.KryptonNumericUpDown numAmount;
        private Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private Krypton.Toolkit.KryptonComboBox cmbQuoteAsset;
        private Krypton.Toolkit.KryptonLabel lblRate;
        private Krypton.Toolkit.KryptonLabel lblResult;
        private Krypton.Toolkit.KryptonButton btnExecuteTrade;
        private Krypton.Toolkit.KryptonLabel lblStatusMessage;
    }
}