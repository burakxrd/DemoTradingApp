using Krypton.Toolkit;
using Microsoft.Data.SqlClient; // Transaction için gerekli
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DemoTradingApp
{
    public partial class ExpensiveMarketForm : KryptonForm
    {
        private class MarketProduct
        {
            public string Name { get; set; } = string.Empty; // Null uyarılarını gidermek için başlangıç değeri atandı
            public decimal Price { get; set; }
            public string Currency { get; set; } = string.Empty;
            public string ImagePath { get; set; } = string.Empty;
        }
        // Gerekli private alanlar
        private readonly DashboardForm _owner;
        private readonly User _currentUser;
        private readonly DataTable _userAssets;
        private List<CartItem> _shoppingCart = new List<CartItem>();
        // Sepet öğesini temsil eden basit bir sınıf
        private class CartItem
        {
            // Null uyarılarını gidermek için başlangıç değerleri atandı
            public string ItemName { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public string CurrencyName { get; set; } = string.Empty;
            public int CurrencyAssetId { get; set; }
            public string ImagePath { get; set; } = string.Empty;
            public int WalletId { get; set; }
        }
        // Constructor
        public ExpensiveMarketForm(DashboardForm owner, User user, DataTable userAssets)
        {
            InitializeComponent();
            _owner = owner;
            _currentUser = user;
            _userAssets = userAssets;
        }

        private void ExpensiveMarketForm_Load(object sender, EventArgs e)
        {
            pnlCart.Visible = false;
            // Form yüklendiğinde ürünleri programatik olarak oluştur
            PopulateMarketItems();
        }
        private void PopulateMarketItems()
        {
            // ===== BAŞLANGIÇ: DÜZELTME =====
            // Paneli temizlemeden önce içindeki tüm eski kontrolleri düzgünce yok et
            while (flpMarketItems.Controls.Count > 0)
            {
                flpMarketItems.Controls[0].Dispose();
            }
            flpMarketItems.Controls.Clear(); // Koleksiyonu temizle
                                             // ===== BİTİŞ: DÜZELTME =====

            // ===== Ürünleri ve Fiyatları Buradan Yönetin! =====
            var products = new List<MarketProduct>
    {
        new MarketProduct { Name = "Lüks Saat", Price = 3131313131, Currency = "TRY" },
        new MarketProduct { Name = "Mazda MX-5", Price = 250000, Currency = "USD" },
        new MarketProduct { Name = "Özel Jet", Price = 5000000, Currency = "USD" },
        new MarketProduct { Name = "Titanik", Price = 2, Currency = "Bitcoin" },
        new MarketProduct { Name = "Cumhurbaşkanı Külliyesi", Price = 128000000000, Currency = "USD" },
    };

            flpMarketItems.Controls.Clear(); // Önce paneli temizle

            foreach (var product in products)
            {
                // Her ürün için bir GroupBox oluştur
                var group = new KryptonGroupBox
                {
                    AutoSize = true,
                    AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink,
                    Margin = new Padding(10),
                    MinimumSize = new Size(180, 0),
                    MaximumSize = new Size(180, 0),
                    Values = { Heading = "Ürün" }
                };

                string imagePath = System.IO.Path.Combine("Images", $"{product.Name}.png");
                // PictureBox oluştur
                var pic = new PictureBox
                {
                    BackColor = System.Drawing.Color.Gainsboro,
                    Size = new System.Drawing.Size(166, 132),
                    SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom,
                    ImageLocation = imagePath,
                    Dock = DockStyle.Top
                };

                // İsim Label'ı oluştur (metni manuel kır)
                string wrappedText = WrapText(product.Name, 15); // 15 karakterde bir satır kır
                var nameLabel = new KryptonLabel
                {
                    AutoSize = true,
                    MaximumSize = new Size(166, 0), // Genişlik 166 piksel ile sınırlı
                    StateCommon = { ShortText = { Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold) } },
                    Values = { Text = wrappedText }, // Kırılmış metni burada kullan
                    Dock = DockStyle.Bottom
                };
                nameLabel.Padding = new Padding(0, 5, 0, 5); // Üst ve alt boşluk

                // Fiyat Label'ı oluştur
                var priceLabel = new KryptonLabel
                {
                    AutoSize = true,
                    Values = { Text = $"{product.Price:N0} {product.Currency}" },
                    Dock = DockStyle.Bottom
                };

                // Buton oluştur
                var button = new KryptonButton
                {
                    Size = new System.Drawing.Size(166, 25),
                    Values = { Text = "Sepete Ekle" },
                    Tag = $"{product.Name};{product.Price};{product.Currency};{imagePath}",
                    Dock = DockStyle.Bottom
                };
                button.Click += AddToCart_Click;

                // Oluşturulan kontrolleri GroupBox'ın paneline ekle (doğru sırayla)
                group.Panel.Controls.Add(pic);      // En üstte resim
                group.Panel.Controls.Add(nameLabel); // Resmin altında isim
                group.Panel.Controls.Add(priceLabel); // İsimin altında fiyat
                group.Panel.Controls.Add(button);    // En altta buton

                // GroupBox'ın yüksekliğini dinamik olarak ayarla (otomatik olduğu için manuel ayara gerek yok)
                // AutoSize zaten yükseklik ayarını hallediyor

                // Hazır olan GroupBox'ı FlowLayoutPanel'e ekle
                flpMarketItems.Controls.Add(group);
            }
        }

        // Metni belirli bir karakter sayısında kıran yardımcı metod
        private string WrapText(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
                return text;

            var words = text.Split(' ');
            var lines = new List<string>();
            var currentLine = new StringBuilder();

            foreach (var word in words)
            {
                if (currentLine.Length + word.Length + (currentLine.Length > 0 ? 1 : 0) <= maxLength)
                {
                    if (currentLine.Length > 0)
                        currentLine.Append(" ");
                    currentLine.Append(word);
                }
                else
                {
                    lines.Add(currentLine.ToString());
                    currentLine = new StringBuilder(word);
                }
            }

            if (currentLine.Length > 0)
                lines.Add(currentLine.ToString());

            return string.Join("\n", lines);
        }
        // Tüm "Sepete Ekle" butonları için ortak olay yöneticisi
        private void AddToCart_Click(object? sender, EventArgs e)
        {
            var button = sender as KryptonButton;
            if (button?.Tag is not string tagString) return;

            var tagInfo = tagString.Split(';');
            if (tagInfo.Length != 4) return;

            string itemName = tagInfo[0];
            if (!decimal.TryParse(tagInfo[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal itemPrice)) return;
            string currencyName = tagInfo[2];
            string imagePath = tagInfo[3];

            DataRow? assetRow = _userAssets.AsEnumerable().FirstOrDefault(r => r.Field<string>("Varlık")?.Trim().Equals(currencyName, StringComparison.OrdinalIgnoreCase) == true);

            if (assetRow != null)
            {
                var selectedAsset = new
                {
                    AssetName = assetRow.Field<string>("Varlık") ?? "",
                    AssetTypeId = assetRow.Field<int>("asset_type_id"),
                    WalletId = assetRow.Field<int>("wallet_id")
                };

                _shoppingCart.Add(new CartItem
                {
                    ItemName = itemName,
                    Price = itemPrice,
                    CurrencyName = selectedAsset.AssetName,
                    CurrencyAssetId = selectedAsset.AssetTypeId,
                    WalletId = selectedAsset.WalletId,
                    ImagePath = imagePath
                });

                KryptonMessageBox.Show($"{itemName} sepete eklendi!");

                // ===== YENİ: Ürün eklenince sepet görünürse anında güncelle =====
                if (pnlCart.Visible)
                {
                    UpdateCartDisplay();
                }
            }
            else
            {
                KryptonMessageBox.Show($"Bu ürünü alabilmek için cüzdanınızda '{currencyName}' bulunmuyor.", "Hata");
            }
        }
        private void UpdateCartDisplay()
        {
            // ===== BAŞLANGIÇ: DÜZELTME =====
            // Paneli temizlemeden önce içindeki tüm eski kontrolleri düzgünce yok et
            while (flpCartItems.Controls.Count > 0)
            {
                flpCartItems.Controls[0].Dispose();
            }
            flpCartItems.Controls.Clear();
            // ===== BİTİŞ: DÜZELTME =====
            if (_shoppingCart.Any())
            {
                var totals = _shoppingCart.GroupBy(item => item.CurrencyName)
                                          .Select(group => new { Currency = group.Key, Total = group.Sum(item => item.Price) })
                                          .ToList();
                lblCartTotal.Text = "Toplam: " + string.Join(", ", totals.Select(t => $"{t.Total:N0} {t.Currency}"));

                foreach (var item in _shoppingCart)
                {
                    var itemPanel = new KryptonPanel
                    {
                        Size = new Size(flpCartItems.ClientSize.Width - 10, 30),
                        StateCommon = { Color1 = Color.Transparent }
                    };

                    var removeButton = new KryptonButton
                    {
                        Text = "X",
                        Size = new Size(25, 25),
                        Dock = DockStyle.Right,
                        Tag = item
                    };
                    removeButton.Click += RemoveFromCart_Click;

                    var cartItemLabel = new KryptonLabel
                    {
                        Text = $"{item.ItemName} - {item.Price:N0} {item.CurrencyName}",
                        Dock = DockStyle.Fill,
                        StateCommon = { ShortText = { TextV = PaletteRelativeAlign.Center } }
                    };

                    itemPanel.Controls.Add(cartItemLabel);
                    itemPanel.Controls.Add(removeButton);
                    flpCartItems.Controls.Add(itemPanel);
                }
            }
            else
            {
                lblCartTotal.Text = "Toplam: 0";
                flpCartItems.Controls.Add(new KryptonLabel { Text = "Sepetiniz şu anda boş.", AutoSize = true });
            }
        }


        private void RemoveFromCart_Click(object? sender, EventArgs e)
        {
            if ((sender as KryptonButton)?.Tag is CartItem itemToRemove)
            {
                _shoppingCart.Remove(itemToRemove);
                // Sepet görünür olduğu için sadece güncelle
                UpdateCartDisplay();
            }
        }
        private void btnShowCart_Click(object? sender, EventArgs e)
        {
            UpdateCartDisplay(); // Sepet içeriğini doldur/güncelle

            // === YENİ: Ödeme Yöntemi ComboBox'ını Doldurma ===
            try
            {
                var wallets = _userAssets.AsEnumerable()
                    .Select(row => new {
                        WalletName = row.Field<string>("Cüzdan"),
                        WalletId = row.Field<int>("wallet_id")
                    })
                    .Distinct()
                    .ToList();

                cmbPaymentMethod.DataSource = wallets;
                cmbPaymentMethod.DisplayMember = "WalletName";
                cmbPaymentMethod.ValueMember = "WalletId";
                cmbPaymentMethod.SelectedIndex = -1;
                cmbPaymentMethod.Text = "Lütfen bir cüzdan seçin...";
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Ödeme yöntemleri yüklenirken hata: " + ex.Message);
            }
            // ===============================================

            pnlCart.Visible = true;
            btnShowCart.Text = "Sepeti Gizle";
            btnShowCart.Click -= btnShowCart_Click;
            btnShowCart.Click += btnHideCart_Click;
        }
        private void btnHideCart_Click(object? sender, EventArgs e)
        {
            pnlCart.Visible = false;
            btnShowCart.Text = "Sepeti Göster"; // Buton metnini eski haline getir

            // Olayları eski haline getir: Artık bu buton sepeti gösterecek
            btnShowCart.Click -= btnHideCart_Click;
            btnShowCart.Click += btnShowCart_Click;
        }

        private async void btnConfirmPurchase_Click(object sender, EventArgs e)
        {
            if (!_shoppingCart.Any())
            {
                KryptonMessageBox.Show("Satın almak için sepetinizde ürün bulunmuyor.", "Boş Sepet", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                return;
            }

            if (cmbPaymentMethod.SelectedValue == null)
            {
                KryptonMessageBox.Show("Lütfen bir ödeme yöntemi (cüzdan) seçin.", "Eksik Bilgi", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                return;
            }

            int selectedWalletId = (int)cmbPaymentMethod.SelectedValue;

            // === YENİ: Kapsamlı Bakiye Kontrolü ===
            bool allFundsAvailable = true;
            var requiredFunds = _shoppingCart
                .GroupBy(item => new { item.CurrencyAssetId, item.CurrencyName })
                .Select(g => new {
                    AssetId = g.Key.CurrencyAssetId,
                    Currency = g.Key.CurrencyName,
                    TotalRequired = g.Sum(i => i.Price)
                });

            StringBuilder missingFundsMessage = new StringBuilder("Seçili cüzdanda yetersiz bakiye:\n");

            foreach (var fund in requiredFunds)
            {
                DataRow? assetRow = _userAssets.AsEnumerable().FirstOrDefault(r =>
                    r.Field<int>("wallet_id") == selectedWalletId &&
                    r.Field<int>("asset_type_id") == fund.AssetId);

                if (assetRow == null)
                {
                    allFundsAvailable = false;
                    missingFundsMessage.AppendLine($" - Cüzdanınızda hiç {fund.Currency} bulunmuyor.");
                }
                else if (assetRow.Field<decimal>("Miktar") < fund.TotalRequired)
                {
                    allFundsAvailable = false;
                    missingFundsMessage.AppendLine($" - {fund.TotalRequired:N2} {fund.Currency} gerekli, ancak {assetRow.Field<decimal>("Miktar"):N2} mevcut.");
                }
            }

            if (!allFundsAvailable)
            {
                KryptonMessageBox.Show(missingFundsMessage.ToString(), "Yetersiz Bakiye", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                return;
            }
            // =====================================

            // Bakiye yeterliyse, satın alma işlemine devam et...
            using (var connection = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in _shoppingCart)
                        {
                            bool success = DatabaseHelper.DeductBalance(connection, transaction, selectedWalletId, item.CurrencyAssetId, item.Price);
                            if (!success) throw new Exception($"{item.CurrencyName} bakiyesi düşülürken bir sorun oluştu.");

                            DatabaseHelper.RecordPurchase(connection, transaction, _currentUser.UserId, item.ItemName, 1, item.Price, item.CurrencyAssetId, item.ImagePath);
                        }

                        transaction.Commit();
                        KryptonMessageBox.Show("Satın alma işlemi başarıyla tamamlandı!", "Başarılı", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);

                        _shoppingCart.Clear();
                        pnlCart.Visible = false;

                        await _owner.LoadAllData();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        KryptonMessageBox.Show("Satın alma sırasında bir hata oluştu: " + ex.Message, "İşlem Başarısız", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}