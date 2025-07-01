namespace DemoTradingApp
{
    partial class MenuForm
    {
        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            btnSignOut = new Krypton.Toolkit.KryptonButton();
            btnShowPossessions = new Krypton.Toolkit.KryptonButton();
            btnShowMarket = new Krypton.Toolkit.KryptonButton();
            btnTrade = new Krypton.Toolkit.KryptonButton();
            btnAddBalance = new Krypton.Toolkit.KryptonButton();
            btnAddWallet = new Krypton.Toolkit.KryptonButton();
            btnShowDashboard = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).BeginInit();
            kryptonPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // kryptonPanel1
            // 
            kryptonPanel1.Controls.Add(btnSignOut);
            kryptonPanel1.Controls.Add(btnShowPossessions);
            kryptonPanel1.Controls.Add(btnShowMarket);
            kryptonPanel1.Controls.Add(btnTrade);
            kryptonPanel1.Controls.Add(btnAddBalance);
            kryptonPanel1.Controls.Add(btnAddWallet);
            kryptonPanel1.Controls.Add(btnShowDashboard);
            kryptonPanel1.Dock = DockStyle.Fill;
            kryptonPanel1.Location = new Point(0, 0);
            kryptonPanel1.Margin = new Padding(3, 4, 3, 4);
            kryptonPanel1.Name = "kryptonPanel1";
            kryptonPanel1.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.ControlClient;
            kryptonPanel1.Size = new Size(134, 359);
            kryptonPanel1.TabIndex = 0;
            // 
            // btnSignOut
            // 
            btnSignOut.Location = new Point(0, 312);
            btnSignOut.Name = "btnSignOut";
            btnSignOut.Size = new Size(134, 47);
            btnSignOut.TabIndex = 0;
            btnSignOut.Values.DropDownArrowColor = Color.Empty;
            btnSignOut.Values.Text = "Çıkış Yap";
            btnSignOut.Click += btnSignOut_Click;
            // 
            // btnShowPossessions
            // 
            btnShowPossessions.Dock = DockStyle.Top;
            btnShowPossessions.Location = new Point(0, 252);
            btnShowPossessions.Margin = new Padding(3, 4, 3, 4);
            btnShowPossessions.Name = "btnShowPossessions";
            btnShowPossessions.Size = new Size(134, 63);
            btnShowPossessions.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnShowPossessions.TabIndex = 6;
            btnShowPossessions.Values.DropDownArrowColor = Color.Empty;
            btnShowPossessions.Values.Text = "Sahip Olunan    \r\n    Varlıklar";
            btnShowPossessions.Click += btnShowPossessions_Click;
            // 
            // btnShowMarket
            // 
            btnShowMarket.Dock = DockStyle.Top;
            btnShowMarket.Location = new Point(0, 200);
            btnShowMarket.Margin = new Padding(3, 4, 3, 4);
            btnShowMarket.Name = "btnShowMarket";
            btnShowMarket.Size = new Size(134, 52);
            btnShowMarket.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnShowMarket.TabIndex = 5;
            btnShowMarket.Values.DropDownArrowColor = Color.Empty;
            btnShowMarket.Values.Text = "Market";
            btnShowMarket.Click += btnShowMarket_Click;
            // 
            // btnTrade
            // 
            btnTrade.Dock = DockStyle.Top;
            btnTrade.Location = new Point(0, 150);
            btnTrade.Margin = new Padding(3, 4, 3, 4);
            btnTrade.Name = "btnTrade";
            btnTrade.Size = new Size(134, 50);
            btnTrade.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnTrade.TabIndex = 4;
            btnTrade.Values.DropDownArrowColor = Color.Empty;
            btnTrade.Values.Text = "Trade Yap";
            btnTrade.Click += btnTrade_Click;
            // 
            // btnAddBalance
            // 
            btnAddBalance.Dock = DockStyle.Top;
            btnAddBalance.Location = new Point(0, 100);
            btnAddBalance.Margin = new Padding(3, 4, 3, 4);
            btnAddBalance.Name = "btnAddBalance";
            btnAddBalance.Size = new Size(134, 50);
            btnAddBalance.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnAddBalance.TabIndex = 2;
            btnAddBalance.Values.DropDownArrowColor = Color.Empty;
            btnAddBalance.Values.Text = "Bakiye Ekle/Sil";
            btnAddBalance.Click += btnAddBalance_Click;
            // 
            // btnAddWallet
            // 
            btnAddWallet.Dock = DockStyle.Top;
            btnAddWallet.Location = new Point(0, 50);
            btnAddWallet.Margin = new Padding(3, 4, 3, 4);
            btnAddWallet.Name = "btnAddWallet";
            btnAddWallet.Size = new Size(134, 50);
            btnAddWallet.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnAddWallet.TabIndex = 1;
            btnAddWallet.Values.DropDownArrowColor = Color.Empty;
            btnAddWallet.Values.Text = "Cüzdan Ekle/Sil";
            btnAddWallet.Click += btnAddWallet_Click;
            // 
            // btnShowDashboard
            // 
            btnShowDashboard.Dock = DockStyle.Top;
            btnShowDashboard.Location = new Point(0, 0);
            btnShowDashboard.Margin = new Padding(3, 4, 3, 4);
            btnShowDashboard.Name = "btnShowDashboard";
            btnShowDashboard.Size = new Size(134, 50);
            btnShowDashboard.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Center;
            btnShowDashboard.TabIndex = 0;
            btnShowDashboard.Values.DropDownArrowColor = Color.Empty;
            btnShowDashboard.Values.Text = "Ana Panel";
            btnShowDashboard.Click += btnShowDashboard_Click;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(134, 359);
            Controls.Add(kryptonPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(150, 375);
            Name = "MenuForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Deactivate += MenuForm_Deactivate;
            ((System.ComponentModel.ISupportInitialize)kryptonPanel1).EndInit();
            kryptonPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private Krypton.Toolkit.KryptonButton btnShowDashboard;
        private Krypton.Toolkit.KryptonButton btnAddBalance;
        private Krypton.Toolkit.KryptonButton btnAddWallet;
        private Krypton.Toolkit.KryptonButton btnTrade;
        private Krypton.Toolkit.KryptonButton btnShowPossessions;
        private Krypton.Toolkit.KryptonButton btnShowMarket;
        private Krypton.Toolkit.KryptonButton btnSignOut;
    }
}