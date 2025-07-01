using Krypton.Toolkit;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;
using System.Linq;

namespace DemoTradingApp
{
    public partial class DashboardForm : KryptonForm
    {
        private readonly User _currentUser;
        private PriceInfo? _exchangeRates;
        private decimal _totalValueInUsd = 0;
        private Dictionary<string, PriceInfo>? _marketPricesData;

        public DashboardForm(User user)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            _currentUser = user;
        }

        private async void DashboardForm_Load(object? sender, EventArgs e)
        {
            this.kryptonManager1.GlobalPaletteMode = PaletteMode.SparkleBlue;
            lblWelcome.Text = $"Hoş geldiniz, {_currentUser.Username}!";

            // Kaldırılan panellerin Visible özellikleri artık burada yönetilmiyor.
            pnlMainDashboard.Visible = true;

            cmbDisplayCurrency.Items.Add("USD");
            cmbDisplayCurrency.Items.Add("EUR");
            cmbDisplayCurrency.Items.Add("TRY");

            await LoadAllData();

            // Veri yüklendikten sonra seçim yapmak daha güvenli
            if (cmbDisplayCurrency.Items.Count > 0)
                cmbDisplayCurrency.SelectedItem = "TRY";
        }

        private async Task LoadExchangeRatesAsync()
        {
            var newExchangeRates = await ApiService.GetExchangeRatesAsync();
            if (newExchangeRates != null)
            {
                _exchangeRates = newExchangeRates;
            }
            else
            {
                Console.WriteLine("API'den döviz kuru verileri alınamadı, mevcut veriler kullanılıyor.");
                if (_exchangeRates == null)
                {
                    KryptonMessageBox.Show("Döviz kurları alınamadı. Varsayılan kurlar kullanılacak.");
                    _exchangeRates = new PriceInfo { Usd = 1, Eur = 0.9m, Try = 32.0m };
                }
            }
        }

        public async Task LoadAllData()
        {
            await LoadExchangeRatesAsync();
            await LoadMarketPrices();
            LoadUserAssets();
            LoadRecentTrades();
        }

        private void StyleDataGridView(KryptonDataGridView dgv)
        {
            if (dgv.ColumnCount == 0 || dgv.Width < 10) return;

            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgv == dgvAssets && dgv.Columns.Contains("K/Z"))
            {
                dgv.Columns["Cüzdan"].FillWeight = 90;
                dgv.Columns["Varlık"].FillWeight = 80;
                dgv.Columns["Miktar"].FillWeight = 100;
                dgv.Columns["Birim Fiyat"].FillWeight = 100;
                dgv.Columns["Toplam Değer"].FillWeight = 110;
                dgv.Columns["K/Z"].FillWeight = 90;
            }

            if (dgv.Columns.Contains("Miktar")) dgv.Columns["Miktar"].DefaultCellStyle.Format = "N6";
            if (dgv.Columns.Contains("Tarih")) dgv.Columns["Tarih"].DefaultCellStyle.Format = "g";

            if (dgv == dgvAssets && dgv.Columns.Contains("K/Z"))
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells["K/Z"].Value?.ToString()?.StartsWith("+") ?? false)
                    {
                        row.Cells["K/Z"].Style.ForeColor = Color.LawnGreen;
                    }
                    else if (row.Cells["K/Z"].Value?.ToString()?.StartsWith("-") ?? false)
                    {
                        row.Cells["K/Z"].Style.ForeColor = Color.Tomato;
                    }
                }
            }
        }

        public string GetSelectedCurrency()
        {
            return cmbDisplayCurrency.SelectedItem?.ToString() ?? "USD";
        }

        private void LoadUserAssets()
        {
            if (_marketPricesData == null) return;
            try
            {
                DataTable assetsFromDb = DatabaseHelper.GetUserAssetsWithValues(_currentUser.UserId);

                var displayTable = new DataTable();
                displayTable.Columns.Add("Cüzdan", typeof(string));
                displayTable.Columns.Add("Varlık", typeof(string));
                displayTable.Columns.Add("Miktar", typeof(decimal));
                displayTable.Columns.Add("Birim Fiyat", typeof(decimal));
                displayTable.Columns.Add("Toplam Değer", typeof(decimal));
                displayTable.Columns.Add("K/Z", typeof(string));

                _totalValueInUsd = 0;

                foreach (DataRow row in assetsFromDb.Rows)
                {
                    string assetName = row["Varlık"].ToString()!;
                    string assetNameLower = assetName.ToLower();
                    decimal amount = Convert.ToDecimal(row["Miktar"]);

                    decimal livePriceUsd = 0;
                    if (_marketPricesData.ContainsKey(assetNameLower))
                    {
                        livePriceUsd = _marketPricesData[assetNameLower].Usd;
                    }
                    else if (new[] { "USD", "TRY", "EUR" }.Contains(assetName.ToUpper()))
                    {
                        livePriceUsd = ConvertToUsd(1, assetName);
                    }

                    decimal totalValueUsd;
                    if (new[] { "TRY", "EUR" }.Contains(assetName.ToUpper()))
                    {
                        totalValueUsd = ConvertToUsd(amount, assetName);
                    }
                    else
                    {
                        totalValueUsd = amount * livePriceUsd;
                    }
                    _totalValueInUsd += totalValueUsd;

                    string selectedCurrency = GetSelectedCurrency();
                    decimal displayPrice = ConvertFromUsd(livePriceUsd, selectedCurrency);
                    decimal displayTotalValue = ConvertFromUsd(totalValueUsd, selectedCurrency);

                    string pnlString = "";
                    var cryptoList = new[] { "BITCOIN", "ETHEREUM", "SOLANA", "TETHER" };
                    if (cryptoList.Contains(assetName.ToUpper()))
                    {
                        decimal netCostUsd = Convert.ToDecimal(row["NetUSDMaliyet"]);
                        decimal pnlUsd = totalValueUsd - netCostUsd;
                        pnlString = FormatPnl(pnlUsd);
                    }

                    displayTable.Rows.Add(row["Cüzdan"], assetName, amount, displayPrice, displayTotalValue, pnlString);
                }

                dgvAssets.DataSource = displayTable;

                if (dgvAssets.Columns.Contains("Birim Fiyat")) FormatPriceColumn(dgvAssets.Columns["Birim Fiyat"]);
                if (dgvAssets.Columns.Contains("Toplam Değer")) FormatPriceColumn(dgvAssets.Columns["Toplam Değer"]);

                StyleDataGridView(dgvAssets);
                UpdateTotalBalanceLabel();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Varlıklar yüklenirken bir hata oluştu: " + ex.Message);
            }
        }

        private string FormatPnl(decimal pnlUsd)
        {
            string selectedCurrency = GetSelectedCurrency();
            decimal pnlConverted = 0;

            if (_exchangeRates != null)
            {
                switch (selectedCurrency)
                {
                    case "EUR": pnlConverted = pnlUsd * _exchangeRates.Eur; break;
                    case "TRY": pnlConverted = pnlUsd * _exchangeRates.Try; break;
                    default: pnlConverted = pnlUsd; break;
                }
            }

            CultureInfo culture = FormattingHelper.GetCultureInfoFor(selectedCurrency);
            return string.Format(culture, "{0:C2}", pnlConverted);
        }

        private void UpdateTotalBalanceLabel()
        {
            if (_exchangeRates == null) return;

            string selectedCurrency = GetSelectedCurrency();
            decimal convertedValue = ConvertFromUsd(_totalValueInUsd, selectedCurrency);
            CultureInfo culture = FormattingHelper.GetCultureInfoFor(selectedCurrency);

            lblBalance.Text = string.Format(culture, "{0:C2}", convertedValue);
        }

        private void cmbDisplayCurrency_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LoadUserAssets();
            UpdateMarketGridDisplay();
        }

        private async Task LoadMarketPrices()
        {
            try
            {
                var coinList = new List<string> { "bitcoin", "ethereum", "solana", "tether" };
                var newMarketPricesData = await ApiService.GetCryptoPricesAsync(coinList);

                if (newMarketPricesData != null && newMarketPricesData.Count > 0)
                {
                    _marketPricesData = newMarketPricesData;
                }
                else
                {
                    Console.WriteLine("API'den yeni piyasa verisi alınamadı.");
                    if (_marketPricesData == null)
                    {
                        _marketPricesData = new Dictionary<string, PriceInfo>();
                    }
                }
                UpdateMarketGridDisplay();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Piyasa fiyatları yüklenirken bir hata oluştu: " + ex.Message, "Hata");
            }
        }

        private void UpdateMarketGridDisplay()
        {
            if (_marketPricesData == null || _exchangeRates == null) return;
            try
            {
                string selectedCurrency = GetSelectedCurrency();

                var displayTable = new DataTable();
                displayTable.Columns.Add("Piyasa", typeof(string));
                displayTable.Columns.Add($"Fiyat ({selectedCurrency})", typeof(decimal));

                foreach (var coin in _marketPricesData)
                {
                    decimal priceUsd = coin.Value.Usd;
                    decimal convertedPrice = ConvertFromUsd(priceUsd, selectedCurrency);
                    string displayName = char.ToUpper(coin.Key[0]) + coin.Key.Substring(1);
                    displayTable.Rows.Add(displayName, convertedPrice);
                }

                dgvMarket.DataSource = displayTable;

                if (dgvMarket.Columns.Count > 1)
                {
                    FormatPriceColumn(dgvMarket.Columns[1]);
                }
                StyleDataGridView(dgvMarket);
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Piyasa tablosu güncellenirken bir hata oluştu: " + ex.Message);
            }
        }

        private void LoadRecentTrades()
        {
            try
            {
                dgvTrades.DataSource = DatabaseHelper.GetRecentTrades(_currentUser.UserId);
                StyleDataGridView(dgvTrades);
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show("Son işlemler yüklenirken bir hata oluştu: " + ex.Message, "Hata");
            }
        }

        // ShowPanelByName ve panel'lere ait diğer metotlar bu bölümden kaldırıldı.

        #region Form Çağırma ve Yardımcı Metotlar

        private void FormatPriceColumn(DataGridViewColumn priceColumn)
        {
            string selectedCurrency = GetSelectedCurrency();
            priceColumn.DefaultCellStyle.Format = "C2";
            priceColumn.DefaultCellStyle.FormatProvider = FormattingHelper.GetCultureInfoFor(selectedCurrency);
        }

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

        public void ShowTradeForm()
        {
            DataTable originalAssets = DatabaseHelper.GetUserAssetsWithValues(_currentUser.UserId);
            if (originalAssets == null || _marketPricesData == null || _exchangeRates == null)
            {
                KryptonMessageBox.Show("Gerekli veriler yüklenemediği için trade ekranı açılamıyor.");
                return;
            }

            TradesForm tradeForm = new TradesForm(this, _currentUser, originalAssets, _marketPricesData, _exchangeRates);
            tradeForm.FormClosed += ChildForm_Closed; // Ortak olaya bağlıyoruz
            this.Hide();
            tradeForm.Show();
        }

        public void ShowMarketForm()
        {
            DataTable assets = DatabaseHelper.GetUserAssetsWithValues(_currentUser.UserId);
            ExpensiveMarketForm marketForm = new ExpensiveMarketForm(this, _currentUser, assets);

            // Form kapatıldığında ChildForm_Closed metodunu tetikle
            marketForm.FormClosed += ChildForm_Closed;
            this.Hide();
            marketForm.Show();
        }


        public void Logout()
        {
            Application.Restart();
        }
        public void ShowPossessionsForm()
        {
            MyPossessionsForm possessionsForm = new MyPossessionsForm(_currentUser.UserId);

            // Form kapatıldığında ChildForm_Closed metodunu tetikle
            possessionsForm.FormClosed += ChildForm_Closed;
            this.Hide();
            possessionsForm.Show();
        }
        private async void ChildForm_Closed(object? sender, FormClosedEventArgs e)
        {
            // Gizlenmiş olan ana formu (Dashboard) tekrar görünür yap.
            this.Show();
            // Form geri geldiğinde güncel verileri görmek için her şeyi yeniden yükle.
            await this.LoadAllData();
        }
        public void ShowAddWalletForm()
        {
            AddWalletForm addWalletForm = new AddWalletForm(_currentUser, this);
            addWalletForm.FormClosed += ChildForm_Closed; // Ortak olaya bağlıyoruz
            this.Hide();
            addWalletForm.Show();
        }
        public void ShowAddBalanceForm()
        {
            AddBalanceForm addBalanceForm = new AddBalanceForm(_currentUser, this);
            addBalanceForm.FormClosed += ChildForm_Closed; // Ortak olaya bağlıyoruz
            this.Hide();
            addBalanceForm.Show();
        }
        #endregion

        #region Form Olayları (Events)
        private void btnMenu_Click(object? sender, EventArgs e)
        {
            MenuForm menu = new MenuForm(this);
            Point buttonScreenLocation = btnMenu.PointToScreen(Point.Empty);
            menu.Location = new Point(buttonScreenLocation.X - menu.Width + btnMenu.Width, buttonScreenLocation.Y + btnMenu.Height);
            menu.Show();
        }

        private void DashboardForm_Resize(object? sender, EventArgs e)
        {
            StyleDataGridView(dgvAssets);
            StyleDataGridView(dgvMarket);
            StyleDataGridView(dgvTrades);
        }

        private async void pnlUpdateTimer_Tick(object? sender, EventArgs e)
        {
            await LoadAllData();
        }

        // Bu metot, form üzerindeki KryptonContextMenu için bırakıldı, ancak işlevselliği artık MenuForm'da.
        // İsterseniz silebilirsiniz veya ana panele sağ tık menüsü eklemek için kullanabilirsiniz.
        private void cmiShowDashboard_Click(object? sender, EventArgs e)
        {
            pnlMainDashboard.Visible = true;
            pnlMainDashboard.BringToFront();
        }

        #endregion
    }
}