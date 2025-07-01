using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DemoTradingApp.DashboardForm;

namespace DemoTradingApp
{
    public partial class TradesForm : KryptonForm
    {
        private readonly DashboardForm _owner;
        private readonly User _currentUser;
        private readonly DataTable _userAssets;
        private readonly Dictionary<string, PriceInfo> _marketPricesData;
        private readonly PriceInfo? _exchangeRates;

        private DataTable? _cashAssetsTable;
        private DataTable? _cryptoAssetsTable;

        public TradesForm(DashboardForm owner, User user, DataTable userAssets, Dictionary<string, PriceInfo> marketPricesData, PriceInfo? exchangeRates)
        {
            InitializeComponent();
            _owner = owner;
            _currentUser = user;
            _userAssets = userAssets;
            _marketPricesData = marketPricesData;
            _exchangeRates = exchangeRates;
            this.kryptonManager1.GlobalPaletteMode = PaletteMode.SparkleBlue;
        }

        private void TradesForm_Load(object? sender, EventArgs e)
        {
            kryptonLabel1.Location = new System.Drawing.Point(22, 128);
            cmbBaseCurrency.Location = new System.Drawing.Point(160, 125);
            lblBalance.Location = new System.Drawing.Point(160, 155);
            kryptonLabel4.Location = new System.Drawing.Point(34, 198);
            cmbQuoteAsset.Location = new System.Drawing.Point(160, 195);
            lblAmount.Location = new System.Drawing.Point(12, 248);
            numAmount.Location = new System.Drawing.Point(160, 245);
            lblRate.Location = new System.Drawing.Point(160, 280);
            lblResult.Location = new System.Drawing.Point(160, 305);
            btnExecuteTrade.Location = new System.Drawing.Point(160, 350);

            PrepareDataSources();
            radioBuy.Checked = true;
            SetupUIForTradeType();
        }

        // DÜZELTİLEN METOT
        private void PrepareDataSources()
        {
            try
            {
                // DÜZELTME: StringComparer.OrdinalIgnoreCase kullanılarak büyük-küçük harf duyarlılığı kaldırıldı.
                var cashAssetsQuery = _userAssets.AsEnumerable()
                    .Where(r => r.Field<string>("Varlık") != null &&
                                new[] { "USD", "TRY", "EUR" }.Contains(r.Field<string>("Varlık"), StringComparer.OrdinalIgnoreCase));

                if (cashAssetsQuery.Any())
                    _cashAssetsTable = cashAssetsQuery.CopyToDataTable();
                else
                    _cashAssetsTable = _userAssets.Clone();
            }
            catch (Exception) { _cashAssetsTable = _userAssets.Clone(); }

            try
            {
                // DÜZELTME: StringComparer.OrdinalIgnoreCase kullanılarak büyük-küçük harf duyarlılığı kaldırıldı.
                var cryptoAssetsQuery = _userAssets.AsEnumerable()
                    .Where(r => r.Field<string>("Varlık") != null &&
                                !new[] { "USD", "TRY", "EUR" }.Contains(r.Field<string>("Varlık"), StringComparer.OrdinalIgnoreCase));

                if (cryptoAssetsQuery.Any())
                    _cryptoAssetsTable = cryptoAssetsQuery.CopyToDataTable();
                else
                    _cryptoAssetsTable = _userAssets.Clone();
            }
            catch (Exception) { _cryptoAssetsTable = _userAssets.Clone(); }
        }

        private void SetupUIForTradeType()
        {
            // 1. Önce tüm kontrolleri varsayılan durumuna getirelim (görünür ve aktif)
            lblStatusMessage.Visible = false;
            kryptonLabel1.Visible = true;
            cmbBaseCurrency.Visible = true;
            cmbBaseCurrency.Enabled = true;
            kryptonLabel4.Visible = true;
            cmbQuoteAsset.Visible = true;
            lblAmount.Visible = true;
            numAmount.Visible = true;
            numAmount.Enabled = true;
            btnExecuteTrade.Enabled = true;
            btnExecuteTrade.Visible = true; // Butonun da görünür olduğundan emin oluyoruz

            // Olay aboneliklerini temizle
            cmbBaseCurrency.SelectedIndexChanged -= CmbBaseCurrency_SelectedIndexChanged;
            cmbQuoteAsset.SelectedIndexChanged -= Cmb_SelectedIndexChanged;

            if (radioBuy.Checked) // ALIM MODU
            {
                kryptonLabel1.Text = "Harcanacak Para:";
                kryptonLabel4.Text = "Alınacak Varlık:";
                btnExecuteTrade.Text = "Varlık Al";
                lblAmount.Text = "Harcanacak Miktar:";

                if (_cashAssetsTable == null || _cashAssetsTable.Rows.Count == 0)
                {
                    // Harcanacak para yoksa ilgili kontrolleri gizle
                    kryptonLabel1.Visible = false;
                    cmbBaseCurrency.Visible = false;
                    lblBalance.Visible = false;
                    kryptonLabel4.Visible = false;
                    cmbQuoteAsset.Visible = false;
                    lblAmount.Visible = false;
                    numAmount.Visible = false;
                    lblRate.Visible = false;
                    lblResult.Visible = false;
                    btnExecuteTrade.Visible = false; // Enabled yerine Visible = false yapıldı

                    // Uyarı mesajını göster ve ortala
                    lblStatusMessage.Text = "Harcanacak bakiye bulunmuyor.";
                    lblStatusMessage.Visible = true;
                    lblStatusMessage.Left = (this.kryptonPanel1.ClientSize.Width - lblStatusMessage.Width) / 2;
                    lblStatusMessage.Top = (this.kryptonPanel1.ClientSize.Height - lblStatusMessage.Height) / 2;
                }
                else
                {
                    lblBalance.Visible = true;
                    lblRate.Visible = true;
                    lblResult.Visible = true;

                    cmbBaseCurrency.DataSource = _cashAssetsTable;
                    cmbBaseCurrency.DisplayMember = "Varlık";
                    cmbBaseCurrency.ValueMember = "asset_type_id";

                    var cryptoMarket = _marketPricesData
                        .Where(kvp => !new[] { "usd", "eur", "try" }.Contains(kvp.Key.ToLower()))
                        .ToDictionary(kvp => char.ToUpper(kvp.Key[0]) + kvp.Key.Substring(1), kvp => kvp.Value);

                    cmbQuoteAsset.DataSource = new BindingSource(cryptoMarket, null);
                    cmbQuoteAsset.DisplayMember = "Key";
                    cmbQuoteAsset.ValueMember = "Key";
                    cmbBaseCurrency.SelectedIndexChanged += CmbBaseCurrency_SelectedIndexChanged;
                }
            }
            else // SATIŞ MODU
            {
                lblAmount.Visible = true;
                numAmount.Visible = true;

                kryptonLabel1.Text = "Satılacak Varlık:";
                kryptonLabel4.Text = "Alınacak Para:";
                btnExecuteTrade.Text = "Varlık Sat";
                lblAmount.Text = "Satılacak Miktar:";

                if (_cryptoAssetsTable == null || _cryptoAssetsTable.Rows.Count == 0)
                {
                    // Satılacak varlık yoksa ilgili kontrolleri gizle
                    kryptonLabel1.Visible = false;
                    cmbBaseCurrency.Visible = false;
                    lblBalance.Visible = false;
                    kryptonLabel4.Visible = false;
                    cmbQuoteAsset.Visible = false;
                    lblAmount.Visible = false;
                    numAmount.Visible = false;
                    lblRate.Visible = false;
                    lblResult.Visible = false;
                    btnExecuteTrade.Visible = false; // DÜZELTME: Butonu devre dışı bırakmak yerine GİZLİYORUZ

                    // Uyarı mesajını göster ve ortala
                    lblStatusMessage.Text = "Satılacak herhangi bir varlığınız bulunmuyor.";
                    lblStatusMessage.Visible = true;
                    lblStatusMessage.Left = (this.kryptonPanel1.ClientSize.Width - lblStatusMessage.Width) / 2;
                    lblStatusMessage.Top = (this.kryptonPanel1.ClientSize.Height - lblStatusMessage.Height) / 2;
                }
                else
                {
                    lblBalance.Visible = true;
                    lblRate.Visible = true;
                    lblResult.Visible = true;

                    cmbBaseCurrency.DataSource = _cryptoAssetsTable;
                    cmbBaseCurrency.DisplayMember = "Varlık";
                    cmbBaseCurrency.ValueMember = "asset_type_id";
                    cmbBaseCurrency.SelectedIndexChanged += CmbBaseCurrency_SelectedIndexChanged;

                    UpdateQuoteAssetsForSellMode();
                }
            }

            cmbQuoteAsset.SelectedIndexChanged += Cmb_SelectedIndexChanged;
            UpdateUI();
        }

        private void UpdateQuoteAssetsForSellMode()
        {
            if (cmbBaseCurrency.SelectedItem == null || !(cmbBaseCurrency.SelectedItem is DataRowView))
            {
                cmbQuoteAsset.DataSource = null;
                return;
            }

            var selectedAssetRow = (DataRowView)cmbBaseCurrency.SelectedItem;
            string selectedAssetName = selectedAssetRow["Varlık"].ToString()!.ToLower();

            var quoteAssets = _marketPricesData.Keys
                .Where(key => key != selectedAssetName)
                .Select(key => key.ToUpper())
                .ToList();

            quoteAssets.InsertRange(0, new[] { "TRY", "USD", "EUR" });

            cmbQuoteAsset.DataSource = quoteAssets.Distinct().ToList();
        }

        private void UpdateUI()
        {
            // Temel null kontrolleri
            if (cmbBaseCurrency.SelectedItem == null || !(cmbBaseCurrency.SelectedItem is DataRowView baseAssetRowView))
            {
                lblRate.Text = "Anlık Fiyat: -";
                lblResult.Text = "Tahmini Sonuç: -";
                lblBalance.Text = "Bakiye: -";
                btnExecuteTrade.Enabled = false;
                return;
            }

            // Harcama/Satma miktarı sıfırsa işlem butonunu devre dışı bırak
            btnExecuteTrade.Enabled = (numAmount.Value > 0);

            string baseAssetName = baseAssetRowView["Varlık"].ToString()!;
            decimal availableBalance = Convert.ToDecimal(baseAssetRowView["Miktar"]);
            string baseFormat = radioBuy.Checked ? "N2" : "N6";
            lblBalance.Text = $"Bakiye: {availableBalance.ToString(baseFormat)} {baseAssetName}";

            decimal tradeAmount = numAmount.Value;

            if (radioBuy.Checked)
            {
                if (cmbQuoteAsset.SelectedItem is KeyValuePair<string, PriceInfo> selectedPair)
                {
                    string fromCurrency = baseAssetName;
                    string toCryptoAsset = selectedPair.Key.ToLower();

                    if (!_marketPricesData.ContainsKey(toCryptoAsset)) return;

                    decimal toCryptoPriceInUsd = _marketPricesData[toCryptoAsset].Usd;
                    decimal fromCurrencyPriceInUsd = ConvertToUsd(1, fromCurrency);
                    if (fromCurrencyPriceInUsd == 0 || toCryptoPriceInUsd == 0) return;

                    decimal rate = toCryptoPriceInUsd / fromCurrencyPriceInUsd;
                    lblRate.Text = $"1 {toCryptoAsset.ToUpper()} ≈ {rate:N2} {fromCurrency}";
                    decimal amountToReceive = (tradeAmount * fromCurrencyPriceInUsd) / toCryptoPriceInUsd;
                    lblResult.Text = $"Tahmini Alınacak: ~{amountToReceive:N6} {toCryptoAsset.ToUpper()}";
                }
                else
                {
                    lblRate.Text = "Anlık Fiyat: -";
                    lblResult.Text = "Tahmini Sonuç: -";
                }
            }
            else // SATIŞ MODU
            {
                if (cmbQuoteAsset.SelectedItem == null) return;

                string fromCryptoAsset = baseAssetName.ToLower();
                if (!_marketPricesData.ContainsKey(fromCryptoAsset))
                {
                    lblRate.Text = "Fiyat Bilgisi Yok";
                    lblResult.Text = "-";
                    return;
                }

                string toAsset = cmbQuoteAsset.SelectedItem.ToString()!.ToUpper(); // Alınacak para birimi (USD, TRY, EUR)

                decimal fromCryptoPriceUsd = _marketPricesData[fromCryptoAsset].Usd;
                decimal totalValueInUsd = tradeAmount * fromCryptoPriceUsd;

                // ===== BAŞLANGIÇ: DÜZELTME KODU =====
                if (new[] { "TRY", "USD", "EUR" }.Contains(toAsset))
                {
                    // 1. Kripto paranın 1 biriminin, seçilen para birimindeki değerini hesapla
                    decimal rateInTargetCurrency = ConvertFromUsd(fromCryptoPriceUsd, toAsset);

                    // 2. Doğru formatlama için Kültür (CultureInfo) bilgisini al
                    CultureInfo culture = FormattingHelper.GetCultureInfoFor(toAsset);

                    // 3. Etiketi, doğru para birimi formatıyla güncelle
                    lblRate.Text = $"1 {fromCryptoAsset.ToUpper()} ≈ {rateInTargetCurrency.ToString("N2", culture)} {toAsset}";

                    // Sonuç miktarını hesapla
                    decimal amountToReceive = ConvertFromUsd(totalValueInUsd, toAsset);
                    lblResult.Text = $"Tahmini Ele Geçecek: ~{amountToReceive:N2} {toAsset}";
                }
                else // Kripto'dan Kripto'ya satış (Bu kısım zaten doğru çalışıyordu)
                {
                    if (!_marketPricesData.ContainsKey(toAsset.ToLower()))
                    {
                        lblRate.Text = "Parite Fiyatı Yok";
                        lblResult.Text = "-";
                        return;
                    }

                    decimal toCryptoPriceUsd = _marketPricesData[toAsset.ToLower()].Usd;
                    if (toCryptoPriceUsd == 0) return;

                    decimal rate = fromCryptoPriceUsd / toCryptoPriceUsd;
                    lblRate.Text = $"1 {fromCryptoAsset.ToUpper()} ≈ {rate:N6} {toAsset.ToUpper()}";
                    decimal amountToReceive = totalValueInUsd / toCryptoPriceUsd;
                    lblResult.Text = $"Tahmini Alınacak: ~{amountToReceive:N6} {toAsset.ToUpper()}";
                }
                // ===== BİTİŞ: DÜZELTME KODU =====
            }
        }
        private void CmbBaseCurrency_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Eğer "Sat" modu aktifse, karşıdaki para birimi listesini güncellememiz gerekir.
            if (radioSell.Checked)
            {
                UpdateQuoteAssetsForSellMode();
            }

            // Her durumda ana arayüzü güncelle.
            UpdateUI();
        }
        private async void btnExecuteTrade_Click(object? sender, EventArgs e)
        {
            if (cmbBaseCurrency.SelectedItem == null || !(cmbBaseCurrency.SelectedItem is DataRowView) || cmbQuoteAsset.SelectedItem == null || numAmount.Value <= 0)
            {
                KryptonMessageBox.Show("Lütfen tüm alanları doğru bir şekilde doldurun.", "Eksik Bilgi");
                return;
            }

            var baseAssetRowView = (DataRowView)cmbBaseCurrency.SelectedItem;
            int fromWalletId = Convert.ToInt32(baseAssetRowView["wallet_id"]);
            int fromAssetTypeId = Convert.ToInt32(baseAssetRowView["asset_type_id"]);
            string fromAssetName = baseAssetRowView["Varlık"].ToString()!;
            decimal availableBalance = Convert.ToDecimal(baseAssetRowView["Miktar"]);
            decimal amountToTrade = numAmount.Value;

            if (amountToTrade > availableBalance)
            {
                KryptonMessageBox.Show("Yetersiz bakiye!", "İşlem Hatası");
                return;
            }

            string toAssetName;
            decimal price = 0;
            decimal amountToReceive = 0;
            string tradeType = radioBuy.Checked ? "BUY" : "SELL";
            decimal amountToSpend = amountToTrade;

            try
            {
                if (tradeType == "BUY")
                {
                    toAssetName = ((KeyValuePair<string, PriceInfo>)cmbQuoteAsset.SelectedItem).Key;
                    price = _marketPricesData[toAssetName.ToLower()].Usd;
                    decimal amountToSpendInUsd = ConvertToUsd(amountToSpend, fromAssetName);
                    if (price > 0)
                        amountToReceive = amountToSpendInUsd / price;
                }
                else // SELL
                {
                    toAssetName = cmbQuoteAsset.SelectedItem.ToString()!;
                    price = _marketPricesData[fromAssetName.ToLower()].Usd;
                    decimal totalValueInUsd = amountToSpend * price;
                    string toAssetLower = toAssetName.ToLower();

                    if (new[] { "try", "usd", "eur" }.Contains(toAssetLower))
                    {
                        amountToReceive = ConvertFromUsd(totalValueInUsd, toAssetLower.ToUpper());
                    }
                    else
                    {
                        if (!_marketPricesData.ContainsKey(toAssetLower))
                        {
                            KryptonMessageBox.Show($"Alınacak varlık olan '{toAssetName}' için anlık fiyat bilgisi bulunamadı.", "Fiyat Hatası");
                            return;
                        }
                        decimal toCryptoPriceUsd = _marketPricesData[toAssetLower].Usd;
                        if (toCryptoPriceUsd > 0)
                            amountToReceive = totalValueInUsd / toCryptoPriceUsd;
                    }
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show($"İşlem için gereken veriler hesaplanırken bir hata oluştu:\n{ex.Message}", "Hesaplama Hatası");
                return;
            }

            int? toAssetTypeId = DatabaseHelper.GetAssetTypeIdByName(toAssetName.ToLower());

            if (toAssetTypeId == null)
            {
                KryptonMessageBox.Show($"Alınacak varlık olan '{toAssetName}' veritabanında tanımlı değil.", "Varlık Tanımsız");
                return;
            }
            if (price <= 0)
            {
                KryptonMessageBox.Show($"İşleme konu olan varlığın fiyatı sıfır veya geçersiz. (Fiyat: {price:C2})", "Geçersiz Fiyat");
                return;
            }
            if (amountToReceive <= 0)
            {
                KryptonMessageBox.Show($"Hesaplama sonucu alınacak miktar sıfır veya daha az. Lütfen girdiğiniz tutarı kontrol edin.\nHesaplanan Miktar: {amountToReceive}", "Hesaplama Hatası");
                return;
            }

            string fromFormat = tradeType == "SELL" ? "N6" : "N2";
            string toFormat = new[] { "TRY", "USD", "EUR" }.Contains(toAssetName.ToUpper()) ? "N2" : "N6";

            string confirmationMessage =
                $"{amountToSpend.ToString(fromFormat)} {fromAssetName} vererek\n" +
                $"~{amountToReceive.ToString(toFormat)} {toAssetName.ToUpper()} alacaksınız.\n\n" +
                "İşlemi onaylıyor musunuz?";

            if (KryptonMessageBox.Show(confirmationMessage, "İşlem Onayı", KryptonMessageBoxButtons.YesNo, KryptonMessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                bool success = DatabaseHelper.ExecuteTrade(_currentUser.UserId, fromWalletId, fromAssetTypeId, amountToSpend, fromWalletId, (int)toAssetTypeId, amountToReceive, price, tradeType);
                if (success)
                {
                    KryptonMessageBox.Show("İşlem başarıyla gerçekleştirildi.", "Başarılı", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                    await _owner.LoadAllData();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Trade işlemi sırasında bir hata oluştu: " + ex.Message, "Veritabanı Hatası", KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
        }

        #region Helper and UI Event Methods
        private decimal ConvertToUsd(decimal amount, string currency)
        {
            if (_exchangeRates == null) return 0;
            switch (currency.ToUpper())
            {
                case "USD": return amount;
                case "EUR": return _exchangeRates.Eur == 0 ? 0 : amount / _exchangeRates.Eur;
                case "TRY": return _exchangeRates.Try == 0 ? 0 : amount / _exchangeRates.Try;
                default: return 0;
            }
        }

        private decimal ConvertFromUsd(decimal amountInUsd, string targetCurrency)
        {
            if (_exchangeRates == null) return 0;
            switch (targetCurrency.ToUpper())
            {
                case "USD": return amountInUsd;
                case "EUR": return amountInUsd * _exchangeRates.Eur;
                case "TRY": return amountInUsd * _exchangeRates.Try;
                default: return 0;
            }
        }

        private void Cmb_SelectedIndexChanged(object? sender, EventArgs e) => UpdateUI();
        private void numAmount_ValueChanged(object? sender, EventArgs e) => UpdateUI();
        private void radioType_CheckedChanged(object? sender, EventArgs e)
        {
            if (sender is KryptonRadioButton radioButton && radioButton.Checked)
            {
                SetupUIForTradeType();
            }
        }
        #endregion
    }
}