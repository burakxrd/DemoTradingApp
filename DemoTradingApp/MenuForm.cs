using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DemoTradingApp;

namespace DemoTradingApp
{
    public partial class MenuForm : KryptonForm
    {
        // Ana forma geri bildirim yapmak için
        private DashboardForm _owner;
        public MenuForm(DashboardForm owner)
        {
            InitializeComponent();
            _owner = owner;
        }

        private void MenuForm_Deactivate(object sender, System.EventArgs e)
        {
            // Form odaktan çıktığı an (başka yere tıklandığında) kendini kapatır.
            this.Close();
        }
        private void btnShowDashboard_Click(object sender, System.EventArgs e)
        {
            _owner.Activate();
            this.Close();
        }
        private void btnAddWallet_Click(object sender, System.EventArgs e)
        {
            this.Close();
            _owner.ShowAddWalletForm();
        }
        private void btnAddBalance_Click(object sender, System.EventArgs e)
        {
            this.Close();
            _owner.ShowAddBalanceForm();
        }

        private void btnTrade_Click(object? sender, EventArgs e)
        {
            this.Close();
            _owner.ShowTradeForm();
        }

        private void btnShowMarket_Click(object sender, EventArgs e)
        {
            this.Close();
            _owner.ShowMarketForm();
            _owner.Activate();
        }

        private void btnShowPossessions_Click(object sender, EventArgs e)
        {
            this.Close();
            _owner.ShowPossessionsForm();
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            _owner.Logout(); // Dashboard'daki Logout metodunu çağır
        }
    }
}
