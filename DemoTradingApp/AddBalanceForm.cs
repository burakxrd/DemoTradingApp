using Krypton.Toolkit;
using System.Data;

namespace DemoTradingApp
{
    public partial class AddBalanceForm : KryptonForm
    {
        private readonly User _currentUser;
        private readonly DashboardForm _dashboard;

        private class UserAssetDisplay
        {
            public string DisplayText { get; set; } = "";
            public int WalletId { get; set; }
            public int AssetTypeId { get; set; }
            public decimal Amount { get; set; }
        }

        /// <summary>
        /// Initializes a new instance of the AddBalanceForm class.
        /// </summary>
        /// <param name="user">The current user</param>
        /// <param name="dashboard">The dashboard form reference</param>
        public AddBalanceForm(User user, DashboardForm dashboard)
        {
            InitializeComponent();
            _currentUser = user;
            _dashboard = dashboard;
        }

        private void AddBalanceForm_Load(object sender, EventArgs e)
        {
            LoadUserWallets();
            LoadCurrencies();
            LoadUserAssetsForRemoval();
        }

        private void LoadUserWallets()
        {
            try
            {
                cmbWallets.DataSource = DatabaseHelper.GetWalletsForUser(_currentUser.UserId);
                cmbWallets.DisplayMember = "wallet_name";
                cmbWallets.ValueMember = "wallet_id";
                cmbWallets.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(Properties.Resources.WalletLoadError + ex.Message, Properties.Resources.DatabaseError, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
        }

        private void LoadCurrencies()
        {
            cmbCurrency.Items.Clear();
            cmbCurrency.Items.AddRange(new object[] { "TRY", "USD", "EUR" });
            cmbCurrency.SelectedIndex = 0;
        }

        private void LoadUserAssetsForRemoval()
        {
            try
            {
                DataTable userAssetsTable = DatabaseHelper.GetUserAssetsWithValues(_currentUser.UserId);
                var displayList = new List<UserAssetDisplay>();

                foreach (DataRow row in userAssetsTable.Rows)
                {
                    decimal amount = Convert.ToDecimal(row["Amount"]);
                    if (amount > 0)
                    {
                        displayList.Add(new UserAssetDisplay
                        {
                            DisplayText = $"{row["Wallet"]} - {row["Asset"]} ({amount:N6})",
                            WalletId = Convert.ToInt32(row["wallet_id"]),
                            AssetTypeId = Convert.ToInt32(row["asset_type_id"]),
                            Amount = amount
                        });
                    }
                }

                cmbAssetToRemove.DataSource = displayList;
                cmbAssetToRemove.DisplayMember = "DisplayText";
                cmbAssetToRemove.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(Properties.Resources.AssetLoadError + ex.Message, Properties.Resources.DatabaseError, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
        }

        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cmbWallets.SelectedValue == null) 
            { 
                KryptonMessageBox.Show(Properties.Resources.SelectWallet, Properties.Resources.MissingInfo, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                return; 
            }
            if (numAmount.Value <= 0) 
            { 
                KryptonMessageBox.Show(Properties.Resources.EnterAmountGreaterThanZero, Properties.Resources.InvalidAmount, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                return; 
            }
            if (cmbCurrency.SelectedItem == null) 
            { 
                KryptonMessageBox.Show(Properties.Resources.SelectCurrency, Properties.Resources.MissingInfo, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                return; 
            }

            try
            {
                DatabaseHelper.AddOrUpdateAssetBalance((int)cmbWallets.SelectedValue, numAmount.Value, cmbCurrency.Text);
                KryptonMessageBox.Show(Properties.Resources.BalanceAddedSuccess, Properties.Resources.Success, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                await _dashboard.LoadAllData();
                this.Close();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(Properties.Resources.BalanceAddError + ex.Message, Properties.Resources.DatabaseError, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
        }

        private async void btnRemoveBalance_Click(object sender, EventArgs e)
        {
            if (cmbAssetToRemove.SelectedItem is not UserAssetDisplay selectedAsset)
            {
                KryptonMessageBox.Show(Properties.Resources.SelectAssetToRemove, Properties.Resources.MissingInfo, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                return;
            }
            if (numAmountToRemove.Value <= 0)
            {
                KryptonMessageBox.Show(Properties.Resources.EnterAmountGreaterThanZero, Properties.Resources.InvalidAmount, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                return;
            }
            if (numAmountToRemove.Value > selectedAsset.Amount)
            {
                KryptonMessageBox.Show(Properties.Resources.AmountExceedsBalance, Properties.Resources.InsufficientBalance, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                return;
            }

            var confirmation = KryptonMessageBox.Show(
                string.Format(Properties.Resources.RemoveConfirmationMessage, selectedAsset.DisplayText, numAmountToRemove.Value),
                Properties.Resources.RemoveConfirmationTitle, KryptonMessageBoxButtons.YesNo, KryptonMessageBoxIcon.Warning);

            if (confirmation == DialogResult.Yes)
            {
                try
                {
                    bool success = DatabaseHelper.DeductBalance(selectedAsset.WalletId, selectedAsset.AssetTypeId, numAmountToRemove.Value); if (success)
                    {
                        KryptonMessageBox.Show(Properties.Resources.BalanceRemovedSuccess, Properties.Resources.Success, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                        await _dashboard.LoadAllData();
                        this.Close();
                    }
                    else
                    {
                        KryptonMessageBox.Show(Properties.Resources.BalanceRemoveFailed, Properties.Resources.OperationError, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    KryptonMessageBox.Show(Properties.Resources.BalanceRemoveError + ex.Message, Properties.Resources.DatabaseError, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}