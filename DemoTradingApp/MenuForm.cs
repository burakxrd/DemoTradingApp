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
        private DashboardForm _owner;

        /// <summary>
        /// Initializes a new instance of the MenuForm class.
        /// </summary>
        /// <param name="owner">The dashboard form that owns this menu</param>
        public MenuForm(DashboardForm owner)
        {
            InitializeComponent();
            _owner = owner;
        }

        private void MenuForm_Deactivate(object sender, System.EventArgs e)
        {
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
            _owner.Logout();
        }
    }
}
