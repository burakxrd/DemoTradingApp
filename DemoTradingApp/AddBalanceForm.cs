using Krypton.Toolkit;
using System.Data;

namespace DemoTradingApp
{
    public partial class AddBalanceForm : KryptonForm
    {
        private readonly User _currentUser;
        private readonly DashboardForm _dashboard;

        // Bakiye çıkarma ComboBox'ı için özel bir sınıf
        private class UserAssetDisplay
        {
            public string DisplayText { get; set; } = "";
            public int WalletId { get; set; }
            public int AssetTypeId { get; set; }
            public decimal Amount { get; set; }
        }

        public AddBalanceForm(User user, DashboardForm dashboard)
        {
            InitializeComponent();
            _currentUser = user;
            _dashboard = dashboard;
        }

        private void AddBalanceForm_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde tüm listeleri doldur
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
                KryptonMessageBox.Show("Cüzdanlar yüklenirken bir hata oluştu: " + ex.Message, "Veritabanı Hatası", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
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
                    decimal amount = Convert.ToDecimal(row["Miktar"]);
                    if (amount > 0) // Sadece miktarı olan varlıkları listele
                    {
                        displayList.Add(new UserAssetDisplay
                        {
                            DisplayText = $"{row["Cüzdan"]} - {row["Varlık"]} ({amount:N6})",
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
                KryptonMessageBox.Show("Mevcut varlıklar yüklenirken bir hata oluştu: " + ex.Message, "Veritabanı Hatası", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
        }

        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cmbWallets.SelectedValue == null) { /* ... hata mesajı ... */ return; }
            if (numAmount.Value <= 0) { /* ... hata mesajı ... */ return; }
            if (cmbCurrency.SelectedItem == null) { /* ... hata mesajı ... */ return; }

            try
            {
                DatabaseHelper.AddOrUpdateAssetBalance((int)cmbWallets.SelectedValue, numAmount.Value, cmbCurrency.Text);
                KryptonMessageBox.Show("Bakiye başarıyla eklendi.", "Başarılı", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                await _dashboard.LoadAllData();
                this.Close();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Bakiye eklenirken bir hata oluştu: " + ex.Message, "Veritabanı Hatası", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
        }

        private async void btnRemoveBalance_Click(object sender, EventArgs e)
        {
            if (cmbAssetToRemove.SelectedItem is not UserAssetDisplay selectedAsset)
            {
                KryptonMessageBox.Show("Lütfen bakiye çıkarılacak bir varlık seçin.", "Eksik Bilgi", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                return;
            }
            if (numAmountToRemove.Value <= 0)
            {
                KryptonMessageBox.Show("Lütfen 0'dan büyük bir miktar girin.", "Geçersiz Miktar", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                return;
            }
            if (numAmountToRemove.Value > selectedAsset.Amount)
            {
                KryptonMessageBox.Show("Çekmek istediğiniz miktar, mevcut bakiyeden fazla olamaz.", "Yetersiz Bakiye", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                return;
            }

            var confirmation = KryptonMessageBox.Show(
                $"{selectedAsset.DisplayText} hesabından {numAmountToRemove.Value} birim bakiye çıkarmak istediğinizden emin misiniz?",
                "Çıkarma Onayı", KryptonMessageBoxButtons.YesNo, KryptonMessageBoxIcon.Warning);

            if (confirmation == DialogResult.Yes)
            {
                try
                {
                    bool success = DatabaseHelper.DeductBalance(selectedAsset.WalletId, selectedAsset.AssetTypeId, numAmountToRemove.Value);

                    if (success)
                    {
                        KryptonMessageBox.Show("Bakiye başarıyla çıkarıldı.", "Başarılı", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                        await _dashboard.LoadAllData();
                        this.Close();
                    }
                    else
                    {
                        KryptonMessageBox.Show("Bakiye çıkarma işlemi başarısız oldu. Yetersiz bakiye olabilir.", "İşlem Hatası", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    KryptonMessageBox.Show("Bakiye çıkarılırken bir hata oluştu: " + ex.Message, "Veritabanı Hatası", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}