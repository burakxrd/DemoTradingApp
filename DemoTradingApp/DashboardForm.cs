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
            this.kryptonManager1.GlobalPaletteMode = Krypton.Toolkit.PaletteMode.SparkleBlue;
            lblWelcome.Text = string.Format(Properties.Resources.WelcomeMessage, _currentUser.Username);
            cmbDisplayCurrency.Items.Add("USD");
            cmbDisplayCurrency.Items.Add("EUR");
            cmbDisplayCurrency.Items.Add("TRY");
            await LoadAllData();
            if (cmbDisplayCurrency.Items.Count > 0)
                cmbDisplayCurrency.SelectedItem = "TRY";
        }
        public async Task LoadAllData()
        {
            await LoadExchangeRatesAsync();
            await LoadMarketPrices();
            LoadUserAssets();
            LoadRecentTrades();
        }
        private string FormatPnl(decimal pnlUsd)
        {
            string selectedCurrency = GetSelectedCurrency();
            decimal pnlConverted = 0;

            if (_exchangeRates != null)
            {
                switch (selectedCurrency.ToUpper())
                {
                    case "EUR": pnlConverted = pnlUsd * _exchangeRates.Eur; break;
                    case "TRY": pnlConverted = pnlUsd * _exchangeRates.Try; break;
                    default: pnlConverted = pnlUsd; break;
                }
            }

            CultureInfo culture = FormattingHelper.GetCultureInfoFor(selectedCurrency);

            if (pnlConverted > 0)
            {
                return "+" + pnlConverted.ToString("C2", culture);
            }
            return pnlConverted.ToString("C2", culture);
        }
        private void LoadUserAssets()
        {
            if (_marketPricesData == null) return;
            try
            {
                DataTable assetsFromDb = DatabaseHelper.GetUserAssetsWithValues(_currentUser.UserId);

                var displayTable = new DataTable();
                displayTable.Columns.Add(Properties.Resources.Wallet, typeof(string));
                displayTable.Columns.Add(Properties.Resources.Asset, typeof(string));
                displayTable.Columns.Add(Properties.Resources.Amount, typeof(decimal));
                displayTable.Columns.Add(Properties.Resources.UnitPrice, typeof(decimal));
                displayTable.Columns.Add(Properties.Resources.TotalValue, typeof(decimal));
                displayTable.Columns.Add(Properties.Resources.PnL, typeof(string));

                _totalValueInUsd = 0;

                foreach (DataRow row in assetsFromDb.Rows)
                {
                    string assetName = row["Asset"].ToString()!;
                    string assetNameLower = assetName.ToLower();
                    decimal amount = Convert.ToDecimal(row["Amount"]);

                    decimal livePriceUsd = 0;
                    if (_marketPricesData.ContainsKey(assetNameLower))
                    {
                        livePriceUsd = _marketPricesData[assetNameLower].Usd;
                    }
                    else if (new[] { "USD", "TRY", "EUR" }.Contains(assetName.ToUpper()))
                    {
                        livePriceUsd = ConvertToUsd(1, assetName);
                    }

                    decimal totalValueUsd = amount * livePriceUsd;
                    _totalValueInUsd += totalValueUsd;

                    string selectedCurrency = GetSelectedCurrency();
                    decimal displayPrice = ConvertFromUsd(livePriceUsd, selectedCurrency);
                    decimal displayTotalValue = ConvertFromUsd(totalValueUsd, selectedCurrency);

                    string pnlString = "";
                    var cryptoList = new[] { "BITCOIN", "ETHEREUM", "SOLANA", "TETHER" };
                    if (cryptoList.Contains(assetName.ToUpper()))
                    {
                        decimal netCostUsd = Convert.ToDecimal(row["NetUSDCost"]);
                        if (netCostUsd > 0)
                        {
                            decimal pnlUsd = totalValueUsd - netCostUsd;
                            pnlString = FormatPnl(pnlUsd);
                        }
                    }

                    displayTable.Rows.Add(row["Wallet"], assetName, amount, displayPrice, displayTotalValue, pnlString);
                }

                dgvAssets.DataSource = displayTable;

                if (dgvAssets.Columns.Contains(Properties.Resources.UnitPrice)) FormatPriceColumn(dgvAssets.Columns[Properties.Resources.UnitPrice]);
                if (dgvAssets.Columns.Contains(Properties.Resources.TotalValue)) FormatPriceColumn(dgvAssets.Columns[Properties.Resources.TotalValue]);

                StyleDataGridView(dgvAssets);
                UpdateTotalBalanceLabel();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(string.Format(Properties.Resources.AssetsLoadError, ex.Message));
            }
        }
        private void StyleDataGridView(KryptonDataGridView dgv)
        {
            if (dgv.ColumnCount == 0 || dgv.Width < 10) return;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgv == dgvAssets && dgv.Columns.Contains(Properties.Resources.PnL))
            {
                dgv.Columns[Properties.Resources.Wallet].FillWeight = 90;
                dgv.Columns[Properties.Resources.Asset].FillWeight = 80;
                dgv.Columns[Properties.Resources.Amount].FillWeight = 100;
                dgv.Columns[Properties.Resources.UnitPrice].FillWeight = 100;
                dgv.Columns[Properties.Resources.TotalValue].FillWeight = 110;
                dgv.Columns[Properties.Resources.PnL].FillWeight = 90;
            }

            if (dgv.Columns.Contains(Properties.Resources.Amount)) dgv.Columns[Properties.Resources.Amount].DefaultCellStyle.Format = "N6";
            if (dgv.Columns.Contains(Properties.Resources.Date)) dgv.Columns[Properties.Resources.Date].DefaultCellStyle.Format = "g";

            if (dgv == dgvAssets && dgv.Columns.Contains(Properties.Resources.PnL))
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells[Properties.Resources.PnL].Value?.ToString()?.StartsWith("+") ?? false)
                        row.Cells[Properties.Resources.PnL].Style.ForeColor = Color.LawnGreen;
                    else if (row.Cells[Properties.Resources.PnL].Value?.ToString()?.StartsWith("-") ?? false)
                        row.Cells[Properties.Resources.PnL].Style.ForeColor = Color.Tomato;
                    else
                        row.Cells[Properties.Resources.PnL].Style.ForeColor = dgv.RowsDefaultCellStyle.ForeColor;
                }
            }
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
                Console.WriteLine(Properties.Resources.ExchangeRatesApiError);
                if (_exchangeRates == null)
                {
                    KryptonMessageBox.Show(Properties.Resources.ExchangeRatesNotAvailable);
                    _exchangeRates = new PriceInfo { Usd = 1, Eur = 0.9m, Try = 32.0m };
                }
            }
        }

        public string GetSelectedCurrency()
        {
            return cmbDisplayCurrency.SelectedItem?.ToString() ?? "USD";
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
                    Console.WriteLine(Properties.Resources.MarketDataApiError);
                    if (_marketPricesData == null)
                    {
                        _marketPricesData = new Dictionary<string, PriceInfo>();
                    }
                }
                UpdateMarketGridDisplay();
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(string.Format(Properties.Resources.MarketPricesLoadError, ex.Message), Properties.Resources.Error);
            }
        }

        private void UpdateMarketGridDisplay()
        {
            if (_marketPricesData == null || _exchangeRates == null) return;
            try
            {
                string selectedCurrency = GetSelectedCurrency();
                var displayTable = new DataTable();
                displayTable.Columns.Add(Properties.Resources.Market, typeof(string));
                displayTable.Columns.Add(string.Format(Properties.Resources.PriceFormat, selectedCurrency), typeof(decimal));

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
                KryptonMessageBox.Show(string.Format(Properties.Resources.MarketTableUpdateError, ex.Message));
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
                KryptonMessageBox.Show(string.Format(Properties.Resources.RecentTradesLoadError, ex.Message), Properties.Resources.Error);
            }
        }

        private void FormatPriceColumn(DataGridViewColumn priceColumn)
        {
            string selectedCurrency = GetSelectedCurrency();
            priceColumn.DefaultCellStyle.Format = "C2";
            priceColumn.DefaultCellStyle.FormatProvider = FormattingHelper.GetCultureInfoFor(selectedCurrency);
        }

        private decimal ConvertToUsd(decimal amount, string currency)
        {
            if (_exchangeRates == null) return 0;
            return currency.ToUpper() switch
            {
                "USD" => amount,
                "EUR" => _exchangeRates.Eur == 0 ? 0 : amount / _exchangeRates.Eur,
                "TRY" => _exchangeRates.Try == 0 ? 0 : amount / _exchangeRates.Try,
                _ => 0,
            };
        }

        private decimal ConvertFromUsd(decimal amountInUsd, string targetCurrency)
        {
            if (_exchangeRates == null) return 0;
            return targetCurrency.ToUpper() switch
            {
                "USD" => amountInUsd,
                "EUR" => amountInUsd * _exchangeRates.Eur,
                "TRY" => amountInUsd * _exchangeRates.Try,
                _ => 0,
            };
        }

        public void ShowTradeForm()
        {
            DataTable originalAssets = DatabaseHelper.GetUserAssetsWithValues(_currentUser.UserId);
            if (originalAssets == null || _marketPricesData == null || _exchangeRates == null)
            {
                KryptonMessageBox.Show(Properties.Resources.TradeFormCannotOpen);
                return;
            }
            TradesForm tradeForm = new TradesForm(this, _currentUser, originalAssets, _marketPricesData, _exchangeRates);
            tradeForm.FormClosed += ChildForm_Closed;
            this.Hide();
            tradeForm.Show();
        }

        public void ShowMarketForm()
        {
            DataTable assets = DatabaseHelper.GetUserAssetsWithValues(_currentUser.UserId);
            ExpensiveMarketForm marketForm = new ExpensiveMarketForm(this, _currentUser, assets);
            marketForm.FormClosed += ChildForm_Closed;
            this.Hide();
            marketForm.Show();
        }

        public void Logout() => Application.Restart();

        public void ShowPossessionsForm()
        {
            MyPossessionsForm possessionsForm = new MyPossessionsForm(_currentUser.UserId);
            possessionsForm.FormClosed += ChildForm_Closed;
            this.Hide();
            possessionsForm.Show();
        }

        private async void ChildForm_Closed(object? sender, FormClosedEventArgs e)
        {
            this.Show();
            await this.LoadAllData();
        }

        public void ShowAddWalletForm()
        {
            AddWalletForm addWalletForm = new AddWalletForm(_currentUser, this);
            addWalletForm.FormClosed += ChildForm_Closed;
            this.Hide();
            addWalletForm.Show();
        }

        public void ShowAddBalanceForm()
        {
            AddBalanceForm addBalanceForm = new AddBalanceForm(_currentUser, this);
            addBalanceForm.FormClosed += ChildForm_Closed;
            this.Hide();
            addBalanceForm.Show();
        }

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

        private async void pnlUpdateTimer_Tick(object? sender, EventArgs e) => await LoadAllData();

        private void cmiShowDashboard_Click(object? sender, EventArgs e)
        {
            pnlMainDashboard.Visible = true;
            pnlMainDashboard.BringToFront();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadAllData();
            KryptonMessageBox.Show(
                Properties.Resources.MarketDataUpdated,
                Properties.Resources.Information,
                KryptonMessageBoxButtons.OK,
                KryptonMessageBoxIcon.Information
            );
        }
    }
}