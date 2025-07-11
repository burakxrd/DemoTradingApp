﻿// AUTOGENERATED - DO NOT EDIT MANUALLY
// This code is generated to use a KryptonPanel as the base for a themed TableLayoutPanel.

using DemoTradingApp.Properties;

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
            this.components = new System.ComponentModel.Container();
            this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonHeader1 = new Krypton.Toolkit.KryptonHeader();
            this.groupTradeType = new Krypton.Toolkit.KryptonGroupBox();
            this.radioSell = new Krypton.Toolkit.KryptonRadioButton();
            this.radioBuy = new Krypton.Toolkit.KryptonRadioButton();
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.cmbBaseCurrency = new Krypton.Toolkit.KryptonComboBox();
            this.lblBalance = new Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel4 = new Krypton.Toolkit.KryptonLabel();
            this.cmbQuoteAsset = new Krypton.Toolkit.KryptonComboBox();
            this.lblAmount = new Krypton.Toolkit.KryptonLabel();
            this.numAmount = new Krypton.Toolkit.KryptonNumericUpDown();
            this.lblRate = new Krypton.Toolkit.KryptonLabel();
            this.lblResult = new Krypton.Toolkit.KryptonLabel();
            this.btnExecuteTrade = new Krypton.Toolkit.KryptonButton();
            this.lblStatusMessage = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.mainTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupTradeType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupTradeType.Panel)).BeginInit();
            this.groupTradeType.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBaseCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuoteAsset)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.GlobalPaletteMode = Krypton.Toolkit.PaletteMode.SparkleBlue;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.mainTableLayoutPanel);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(498, 550);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.mainTableLayoutPanel.ColumnCount = 2;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Controls.Add(this.kryptonHeader1, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.groupTradeType, 0, 1);
            this.mainTableLayoutPanel.Controls.Add(this.kryptonLabel1, 0, 2);
            this.mainTableLayoutPanel.Controls.Add(this.cmbBaseCurrency, 1, 2);
            this.mainTableLayoutPanel.Controls.Add(this.lblBalance, 1, 3);
            this.mainTableLayoutPanel.Controls.Add(this.kryptonLabel4, 0, 4);
            this.mainTableLayoutPanel.Controls.Add(this.cmbQuoteAsset, 1, 4);
            this.mainTableLayoutPanel.Controls.Add(this.lblAmount, 0, 5);
            this.mainTableLayoutPanel.Controls.Add(this.numAmount, 1, 5);
            this.mainTableLayoutPanel.Controls.Add(this.lblRate, 1, 6);
            this.mainTableLayoutPanel.Controls.Add(this.lblResult, 1, 7);
            this.mainTableLayoutPanel.Controls.Add(this.btnExecuteTrade, 0, 8);
            this.mainTableLayoutPanel.Controls.Add(this.lblStatusMessage, 0, 9);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.Padding = new System.Windows.Forms.Padding(10);
            this.mainTableLayoutPanel.RowCount = 10;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(498, 550);
            this.mainTableLayoutPanel.TabIndex = 1;
            // 
            // kryptonHeader1
            // 
            this.mainTableLayoutPanel.SetColumnSpan(this.kryptonHeader1, 2);
            this.kryptonHeader1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeader1.Location = new System.Drawing.Point(13, 13);
            this.kryptonHeader1.Name = "kryptonHeader1";
            this.kryptonHeader1.Size = new System.Drawing.Size(472, 36);
            this.kryptonHeader1.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.kryptonHeader1.TabIndex = 0;
            this.kryptonHeader1.Values.Description = "";
            this.kryptonHeader1.Values.Heading = global::DemoTradingApp.Properties.Resources.BuySellScreen;
            this.kryptonHeader1.Values.Image = null;
            // 
            // groupTradeType
            // 
            this.mainTableLayoutPanel.SetColumnSpan(this.groupTradeType, 2);
            this.groupTradeType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupTradeType.Location = new System.Drawing.Point(13, 55);
            this.groupTradeType.Name = "groupTradeType";
            // 
            // groupTradeType.Panel
            // 
            this.groupTradeType.Panel.Controls.Add(this.radioSell);
            this.groupTradeType.Panel.Controls.Add(this.radioBuy);
            this.groupTradeType.Size = new System.Drawing.Size(472, 75);
            this.groupTradeType.TabIndex = 1;
            this.groupTradeType.Values.Heading = global::DemoTradingApp.Properties.Resources.TransactionType;
            // 
            // radioSell
            // 
            this.radioSell.Location = new System.Drawing.Point(240, 10);
            this.radioSell.Name = "radioSell";
            this.radioSell.Size = new System.Drawing.Size(48, 24);
            this.radioSell.TabIndex = 1;
            this.radioSell.Values.Text = global::DemoTradingApp.Properties.Resources.Sell;
            this.radioSell.CheckedChanged += new System.EventHandler(this.radioType_CheckedChanged);
            // 
            // radioBuy
            // 
            this.radioBuy.Checked = true;
            this.radioBuy.Location = new System.Drawing.Point(180, 10);
            this.radioBuy.Name = "radioBuy";
            this.radioBuy.Size = new System.Drawing.Size(40, 24);
            this.radioBuy.TabIndex = 0;
            this.radioBuy.Values.Text = global::DemoTradingApp.Properties.Resources.Buy;
            this.radioBuy.CheckedChanged += new System.EventHandler(this.radioType_CheckedChanged);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kryptonLabel1.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonLabel1.Location = new System.Drawing.Point(13, 142);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(125, 24);
            this.kryptonLabel1.TabIndex = 2;
            this.kryptonLabel1.Values.Text = "Money to Spend:";
            // 
            // cmbBaseCurrency
            // 
            this.cmbBaseCurrency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBaseCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBaseCurrency.DropDownWidth = 212;
            this.cmbBaseCurrency.IntegralHeight = false;
            this.cmbBaseCurrency.Location = new System.Drawing.Point(144, 141);
            this.cmbBaseCurrency.Name = "cmbBaseCurrency";
            this.cmbBaseCurrency.Size = new System.Drawing.Size(341, 26);
            this.cmbBaseCurrency.TabIndex = 3;
            this.cmbBaseCurrency.SelectedIndexChanged += new System.EventHandler(this.Cmb_SelectedIndexChanged);
            // 
            // lblBalance
            // 
            this.lblBalance.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBalance.Location = new System.Drawing.Point(144, 173);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(125, 24);
            this.lblBalance.TabIndex = 4;
            this.lblBalance.Values.Text = global::DemoTradingApp.Properties.Resources.BalanceFormat;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.kryptonLabel4.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.kryptonLabel4.Location = new System.Drawing.Point(34, 206);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(104, 24);
            this.kryptonLabel4.TabIndex = 5;
            this.kryptonLabel4.Values.Text = "Asset to Buy:";
            // 
            // cmbQuoteAsset
            // 
            this.cmbQuoteAsset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbQuoteAsset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuoteAsset.DropDownWidth = 212;
            this.cmbQuoteAsset.IntegralHeight = false;
            this.cmbQuoteAsset.Location = new System.Drawing.Point(144, 205);
            this.cmbQuoteAsset.Name = "cmbQuoteAsset";
            this.cmbQuoteAsset.Size = new System.Drawing.Size(341, 26);
            this.cmbQuoteAsset.TabIndex = 6;
            this.cmbQuoteAsset.SelectedIndexChanged += new System.EventHandler(this.Cmb_SelectedIndexChanged);
            // 
            // lblAmount
            // 
            this.lblAmount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblAmount.LabelStyle = Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblAmount.Location = new System.Drawing.Point(13, 240);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(125, 24);
            this.lblAmount.TabIndex = 7;
            this.lblAmount.Values.Text = "Amount to Spend:";
            // 
            // numAmount
            // 
            this.numAmount.AllowDecimals = true;
            this.numAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numAmount.DecimalPlaces = 6;
            this.numAmount.Location = new System.Drawing.Point(144, 239);
            this.numAmount.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numAmount.Name = "numAmount";
            this.numAmount.Size = new System.Drawing.Size(341, 26);
            this.numAmount.TabIndex = 8;
            this.numAmount.ValueChanged += new System.EventHandler(this.numAmount_ValueChanged);
            // 
            // lblRate
            // 
            this.lblRate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRate.Location = new System.Drawing.Point(144, 271);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(126, 24);
            this.lblRate.TabIndex = 9;
            this.lblRate.Values.Text = global::DemoTradingApp.Properties.Resources.LivePriceFormat;
            // 
            // lblResult
            // 
            this.lblResult.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblResult.Location = new System.Drawing.Point(144, 301);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(191, 24);
            this.lblResult.TabIndex = 10;
            this.lblResult.Values.Text = global::DemoTradingApp.Properties.Resources.EstimatedResultFormat;
            // 
            // btnExecuteTrade
            // 
            this.btnExecuteTrade.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mainTableLayoutPanel.SetColumnSpan(this.btnExecuteTrade, 2);
            this.btnExecuteTrade.Location = new System.Drawing.Point(143, 334);
            this.btnExecuteTrade.Name = "btnExecuteTrade";
            this.btnExecuteTrade.Size = new System.Drawing.Size(212, 69);
            this.btnExecuteTrade.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnExecuteTrade.TabIndex = 11;
            this.btnExecuteTrade.Values.Text = global::DemoTradingApp.Properties.Resources.ConfirmTransaction;
            this.btnExecuteTrade.Click += new System.EventHandler(this.btnExecuteTrade_Click);
            // 
            // lblStatusMessage
            // 
            this.lblStatusMessage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mainTableLayoutPanel.SetColumnSpan(this.lblStatusMessage, 2);
            this.lblStatusMessage.Location = new System.Drawing.Point(188, 492);
            this.lblStatusMessage.Name = "lblStatusMessage";
            this.lblStatusMessage.Size = new System.Drawing.Size(116, 27);
            this.lblStatusMessage.StateCommon.ShortText.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblStatusMessage.TabIndex = 13;
            this.lblStatusMessage.Values.Text = global::DemoTradingApp.Properties.Resources.StatusMessage;
            this.lblStatusMessage.Visible = false;
            // 
            // TradesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 635);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TradesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trade Transaction";
            this.Load += new System.EventHandler(this.TradesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.mainTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupTradeType.Panel)).EndInit();
            this.groupTradeType.Panel.ResumeLayout(false);
            this.groupTradeType.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupTradeType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBaseCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbQuoteAsset)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonManager kryptonManager1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private Krypton.Toolkit.KryptonHeader kryptonHeader1;
        private Krypton.Toolkit.KryptonGroupBox groupTradeType;
        private Krypton.Toolkit.KryptonRadioButton radioSell;
        private Krypton.Toolkit.KryptonRadioButton radioBuy;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonComboBox cmbBaseCurrency;
        private Krypton.Toolkit.KryptonLabel lblBalance;
        private Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private Krypton.Toolkit.KryptonComboBox cmbQuoteAsset;
        private Krypton.Toolkit.KryptonLabel lblAmount;
        private Krypton.Toolkit.KryptonNumericUpDown numAmount;
        private Krypton.Toolkit.KryptonLabel lblRate;
        private Krypton.Toolkit.KryptonLabel lblResult;
        private Krypton.Toolkit.KryptonButton btnExecuteTrade;
        private Krypton.Toolkit.KryptonLabel lblStatusMessage;
    }
}
