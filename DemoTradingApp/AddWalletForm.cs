using Krypton.Toolkit;
using System;
using System.Data;
using System.Windows.Forms;

namespace DemoTradingApp
{
    public partial class AddWalletForm : KryptonForm
    {
        private readonly User _currentUser;
        private readonly DashboardForm _dashboard;

        /// <summary>
        /// Initializes a new instance of the AddWalletForm class.
        /// </summary>
        /// <param name="user">The current user.</param>
        /// <param name="dashboard">The dashboard form to refresh after changes.</param>
        public AddWalletForm(User user, DashboardForm dashboard)
        {
            InitializeComponent();
            _currentUser = user;
            _dashboard = dashboard;
        }

        /// <summary>
        /// Handles the form load event and populates wallet type and existing wallet lists.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void AddWalletForm_Load(object sender, EventArgs e)
        {
            LoadWalletTypes();
            LoadExistingWallets();
        }

        private void LoadWalletTypes()
        {
            try
            {
                cmbWalletType.DataSource = DatabaseHelper.GetWalletTypes();
                cmbWalletType.DisplayMember = "type_name";
                cmbWalletType.ValueMember = "wallet_type_id";
                cmbWalletType.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(Properties.Resources.WalletTypeLoadError + ex.Message, Properties.Resources.DatabaseError, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
        }

        private void LoadExistingWallets()
        {
            try
            {
                cmbExistingWallets.DataSource = DatabaseHelper.GetWalletsForUser(_currentUser.UserId);
                cmbExistingWallets.DisplayMember = "wallet_name";
                cmbExistingWallets.ValueMember = "wallet_id";
                cmbExistingWallets.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(Properties.Resources.ExistingWalletsLoadError + ex.Message, Properties.Resources.DatabaseError, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the confirm button click event to create a new wallet.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWalletName.Text))
            {
                KryptonMessageBox.Show(Properties.Resources.WalletNameRequired, Properties.Resources.MissingInfo, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                return;
            }
            if (cmbWalletType.SelectedValue == null)
            {
                KryptonMessageBox.Show(Properties.Resources.SelectWalletType, Properties.Resources.MissingInfo, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = DatabaseHelper.CreateWallet(_currentUser.UserId, txtWalletName.Text.Trim(), (int)cmbWalletType.SelectedValue);

                if (success)
                {
                    KryptonMessageBox.Show(Properties.Resources.WalletCreatedSuccess, Properties.Resources.Success, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                    await _dashboard.LoadAllData();
                    this.Close();
                }
                else
                {
                    KryptonMessageBox.Show(Properties.Resources.WalletCreateError, Properties.Resources.OperationError, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(Properties.Resources.CriticalDatabaseError + ex.Message, Properties.Resources.Error, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the delete button click event to delete the selected wallet.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (cmbExistingWallets.SelectedValue == null)
            {
                KryptonMessageBox.Show(Properties.Resources.SelectWalletToDelete, Properties.Resources.MissingInfo, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                return;
            }

            string walletNameToDelete = cmbExistingWallets.Text;
            var confirmation = KryptonMessageBox.Show(
                string.Format(Properties.Resources.DeleteWalletConfirmation, walletNameToDelete),
                Properties.Resources.DeleteConfirmationTitle,
                KryptonMessageBoxButtons.YesNo,
                KryptonMessageBoxIcon.Warning);

            if (confirmation == DialogResult.Yes)
            {
                try
                {
                    int walletIdToDelete = (int)cmbExistingWallets.SelectedValue;
                    bool success = DatabaseHelper.DeleteWallet(walletIdToDelete);

                    if (success)
                    {
                        KryptonMessageBox.Show(Properties.Resources.WalletDeletedSuccess, Properties.Resources.Success, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                        await _dashboard.LoadAllData();
                        this.Close();
                    }
                    else
                    {
                        KryptonMessageBox.Show(Properties.Resources.WalletDeleteError, Properties.Resources.OperationError, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    KryptonMessageBox.Show(Properties.Resources.CriticalDatabaseError + ex.Message, Properties.Resources.Error, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Handles the cancel button click event to close the form.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}