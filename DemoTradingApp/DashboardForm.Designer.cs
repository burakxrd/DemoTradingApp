// Bu kodun tamamını kopyalayıp DashboardForm.Designer.cs dosyanızın içine yapıştırın.

namespace DemoTradingApp
{
    partial class DashboardForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardForm));
            kryptonManager1 = new Krypton.Toolkit.KryptonManager(components);
            kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            cmbDisplayCurrency = new Krypton.Toolkit.KryptonComboBox();
            lblDisplayCurrency = new Krypton.Toolkit.KryptonLabel();
            btnMenu = new Krypton.Toolkit.KryptonButton();
            kcmMenu = new Krypton.Toolkit.KryptonContextMenu();
            kryptonContextMenuItems1 = new Krypton.Toolkit.KryptonContextMenuItems();
            cmiShowDashboard = new Krypton.Toolkit.KryptonContextMenuItem();
            kryptonContextMenuSeparator1 = new Krypton.Toolkit.KryptonContextMenuSeparator();
            cmiAddWallet = new Krypton.Toolkit.KryptonContextMenuItem();
            cmiAddBalance = new Krypton.Toolkit.KryptonContextMenuItem();
            lblWelcome = new Krypton.Toolkit.KryptonLabel();
            pnlMainDashboard = new Krypton.Toolkit.KryptonPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            groupTrades = new Krypton.Toolkit.KryptonHeaderGroup();
            dgvTrades = new Krypton.Toolkit.KryptonDataGridView();
            groupMarket = new Krypton.Toolkit.KryptonHeaderGroup();
            dgvMarket = new Krypton.Toolkit.KryptonDataGridView();
            groupAssets = new Krypton.Toolkit.KryptonHeaderGroup();
            dgvAssets = new Krypton.Toolkit.KryptonDataGridView();
            groupBalance = new Krypton.Toolkit.KryptonHeaderGroup();
            lblBalance = new Krypton.Toolkit.KryptonLabel();
            pnlUpdateTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).BeginInit();
            kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cmbDisplayCurrency).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pnlMainDashboard).BeginInit();
            pnlMainDashboard.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)groupTrades).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupTrades.Panel).BeginInit();
            groupTrades.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTrades).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupMarket).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupMarket.Panel).BeginInit();
            groupMarket.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMarket).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupAssets).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupAssets.Panel).BeginInit();
            groupAssets.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAssets).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupBalance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupBalance.Panel).BeginInit();
            groupBalance.Panel.SuspendLayout();
            SuspendLayout();
            // 
            // kryptonManager1
            // 
            kryptonManager1.GlobalPaletteMode = Krypton.Toolkit.PaletteMode.SparkleBlue;
            // 
            // kryptonPanel1
            // 
            kryptonPanel1.Controls.Add(cmbDisplayCurrency);
            kryptonPanel1.Controls.Add(lblDisplayCurrency);
            kryptonPanel1.Controls.Add(btnMenu);
            kryptonPanel1.Controls.Add(lblWelcome);
            kryptonPanel1.Controls.Add(pnlMainDashboard);
            kryptonPanel1.Dock = DockStyle.Fill;
            kryptonPanel1.Location = new Point(0, 0);
            kryptonPanel1.Margin = new Padding(3, 4, 3, 4);
            kryptonPanel1.Name = "kryptonPanel1";
            kryptonPanel1.Padding = new Padding(10, 12, 10, 12);
            kryptonPanel1.Size = new Size(1358, 809);
            kryptonPanel1.TabIndex = 0;
            // 
            // cmbDisplayCurrency
            // 
            cmbDisplayCurrency.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbDisplayCurrency.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDisplayCurrency.DropDownWidth = 121;
            cmbDisplayCurrency.IntegralHeight = false;
            cmbDisplayCurrency.Location = new Point(1132, 10);
            cmbDisplayCurrency.Margin = new Padding(3, 4, 3, 4);
            cmbDisplayCurrency.Name = "cmbDisplayCurrency";
            cmbDisplayCurrency.Size = new Size(121, 26);
            cmbDisplayCurrency.TabIndex = 9;
            cmbDisplayCurrency.SelectedIndexChanged += cmbDisplayCurrency_SelectedIndexChanged;
            // 
            // lblDisplayCurrency
            // 
            lblDisplayCurrency.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDisplayCurrency.Location = new Point(1038, 11);
            lblDisplayCurrency.Margin = new Padding(3, 4, 3, 4);
            lblDisplayCurrency.Name = "lblDisplayCurrency";
            lblDisplayCurrency.Size = new Size(88, 24);
            lblDisplayCurrency.TabIndex = 10;
            lblDisplayCurrency.Values.Text = "Para Birimi:";
            // 
            // btnMenu
            // 
            btnMenu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMenu.KryptonContextMenu = kcmMenu;
            btnMenu.Location = new Point(1259, 10);
            btnMenu.Margin = new Padding(3, 4, 3, 4);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(87, 31);
            btnMenu.StateCommon.Content.ShortText.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnMenu.TabIndex = 7;
            btnMenu.Values.DropDownArrowColor = Color.Empty;
            btnMenu.Values.Text = "≡";
            btnMenu.Click += btnMenu_Click;
            // 
            // kcmMenu
            // 
            kcmMenu.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] { kryptonContextMenuItems1 });
            // 
            // kryptonContextMenuItems1
            // 
            kryptonContextMenuItems1.Items.AddRange(new Krypton.Toolkit.KryptonContextMenuItemBase[] { cmiShowDashboard, kryptonContextMenuSeparator1, cmiAddWallet, cmiAddBalance });
            // 
            // cmiShowDashboard
            // 
            cmiShowDashboard.Text = "Ana Panel";
            cmiShowDashboard.Click += cmiShowDashboard_Click;
            // 
            // lblWelcome
            // 
            lblWelcome.Location = new Point(13, 10);
            lblWelcome.Margin = new Padding(3, 4, 3, 4);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(161, 32);
            lblWelcome.StateCommon.ShortText.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblWelcome.TabIndex = 0;
            lblWelcome.Values.Text = "Hoş geldiniz, ...";
            // 
            // pnlMainDashboard
            // 
            pnlMainDashboard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlMainDashboard.Controls.Add(tableLayoutPanel1);
            pnlMainDashboard.Location = new Point(10, 56);
            pnlMainDashboard.Margin = new Padding(3, 4, 3, 4);
            pnlMainDashboard.Name = "pnlMainDashboard";
            pnlMainDashboard.Size = new Size(1338, 740);
            pnlMainDashboard.TabIndex = 8;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.Transparent;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(groupTrades, 1, 1);
            tableLayoutPanel1.Controls.Add(groupMarket, 1, 0);
            tableLayoutPanel1.Controls.Add(groupAssets, 0, 1);
            tableLayoutPanel1.Controls.Add(groupBalance, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 250F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1338, 740);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // groupTrades
            // 
            groupTrades.Dock = DockStyle.Fill;
            groupTrades.HeaderVisibleSecondary = false;
            groupTrades.Location = new Point(672, 254);
            groupTrades.Margin = new Padding(3, 4, 3, 4);
            // 
            // 
            // 
            groupTrades.Panel.Controls.Add(dgvTrades);
            groupTrades.Size = new Size(663, 482);
            groupTrades.StateCommon.HeaderPrimary.Content.ShortText.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            groupTrades.TabIndex = 8;
            groupTrades.ValuesPrimary.Heading = "Son İşlemler";
            groupTrades.ValuesPrimary.Image = (Image)resources.GetObject("groupTrades.ValuesPrimary.Image");
            // 
            // dgvTrades
            // 
            dgvTrades.AllowUserToAddRows = false;
            dgvTrades.AllowUserToDeleteRows = false;
            dgvTrades.BorderStyle = BorderStyle.None;
            dgvTrades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTrades.Dock = DockStyle.Fill;
            dgvTrades.Location = new Point(0, 0);
            dgvTrades.Margin = new Padding(3, 4, 3, 4);
            dgvTrades.Name = "dgvTrades";
            dgvTrades.ReadOnly = true;
            dgvTrades.RowHeadersWidth = 51;
            dgvTrades.RowTemplate.Height = 24;
            dgvTrades.Size = new Size(661, 435);
            dgvTrades.TabIndex = 0;
            // 
            // groupMarket
            // 
            groupMarket.Dock = DockStyle.Fill;
            groupMarket.HeaderVisibleSecondary = false;
            groupMarket.Location = new Point(672, 4);
            groupMarket.Margin = new Padding(3, 4, 3, 4);
            // 
            // 
            // 
            groupMarket.Panel.Controls.Add(dgvMarket);
            groupMarket.Size = new Size(663, 242);
            groupMarket.StateCommon.HeaderPrimary.Content.ShortText.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            groupMarket.TabIndex = 7;
            groupMarket.ValuesPrimary.Heading = "Piyasa Fiyatları";
            groupMarket.ValuesPrimary.Image = (Image)resources.GetObject("groupMarket.ValuesPrimary.Image");
            // 
            // dgvMarket
            // 
            dgvMarket.AllowUserToAddRows = false;
            dgvMarket.AllowUserToDeleteRows = false;
            dgvMarket.BorderStyle = BorderStyle.None;
            dgvMarket.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMarket.Dock = DockStyle.Fill;
            dgvMarket.Location = new Point(0, 0);
            dgvMarket.Margin = new Padding(3, 4, 3, 4);
            dgvMarket.Name = "dgvMarket";
            dgvMarket.ReadOnly = true;
            dgvMarket.RowHeadersWidth = 51;
            dgvMarket.RowTemplate.Height = 24;
            dgvMarket.Size = new Size(661, 195);
            dgvMarket.TabIndex = 0;
            // 
            // groupAssets
            // 
            groupAssets.Dock = DockStyle.Fill;
            groupAssets.HeaderVisibleSecondary = false;
            groupAssets.Location = new Point(3, 254);
            groupAssets.Margin = new Padding(3, 4, 3, 4);
            // 
            // 
            // 
            groupAssets.Panel.Controls.Add(dgvAssets);
            groupAssets.Size = new Size(663, 482);
            groupAssets.StateCommon.HeaderPrimary.Content.ShortText.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            groupAssets.TabIndex = 6;
            groupAssets.ValuesPrimary.Heading = "Varlıklarım";
            groupAssets.ValuesPrimary.Image = (Image)resources.GetObject("groupAssets.ValuesPrimary.Image");
            // 
            // dgvAssets
            // 
            dgvAssets.AllowUserToAddRows = false;
            dgvAssets.AllowUserToDeleteRows = false;
            dgvAssets.BorderStyle = BorderStyle.None;
            dgvAssets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAssets.Dock = DockStyle.Fill;
            dgvAssets.Location = new Point(0, 0);
            dgvAssets.Margin = new Padding(3, 4, 3, 4);
            dgvAssets.Name = "dgvAssets";
            dgvAssets.ReadOnly = true;
            dgvAssets.RowHeadersWidth = 51;
            dgvAssets.RowTemplate.Height = 24;
            dgvAssets.Size = new Size(661, 435);
            dgvAssets.TabIndex = 0;
            // 
            // groupBalance
            // 
            groupBalance.Dock = DockStyle.Fill;
            groupBalance.HeaderVisibleSecondary = false;
            groupBalance.Location = new Point(3, 4);
            groupBalance.Margin = new Padding(3, 4, 3, 4);
            // 
            // 
            // 
            groupBalance.Panel.Controls.Add(lblBalance);
            groupBalance.Size = new Size(663, 242);
            groupBalance.StateCommon.HeaderPrimary.Content.ShortText.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            groupBalance.TabIndex = 5;
            groupBalance.ValuesPrimary.Heading = "Toplam Varlık Değeri";
            groupBalance.ValuesPrimary.Image = (Image)resources.GetObject("groupBalance.ValuesPrimary.Image");
            // 
            // lblBalance
            // 
            lblBalance.Dock = DockStyle.Fill;
            lblBalance.Location = new Point(0, 0);
            lblBalance.Margin = new Padding(3, 4, 3, 4);
            lblBalance.Name = "lblBalance";
            lblBalance.Size = new Size(661, 195);
            lblBalance.StateCommon.ShortText.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblBalance.StateCommon.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            lblBalance.StateCommon.ShortText.TextV = Krypton.Toolkit.PaletteRelativeAlign.Center;
            lblBalance.TabIndex = 0;
            lblBalance.Values.Text = "0.00";
            // 
            // pnlUpdateTimer
            // 
            pnlUpdateTimer.Enabled = true;
            pnlUpdateTimer.Interval = 60000;
            pnlUpdateTimer.Tick += pnlUpdateTimer_Tick;
            // 
            // DashboardForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1358, 809);
            Controls.Add(kryptonPanel1);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(1300, 863);
            Name = "DashboardForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ana Panel";
            Load += DashboardForm_Load;
            Resize += DashboardForm_Resize;
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).EndInit();
            kryptonPanel1.ResumeLayout(false);
            kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cmbDisplayCurrency).EndInit();
            ((System.ComponentModel.ISupportInitialize)pnlMainDashboard).EndInit();
            pnlMainDashboard.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)groupTrades.Panel).EndInit();
            groupTrades.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)groupTrades).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTrades).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupMarket.Panel).EndInit();
            groupMarket.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)groupMarket).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMarket).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupAssets.Panel).EndInit();
            groupAssets.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)groupAssets).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAssets).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupBalance.Panel).EndInit();
            groupBalance.Panel.ResumeLayout(false);
            groupBalance.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)groupBalance).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonManager kryptonManager1;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonLabel lblWelcome;
        private Krypton.Toolkit.KryptonButton btnMenu;
        private Krypton.Toolkit.KryptonContextMenu kcmMenu;
        private Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
        private Krypton.Toolkit.KryptonContextMenuItem cmiShowDashboard;
        private Krypton.Toolkit.KryptonContextMenuItem cmiAddWallet;
        private Krypton.Toolkit.KryptonContextMenuItem cmiAddBalance;
        private Krypton.Toolkit.KryptonContextMenuSeparator kryptonContextMenuSeparator1;
        private Krypton.Toolkit.KryptonPanel pnlMainDashboard;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Krypton.Toolkit.KryptonHeaderGroup groupTrades;
        private Krypton.Toolkit.KryptonDataGridView dgvTrades;
        private Krypton.Toolkit.KryptonHeaderGroup groupMarket;
        private Krypton.Toolkit.KryptonDataGridView dgvMarket;
        private Krypton.Toolkit.KryptonHeaderGroup groupAssets;
        private Krypton.Toolkit.KryptonDataGridView dgvAssets;
        private Krypton.Toolkit.KryptonHeaderGroup groupBalance;
        private Krypton.Toolkit.KryptonLabel lblBalance;
        private Krypton.Toolkit.KryptonLabel lblDisplayCurrency;
        private Krypton.Toolkit.KryptonComboBox cmbDisplayCurrency;
        private System.Windows.Forms.Timer pnlUpdateTimer;
    }
}