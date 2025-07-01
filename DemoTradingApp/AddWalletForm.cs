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

        public AddWalletForm(User user, DashboardForm dashboard)
        {
            InitializeComponent();
            _currentUser = user;
            _dashboard = dashboard;
        }

        private void AddWalletForm_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde her iki ComboBox'ı da doldur
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
                KryptonMessageBox.Show("Cüzdan türleri yüklenirken bir hata oluştu: " + ex.Message, "Veritabanı Hatası", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
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
                KryptonMessageBox.Show("Mevcut cüzdanlar yüklenirken bir hata oluştu: " + ex.Message, "Veritabanı Hatası", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
        }

        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWalletName.Text))
            {
                KryptonMessageBox.Show("Cüzdan adı boş bırakılamaz.", "Eksik Bilgi", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                return;
            }
            if (cmbWalletType.SelectedValue == null)
            {
                KryptonMessageBox.Show("Lütfen bir cüzdan türü seçin.", "Eksik Bilgi", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = DatabaseHelper.CreateWallet(_currentUser.UserId, txtWalletName.Text.Trim(), (int)cmbWalletType.SelectedValue);

                if (success)
                {
                    KryptonMessageBox.Show("Cüzdan başarıyla oluşturuldu.", "Başarılı", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                    await _dashboard.LoadAllData();
                    this.Close();
                }
                else
                {
                    KryptonMessageBox.Show("Cüzdan oluşturulurken bir hata oluştu.", "İşlem Hatası", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Kritik veritabanı hatası: " + ex.Message, "Hata", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (cmbExistingWallets.SelectedValue == null)
            {
                KryptonMessageBox.Show("Lütfen silmek için bir cüzdan seçin.", "Eksik Bilgi", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                return;
            }

            string walletNameToDelete = cmbExistingWallets.Text;
            var confirmation = KryptonMessageBox.Show(
                $"'{walletNameToDelete}' adlı cüzdanı silmek istediğinizden emin misiniz?\n\nBU İŞLEM GERİ ALINAMAZ VE CÜZDAN İÇİNDEKİ TÜM VARLIKLAR SİLİNİR!",
                "Silme Onayı",
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
                        KryptonMessageBox.Show("Cüzdan başarıyla silindi.", "Başarılı", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                        await _dashboard.LoadAllData();
                        this.Close();
                    }
                    else
                    {
                        KryptonMessageBox.Show("Cüzdan silinirken bir hata oluştu.", "İşlem Hatası", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    KryptonMessageBox.Show("Kritik veritabanı hatası: " + ex.Message, "Hata", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}