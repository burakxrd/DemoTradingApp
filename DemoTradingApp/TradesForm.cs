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

        /// <summary>
        /// Initializes a new instance of the TradesForm class.
        /// </summary>
        /// <param name="owner">The dashboard form that owns this form</param>
        /// <param name="user">The current user</param>
        /// <param name="userAssets">User's asset data</param>
        /// <param name="marketPricesData">Current market prices</param>
        /// <param name="exchangeRates">Exchange rates for currencies</param>
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
            PrepareDataSources();
            radioBuy.Checked = true;
            SetupUIForTradeType();
        }
        private void PrepareDataSources()
        {
            try
            {
                var cashAssetsQuery = _userAssets.AsEnumerable()
                    .Where(r => r.Field<string>("Asset") != null &&
                                new[] { "USD", "TRY", "EUR" }.Contains(r.Field<string>("Asset")!, StringComparer.OrdinalIgnoreCase));
                _cashAssetsTable = cashAssetsQuery.Any() ? cashAssetsQuery.CopyToDataTable() : _userAssets.Clone();

                var cryptoAssetsQuery = _userAssets.AsEnumerable()
                    .Where(r => r.Field<string>("Asset") != null &&
                                !new[] { "USD", "TRY", "EUR" }.Contains(r.Field<string>("Asset")!, StringComparer.OrdinalIgnoreCase));
                _cryptoAssetsTable = cryptoAssetsQuery.Any() ? cryptoAssetsQuery.CopyToDataTable() : _userAssets.Clone();
            }
            catch (Exception ex)
            {
                _cashAssetsTable = _userAssets.Clone();
                _cryptoAssetsTable = _userAssets.Clone();
                KryptonMessageBox.Show("Error preparing data sources for trade: " + ex.Message);
            }
        }
        private void SetupUIForTradeType()
        {
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
            btnExecuteTrade.Visible = true;

            cmbBaseCurrency.SelectedIndexChanged -= CmbBaseCurrency_SelectedIndexChanged;
            cmbQuoteAsset.SelectedIndexChanged -= Cmb_SelectedIndexChanged;

            if (radioBuy.Checked)
            {
                kryptonLabel1.Text = Properties.Resources.SpendingMoney;
                kryptonLabel4.Text = Properties.Resources.AssetToBuy;
                btnExecuteTrade.Text = Properties.Resources.BuyAsset;
                lblAmount.Text = Properties.Resources.AmountToSpend;

                if (_cashAssetsTable == null || _cashAssetsTable.Rows.Count == 0)
                {
                    kryptonLabel1.Visible = false;
                    cmbBaseCurrency.Visible = false;
                    lblBalance.Visible = false;
                    kryptonLabel4.Visible = false;
                    cmbQuoteAsset.Visible = false;
                    lblAmount.Visible = false;
                    numAmount.Visible = false;
                    lblRate.Visible = false;
                    lblResult.Visible = false;
                    btnExecuteTrade.Visible = false;

                    lblStatusMessage.Text = Properties.Resources.NoSpendingBalance;
                    lblStatusMessage.Visible = true;
                }
                else
                {
                    lblBalance.Visible = true;
                    lblRate.Visible = true;
                    lblResult.Visible = true;

                    cmbBaseCurrency.DataSource = _cashAssetsTable;
                    cmbBaseCurrency.DisplayMember = "Asset";
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
            else
            {
                lblAmount.Visible = true;
                numAmount.Visible = true;

                kryptonLabel1.Text = Properties.Resources.AssetToSell;
                kryptonLabel4.Text = Properties.Resources.MoneyToReceive;
                btnExecuteTrade.Text = Properties.Resources.SellAsset;
                lblAmount.Text = Properties.Resources.AmountToSell;

                if (_cryptoAssetsTable == null || _cryptoAssetsTable.Rows.Count == 0)
                {
                    kryptonLabel1.Visible = false;
                    cmbBaseCurrency.Visible = false;
                    lblBalance.Visible = false;
                    kryptonLabel4.Visible = false;
                    cmbQuoteAsset.Visible = false;
                    lblAmount.Visible = false;
                    numAmount.Visible = false;
                    lblRate.Visible = false;
                    lblResult.Visible = false;
                    btnExecuteTrade.Visible = false;

                    lblStatusMessage.Text = Properties.Resources.NoAssetsToSell;
                    lblStatusMessage.Visible = true;
                }
                else
                {
                    lblBalance.Visible = true;
                    lblRate.Visible = true;
                    lblResult.Visible = true;

                    cmbBaseCurrency.DataSource = _cryptoAssetsTable;
                    cmbBaseCurrency.DisplayMember = "Asset";
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
            string selectedAssetName = selectedAssetRow["Asset"].ToString()!.ToLower();

            var quoteAssets = _marketPricesData.Keys
                .Where(key => key != selectedAssetName)
                .Select(key => key.ToUpper())
                .ToList();

            quoteAssets.InsertRange(0, new[] { "TRY", "USD", "EUR" });

            cmbQuoteAsset.DataSource = quoteAssets.Distinct().ToList();
        }

        private void UpdateUI()
        {
            if (cmbBaseCurrency.SelectedItem == null || !(cmbBaseCurrency.SelectedItem is DataRowView baseAssetRowView))
            {
                lblRate.Text = Properties.Resources.LivePrice;
                lblResult.Text = Properties.Resources.EstimatedResult;
                lblBalance.Text = Properties.Resources.Balance; 
                btnExecuteTrade.Enabled = false;
                return;
            }

            btnExecuteTrade.Enabled = (numAmount.Value > 0);

            string baseAssetName = baseAssetRowView["Asset"].ToString()!;
            decimal availableBalance = Convert.ToDecimal(baseAssetRowView["Amount"]);
            string balanceFormat = radioBuy.Checked ? "N2" : "N6";
            lblBalance.Text = $"Balance: {availableBalance.ToString(balanceFormat)} {baseAssetName}";

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
                    lblResult.Text = string.Format(Properties.Resources.EstimatedToReceive, amountToReceive.ToString("N6"), toCryptoAsset.ToUpper());
                }
                else
                {
                    lblRate.Text = Properties.Resources.LivePrice;
                    lblResult.Text = Properties.Resources.EstimatedResult;
                }
            }
            else
            {
                if (cmbQuoteAsset.SelectedItem == null) return;

                string fromCryptoAsset = baseAssetName.ToLower();
                if (!_marketPricesData.ContainsKey(fromCryptoAsset))
                {
                    lblRate.Text = Properties.Resources.NoPriceInfo;
                    lblResult.Text = "-";
                    return;
                }

                string toAsset = cmbQuoteAsset.SelectedItem.ToString()!.ToUpper();

                decimal fromCryptoPriceUsd = _marketPricesData[fromCryptoAsset].Usd;
                decimal totalValueInUsd = tradeAmount * fromCryptoPriceUsd;

                if (new[] { "TRY", "USD", "EUR" }.Contains(toAsset))
                {
                    decimal rateInTargetCurrency = ConvertFromUsd(fromCryptoPriceUsd, toAsset);

                    CultureInfo culture = FormattingHelper.GetCultureInfoFor(toAsset);

                    lblRate.Text = $"1 {fromCryptoAsset.ToUpper()} ≈ {rateInTargetCurrency.ToString("N2", culture)} {toAsset}";

                    decimal amountToReceive = ConvertFromUsd(totalValueInUsd, toAsset);
                    lblResult.Text = string.Format(Properties.Resources.EstimatedToGet, amountToReceive.ToString("N2"), toAsset);
                }
                else
                {
                    if (!_marketPricesData.ContainsKey(toAsset.ToLower()))
                    {
                        lblRate.Text = Properties.Resources.NoPairPrice;
                        lblResult.Text = "-";
                        return;
                    }

                    decimal toCryptoPriceUsd = _marketPricesData[toAsset.ToLower()].Usd;
                    if (toCryptoPriceUsd == 0) return;

                    decimal rate = fromCryptoPriceUsd / toCryptoPriceUsd;
                    lblRate.Text = $"1 {fromCryptoAsset.ToUpper()} ≈ {rate:N6} {toAsset.ToUpper()}";
                    decimal amountToReceive = totalValueInUsd / toCryptoPriceUsd;
                    lblResult.Text = string.Format(Properties.Resources.EstimatedToReceive, amountToReceive.ToString("N6"), toAsset.ToUpper());
                }
            }
        }
        private void CmbBaseCurrency_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (radioSell.Checked)
            {
                UpdateQuoteAssetsForSellMode();
            }

            UpdateUI();
        }
        private async void btnExecuteTrade_Click(object? sender, EventArgs e)
        {
            if (cmbBaseCurrency.SelectedItem == null || !(cmbBaseCurrency.SelectedItem is DataRowView) || cmbQuoteAsset.SelectedItem == null || numAmount.Value <= 0)
            {
                KryptonMessageBox.Show(Properties.Resources.FillAllFieldsCorrectly, Properties.Resources.MissingInfo);
                return;
            }
            var baseAssetRowView = (DataRowView)cmbBaseCurrency.SelectedItem;
            int fromWalletId = Convert.ToInt32(baseAssetRowView["wallet_id"]);
            int fromAssetTypeId = Convert.ToInt32(baseAssetRowView["asset_type_id"]);
            string fromAssetName = baseAssetRowView["Asset"].ToString()!;
            decimal availableBalance = Convert.ToDecimal(baseAssetRowView["Amount"]);
            decimal amountToSpend = numAmount.Value;
            if (amountToSpend > availableBalance)
            {
                KryptonMessageBox.Show(Properties.Resources.InsufficientBalanceExclamation, Properties.Resources.OperationError);
                return;
            }
            string toAssetName;
            decimal price = 0;
            decimal amountToReceive = 0;
            string tradeType = radioBuy.Checked ? "BUY" : "SELL";
            try
            {
                if (tradeType == "BUY")
                {
                    toAssetName = ((KeyValuePair<string, PriceInfo>)cmbQuoteAsset.SelectedItem).Key;
                    price = _marketPricesData![toAssetName.ToLower()].Usd;
                    decimal amountToSpendInUsd = ConvertToUsd(amountToSpend, fromAssetName);
                    if (price > 0)
                        amountToReceive = amountToSpendInUsd / price;
                }
                else 
                {
                    toAssetName = cmbQuoteAsset.SelectedItem.ToString()!;
                    price = _marketPricesData![fromAssetName.ToLower()].Usd;
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
                            KryptonMessageBox.Show(string.Format(Properties.Resources.PriceNotFoundForAsset, toAssetName), Properties.Resources.PriceError);
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
                KryptonMessageBox.Show(string.Format(Properties.Resources.CalculationError, ex.Message), "Calculation Error");
                return;
            }
            int? toAssetTypeId = DatabaseHelper.GetAssetTypeIdByName(toAssetName.ToLower());
            if (toAssetTypeId == null)
            {
                KryptonMessageBox.Show(string.Format(Properties.Resources.AssetNotDefinedInDatabase, toAssetName), Properties.Resources.AssetUndefined);
                return;
            }
            if (price <= 0 || amountToReceive <= 0)
            {
                KryptonMessageBox.Show(Properties.Resources.CalculatedAmountError, "Calculation Error");
                return;
            }
            string fromFormat = tradeType == "SELL" ? "N6" : "N2";
            string toFormat = new[] { "TRY", "USD", "EUR" }.Contains(toAssetName.ToUpper()) ? "N2" : "N6";

            string rawMessage = string.Format(
                Properties.Resources.TradeConfirmationMessage,
                amountToSpend.ToString(fromFormat),
                fromAssetName,
                amountToReceive.ToString(toFormat),
                toAssetName.ToUpper());

            string finalMessage = rawMessage.Replace("\\n", Environment.NewLine);

            if (KryptonMessageBox.Show(finalMessage, Properties.Resources.TradeConfirmationTitle, KryptonMessageBoxButtons.YesNo, KryptonMessageBoxIcon.Question) == DialogResult.No)
                return;
            try
            {
                bool success = DatabaseHelper.ExecuteTrade(_currentUser.UserId, fromWalletId, fromAssetTypeId, amountToSpend, (int)toAssetTypeId, amountToReceive, price, tradeType); if (success)
                {
                    KryptonMessageBox.Show(Properties.Resources.TradeCompletedSuccessfully, Properties.Resources.Success, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                    await _owner.LoadAllData();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(string.Format(Properties.Resources.TradeError, ex.Message), Properties.Resources.DatabaseError, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
            }
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

        private void Cmb_SelectedIndexChanged(object? sender, EventArgs e) => UpdateUI();
        private void numAmount_ValueChanged(object? sender, EventArgs e) => UpdateUI();
        private void radioType_CheckedChanged(object? sender, EventArgs e)
        {
            if (sender is KryptonRadioButton radioButton && radioButton.Checked)
            {
                SetupUIForTradeType();
            }
        }
    }
}