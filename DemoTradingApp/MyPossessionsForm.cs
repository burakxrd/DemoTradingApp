using Krypton.Toolkit;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DemoTradingApp
{
    public partial class MyPossessionsForm : KryptonForm
    {
        private readonly int _userId;

        public MyPossessionsForm(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void MyPossessionsForm_Load(object sender, EventArgs e)
        {
            LoadPossessions();
        }

        private void LoadPossessions()
        {
            // ===== BAŞLANGIÇ: DÜZELTME =====
            // Paneli temizlemeden önce içindeki tüm eski kontrolleri düzgünce yok et
            while (flpPossessions.Controls.Count > 0)
            {
                flpPossessions.Controls[0].Dispose();
            }
            flpPossessions.Controls.Clear();
            // ===== BİTİŞ: DÜZELTME =====
            DataTable possessions = DatabaseHelper.GetUserPossessions(_userId);

            foreach (DataRow row in possessions.Rows)
            {
                // Her ürün için bir ana kutu (GroupBox) oluştur
                var itemBox = new KryptonGroupBox
                {
                    Size = new Size(180, 260), // Boyutu biraz artırdık
                    Margin = new Padding(10),
                    Values = { Heading = row["Ürün Adı"].ToString() }
                };

                // Resmi gösterecek PictureBox
                var pic = new PictureBox
                {
                    ImageLocation = row["image_path"].ToString(),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.Gainsboro,
                    Dock = DockStyle.Fill
                };

                // Silme butonu
                var deleteButton = new KryptonButton
                {
                    Text = "Sil",
                    Dock = DockStyle.Bottom,
                    // Silinecek öğenin ID'sini butonun Tag özelliğinde saklıyoruz
                    Tag = row["purchase_id"]
                };
                deleteButton.Click += DeletePossession_Click; // Click olayını bağlıyoruz

                // Bilgi etiketi
                var infoLabel = new KryptonLabel
                {
                    Text = $"{row["Adet"]} adet - {Convert.ToDecimal(row["Alış Fiyatı"]):N0} {row["Para Birimi"]}",
                    Dock = DockStyle.Bottom,
                    StateCommon = { ShortText = { TextH = PaletteRelativeAlign.Center } }
                };

                // Kontrolleri GroupBox'ın paneline ekle
                itemBox.Panel.Controls.Add(pic);
                itemBox.Panel.Controls.Add(infoLabel);
                itemBox.Panel.Controls.Add(deleteButton);

                // Hazır olan kartı ana panele ekle
                flpPossessions.Controls.Add(itemBox);
            }
        }

        private void DeletePossession_Click(object? sender, EventArgs e)
        {
            var button = sender as KryptonButton;
            if (button?.Tag == null) return;

            int purchaseId = Convert.ToInt32(button.Tag);

            var confirmation = KryptonMessageBox.Show(
                "Bu varlığı kalıcı olarak silmek istediğinizden emin misiniz?",
                "Silme Onayı",
                KryptonMessageBoxButtons.YesNo,
                KryptonMessageBoxIcon.Warning);

            if (confirmation == DialogResult.No) return;

            try
            {
                bool success = DatabaseHelper.DeletePossession(purchaseId);
                if (success)
                {
                    KryptonMessageBox.Show("Varlık başarıyla silindi.", "Başarılı", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                    // Arayüzü güncellemek için listeyi yeniden yükle
                    LoadPossessions();
                }
                else
                {
                    KryptonMessageBox.Show("Varlık silinirken bir hata oluştu.", "Hata", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Veritabanı hatası: " + ex.Message, "Kritik Hata", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
        }
    }
}