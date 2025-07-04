﻿// AUTOGENERATED - DO NOT EDIT MANUALLY
// This code is generated to use a TableLayoutPanel for a responsive and localizable layout.

using DemoTradingApp.Properties;

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
            components = new System.ComponentModel.Container();
            kryptonManager1 = new Krypton.Toolkit.KryptonManager(components);
            kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            mainTableLayoutPanel = new TableLayoutPanel();
            groupAddWallet = new Krypton.Toolkit.KryptonGroupBox();
            tableLayoutPanelAdd = new TableLayoutPanel();
            kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            txtWalletName = new Krypton.Toolkit.KryptonTextBox();
            kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            cmbWalletType = new Krypton.Toolkit.KryptonComboBox();
            btnConfirm = new Krypton.Toolkit.KryptonButton();
            groupDeleteWallet = new Krypton.Toolkit.KryptonGroupBox();
            tableLayoutPanelDelete = new TableLayoutPanel();
            cmbExistingWallets = new Krypton.Toolkit.KryptonComboBox();
            btnDelete = new Krypton.Toolkit.KryptonButton();
            btnCancel = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).BeginInit();
            kryptonPanel1.SuspendLayout();
            mainTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)groupAddWallet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupAddWallet.Panel).BeginInit();
            groupAddWallet.Panel.SuspendLayout();
            tableLayoutPanelAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cmbWalletType).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupDeleteWallet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupDeleteWallet.Panel).BeginInit();
            groupDeleteWallet.Panel.SuspendLayout();
            tableLayoutPanelDelete.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cmbExistingWallets).BeginInit();
            SuspendLayout();
            // 
            // kryptonManager1
            // 
            kryptonManager1.GlobalPaletteMode = Krypton.Toolkit.PaletteMode.SparkleBlue;
            // 
            // kryptonPanel1
            // 
            kryptonPanel1.Controls.Add(mainTableLayoutPanel);
            kryptonPanel1.Dock = DockStyle.Fill;
            kryptonPanel1.Location = new Point(0, 0);
            kryptonPanel1.Name = "kryptonPanel1";
            kryptonPanel1.Padding = new Padding(10);
            kryptonPanel1.Size = new Size(550, 407);
            kryptonPanel1.TabIndex = 0;
            // 
            // mainTableLayoutPanel
            // 
            mainTableLayoutPanel.BackColor = Color.Transparent;
            mainTableLayoutPanel.ColumnCount = 1;
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainTableLayoutPanel.Controls.Add(groupAddWallet, 0, 0);
            mainTableLayoutPanel.Controls.Add(groupDeleteWallet, 0, 1);
            mainTableLayoutPanel.Controls.Add(btnCancel, 0, 2);
            mainTableLayoutPanel.Dock = DockStyle.Fill;
            mainTableLayoutPanel.Location = new Point(10, 10);
            mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            mainTableLayoutPanel.RowCount = 3;
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 220F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainTableLayoutPanel.Size = new Size(530, 387);
            mainTableLayoutPanel.TabIndex = 0;
            // 
            // groupAddWallet
            // 
            groupAddWallet.CaptionStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            groupAddWallet.Dock = DockStyle.Fill;
            groupAddWallet.Location = new Point(3, 3);
            // 
            // 
            // 
            groupAddWallet.Panel.Controls.Add(tableLayoutPanelAdd);
            groupAddWallet.Size = new Size(524, 214);
            groupAddWallet.TabIndex = 0;
            groupAddWallet.Values.Heading = Resources.CreateNewWallet;
            // 
            // tableLayoutPanelAdd
            // 
            tableLayoutPanelAdd.BackColor = Color.Transparent;
            tableLayoutPanelAdd.ColumnCount = 2;
            tableLayoutPanelAdd.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanelAdd.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelAdd.Controls.Add(kryptonLabel1, 0, 0);
            tableLayoutPanelAdd.Controls.Add(txtWalletName, 1, 0);
            tableLayoutPanelAdd.Controls.Add(kryptonLabel2, 0, 1);
            tableLayoutPanelAdd.Controls.Add(cmbWalletType, 1, 1);
            tableLayoutPanelAdd.Controls.Add(btnConfirm, 1, 2);
            tableLayoutPanelAdd.Dock = DockStyle.Fill;
            tableLayoutPanelAdd.Location = new Point(0, 0);
            tableLayoutPanelAdd.Name = "tableLayoutPanelAdd";
            tableLayoutPanelAdd.Padding = new Padding(5);
            tableLayoutPanelAdd.RowCount = 3;
            tableLayoutPanelAdd.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
            tableLayoutPanelAdd.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
            tableLayoutPanelAdd.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333F));
            tableLayoutPanelAdd.Size = new Size(520, 177);
            tableLayoutPanelAdd.TabIndex = 0;
            // 
            // kryptonLabel1
            // 
            kryptonLabel1.Anchor = AnchorStyles.Right;
            kryptonLabel1.Location = new Point(16, 20);
            kryptonLabel1.Name = "kryptonLabel1";
            kryptonLabel1.Size = new Size(93, 24);
            kryptonLabel1.TabIndex = 0;
            kryptonLabel1.Values.Text = Resources.WalletName;
            // 
            // txtWalletName
            // 
            txtWalletName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtWalletName.Location = new Point(115, 19);
            txtWalletName.Name = "txtWalletName";
            txtWalletName.Size = new Size(397, 27);
            txtWalletName.TabIndex = 1;
            // 
            // kryptonLabel2
            // 
            kryptonLabel2.Anchor = AnchorStyles.Right;
            kryptonLabel2.Location = new Point(8, 75);
            kryptonLabel2.Name = "kryptonLabel2";
            kryptonLabel2.Size = new Size(101, 24);
            kryptonLabel2.TabIndex = 2;
            kryptonLabel2.Values.Text = Resources.WalletType;
            // 
            // cmbWalletType
            // 
            cmbWalletType.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbWalletType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbWalletType.DropDownWidth = 217;
            cmbWalletType.IntegralHeight = false;
            cmbWalletType.Location = new Point(115, 74);
            cmbWalletType.Name = "cmbWalletType";
            cmbWalletType.Size = new Size(397, 26);
            cmbWalletType.TabIndex = 3;
            // 
            // btnConfirm
            // 
            btnConfirm.Anchor = AnchorStyles.None;
            btnConfirm.Location = new Point(233, 123);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(160, 41);
            btnConfirm.TabIndex = 4;
            btnConfirm.Values.DropDownArrowColor = Color.Empty;
            btnConfirm.Values.Text = Resources.CreateWallet;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // groupDeleteWallet
            // 
            groupDeleteWallet.CaptionStyle = Krypton.Toolkit.LabelStyle.TitleControl;
            groupDeleteWallet.Dock = DockStyle.Fill;
            groupDeleteWallet.Location = new Point(3, 223);
            // 
            // 
            // 
            groupDeleteWallet.Panel.Controls.Add(tableLayoutPanelDelete);
            groupDeleteWallet.Size = new Size(524, 114);
            groupDeleteWallet.TabIndex = 1;
            groupDeleteWallet.Values.Heading = Resources.DeleteExistingWallet;
            // 
            // tableLayoutPanelDelete
            // 
            tableLayoutPanelDelete.BackColor = Color.Transparent;
            tableLayoutPanelDelete.ColumnCount = 2;
            tableLayoutPanelDelete.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelDelete.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelDelete.Controls.Add(cmbExistingWallets, 0, 0);
            tableLayoutPanelDelete.Controls.Add(btnDelete, 1, 0);
            tableLayoutPanelDelete.Dock = DockStyle.Fill;
            tableLayoutPanelDelete.Location = new Point(0, 0);
            tableLayoutPanelDelete.Name = "tableLayoutPanelDelete";
            tableLayoutPanelDelete.Padding = new Padding(5);
            tableLayoutPanelDelete.RowCount = 1;
            tableLayoutPanelDelete.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelDelete.Size = new Size(520, 77);
            tableLayoutPanelDelete.TabIndex = 0;
            // 
            // cmbExistingWallets
            // 
            cmbExistingWallets.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbExistingWallets.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbExistingWallets.DropDownWidth = 290;
            cmbExistingWallets.IntegralHeight = false;
            cmbExistingWallets.Location = new Point(8, 25);
            cmbExistingWallets.Name = "cmbExistingWallets";
            cmbExistingWallets.Size = new Size(351, 26);
            cmbExistingWallets.TabIndex = 0;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnDelete.Location = new Point(365, 10);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(147, 57);
            btnDelete.TabIndex = 1;
            btnDelete.Values.DropDownArrowColor = Color.Empty;
            btnDelete.Values.Text = Resources.DeleteSelectedWallet;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.None;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(185, 345);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(160, 36);
            btnCancel.TabIndex = 2;
            btnCancel.Values.DropDownArrowColor = Color.Empty;
            btnCancel.Values.Text = Resources.Close;
            btnCancel.Click += btnCancel_Click;
            // 
            // AddWalletForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(550, 407);
            Controls.Add(kryptonPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddWalletForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = Properties.Resources.WalletManagement;
            Load += AddWalletForm_Load;
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).EndInit();
            kryptonPanel1.ResumeLayout(false);
            mainTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)groupAddWallet.Panel).EndInit();
            groupAddWallet.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)groupAddWallet).EndInit();
            tableLayoutPanelAdd.ResumeLayout(false);
            tableLayoutPanelAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cmbWalletType).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupDeleteWallet.Panel).EndInit();
            groupDeleteWallet.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)groupDeleteWallet).EndInit();
            tableLayoutPanelDelete.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)cmbExistingWallets).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonManager kryptonManager1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private Krypton.Toolkit.KryptonGroupBox groupAddWallet;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelAdd;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonTextBox txtWalletName;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonComboBox cmbWalletType;
        private Krypton.Toolkit.KryptonButton btnConfirm;
        private Krypton.Toolkit.KryptonGroupBox groupDeleteWallet;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDelete;
        private Krypton.Toolkit.KryptonComboBox cmbExistingWallets;
        private Krypton.Toolkit.KryptonButton btnDelete;
        private Krypton.Toolkit.KryptonButton btnCancel;
    }
}
