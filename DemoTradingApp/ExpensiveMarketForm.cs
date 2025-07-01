using Krypton.Toolkit;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DemoTradingApp
{
    /// <summary>
    /// Form for purchasing luxury items in the expensive market.
    /// </summary>
    public partial class ExpensiveMarketForm : KryptonForm
    {
        private class MarketProduct
        {
            public string Name { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public string Currency { get; set; } = string.Empty;
            public string ImagePath { get; set; } = string.Empty;
        }
        private readonly DashboardForm _owner;
        private readonly User _currentUser;
        private readonly DataTable _userAssets;
        private List<CartItem> _shoppingCart = new List<CartItem>();
        private class CartItem
        {
            public string ItemName { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public string CurrencyName { get; set; } = string.Empty;
            public int CurrencyAssetId { get; set; }
            public string ImagePath { get; set; } = string.Empty;
            public int WalletId { get; set; }
        }
        /// <summary>
        /// Initializes a new instance of the ExpensiveMarketForm class.
        /// </summary>
        /// <param name="owner">The parent dashboard form</param>
        /// <param name="user">The current user</param>
        /// <param name="userAssets">User's asset data</param>
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
            PopulateMarketItems();
        }
        private void PopulateMarketItems()
        {
            while (flpMarketItems.Controls.Count > 0)
            {
                flpMarketItems.Controls[0].Dispose();
            }
            flpMarketItems.Controls.Clear();
            var products = new List<MarketProduct>
    {           // Your Products ( this is just a sample, you can add more products on image path (bin/debug/net8.0-windows/Images) folder )
                new MarketProduct { Name = "Luxury Watch", Price = 3131313131, Currency = "TRY" },
                new MarketProduct { Name = "Mazda MX-5", Price = 250000, Currency = "USD" },
                new MarketProduct { Name = "Private Jet", Price = 5000000, Currency = "USD" },
                new MarketProduct { Name = "Titanic", Price = 2, Currency = "Bitcoin" },
                //new MarketProduct { Name = "Presidential Complex", Price = 128000000000, Currency = "USD" },
    };

            flpMarketItems.Controls.Clear();

            foreach (var product in products)
            {
                var group = new KryptonGroupBox
                {
                    AutoSize = true,
                    AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink,
                    Margin = new Padding(10),
                    MinimumSize = new Size(180, 0),
                    MaximumSize = new Size(180, 0),
                    Values = { Heading = Properties.Resources.Product }
                };

                string imagePath = System.IO.Path.Combine("Images", $"{product.Name}.png");
                var pic = new PictureBox
                {
                    BackColor = System.Drawing.Color.Gainsboro,
                    Size = new System.Drawing.Size(166, 132),
                    SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom,
                    ImageLocation = imagePath,
                    Dock = DockStyle.Top
                };

                string wrappedText = WrapText(product.Name, 15);
                var nameLabel = new KryptonLabel
                {
                    AutoSize = true,
                    MaximumSize = new Size(166, 0),
                    StateCommon = { ShortText = { Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold) } },
                    Values = { Text = wrappedText },
                    Dock = DockStyle.Bottom
                };
                nameLabel.Padding = new Padding(0, 5, 0, 5);

                var priceLabel = new KryptonLabel
                {
                    AutoSize = true,
                    Values = { Text = $"{product.Price:N0} {product.Currency}" },
                    Dock = DockStyle.Bottom
                };

                var button = new KryptonButton
                {
                    Size = new System.Drawing.Size(166, 25),
                    Values = { Text = Properties.Resources.AddToCart },
                    Tag = $"{product.Name};{product.Price};{product.Currency};{imagePath}",
                    Dock = DockStyle.Bottom
                };
                button.Click += AddToCart_Click;

                group.Panel.Controls.Add(pic);
                group.Panel.Controls.Add(nameLabel);
                group.Panel.Controls.Add(priceLabel);
                group.Panel.Controls.Add(button);

                flpMarketItems.Controls.Add(group);
            }
        }

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

            DataRow? assetRow = _userAssets.AsEnumerable().FirstOrDefault(r => r.Field<string>("Asset")?.Trim().Equals(currencyName, StringComparison.OrdinalIgnoreCase) == true);

            if (assetRow != null)
            {
                var selectedAsset = new
                {
                    AssetName = assetRow.Field<string>("Asset") ?? "",
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

                KryptonMessageBox.Show(string.Format(Properties.Resources.ItemAddedToCart, itemName));

                if (pnlCart.Visible)
                {
                    UpdateCartDisplay();
                }
            }
            else
            {
                KryptonMessageBox.Show(string.Format(Properties.Resources.NoCurrencyForPurchase, currencyName), Properties.Resources.ErrorTitle);
            }
        }
        private void UpdateCartDisplay()
        {
            while (flpCartItems.Controls.Count > 0)
            {
                flpCartItems.Controls[0].Dispose();
            }
            flpCartItems.Controls.Clear();
            if (_shoppingCart.Any())
            {
                var totals = _shoppingCart.GroupBy(item => item.CurrencyName)
                                          .Select(group => new { Currency = group.Key, Total = group.Sum(item => item.Price) })
                                          .ToList();
                lblCartTotal.Text = Properties.Resources.TotalPrefix + string.Join(", ", totals.Select(t => $"{t.Total:N0} {t.Currency}"));

                foreach (var item in _shoppingCart)
                {
                    var itemPanel = new KryptonPanel
                    {
                        Size = new Size(flpCartItems.ClientSize.Width - 10, 30),
                        StateCommon = { Color1 = Color.Transparent }
                    };

                    var removeButton = new KryptonButton
                    {
                        Text = Properties.Resources.RemoveButton,
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
                lblCartTotal.Text = Properties.Resources.TotalZero;
                flpCartItems.Controls.Add(new KryptonLabel { Text = Properties.Resources.CartEmpty, AutoSize = true });
            }
        }


        private void RemoveFromCart_Click(object? sender, EventArgs e)
        {
            if ((sender as KryptonButton)?.Tag is CartItem itemToRemove)
            {
                _shoppingCart.Remove(itemToRemove);
                UpdateCartDisplay();
            }
        }
        private void btnShowCart_Click(object? sender, EventArgs e)
        {
            UpdateCartDisplay();

            try
            {
                var wallets = _userAssets.AsEnumerable()
                    .Select(row => new {
                        WalletName = row.Field<string>("Wallet"),
                        WalletId = row.Field<int>("wallet_id")
                    })
                    .Distinct()
                    .ToList();

                cmbPaymentMethod.DataSource = wallets;
                cmbPaymentMethod.DisplayMember = "WalletName";
                cmbPaymentMethod.ValueMember = "WalletId";
                cmbPaymentMethod.SelectedIndex = -1;
                cmbPaymentMethod.Text = Properties.Resources.PleaseSelectWallet;
            }
            catch (Exception ex)
            {
                KryptonMessageBox.Show(Properties.Resources.PaymentMethodsLoadError + ex.Message);
            }

            pnlCart.Visible = true;
            btnShowCart.Text = Properties.Resources.ShowCart;
            btnShowCart.Click -= btnShowCart_Click;
            btnShowCart.Click += btnHideCart_Click;
        }
        private void btnHideCart_Click(object? sender, EventArgs e)
        {
            pnlCart.Visible = false;
            btnShowCart.Text = Properties.Resources.ShowCart;

            btnShowCart.Click -= btnHideCart_Click;
            btnShowCart.Click += btnShowCart_Click;
        }

        private async void btnConfirmPurchase_Click(object sender, EventArgs e)
        {
            if (!_shoppingCart.Any())
            {
                KryptonMessageBox.Show(Properties.Resources.NoItemsInCart, Properties.Resources.EmptyCart, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);
                return;
            }

            if (cmbPaymentMethod.SelectedValue == null)
            {
                KryptonMessageBox.Show(Properties.Resources.PleaseSelectPaymentMethod, Properties.Resources.MissingInformation, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Warning);
                return;
            }

            int selectedWalletId = (int)cmbPaymentMethod.SelectedValue;

            bool allFundsAvailable = true;
            var requiredFunds = _shoppingCart
                .GroupBy(item => new { item.CurrencyAssetId, item.CurrencyName })
                .Select(g => new {
                    AssetId = g.Key.CurrencyAssetId,
                    Currency = g.Key.CurrencyName,
                    TotalRequired = g.Sum(i => i.Price)
                });

            StringBuilder missingFundsMessage = new StringBuilder(Properties.Resources.InsufficientBalanceInWallet + "\n");

            foreach (var fund in requiredFunds)
            {
                DataRow? assetRow = _userAssets.AsEnumerable().FirstOrDefault(r =>
                    r.Field<int>("wallet_id") == selectedWalletId &&
                    r.Field<int>("asset_type_id") == fund.AssetId);

                if (assetRow == null)
                {
                    allFundsAvailable = false;
                    missingFundsMessage.AppendLine(string.Format(Properties.Resources.NoCurrencyInWallet, fund.Currency));
                }
                else if (assetRow.Field<decimal>("Amount") < fund.TotalRequired)
                {
                    allFundsAvailable = false;
                    missingFundsMessage.AppendLine($" - {fund.TotalRequired:N2} {fund.Currency} " + string.Format(Properties.Resources.RequiredButAvailable, assetRow.Field<decimal>("Amount").ToString("N2")));
                }
            }

            if (!allFundsAvailable)
            {
                KryptonMessageBox.Show(missingFundsMessage.ToString(), Properties.Resources.InsufficientBalanceTitle, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                return;
            }

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
                            if (!success) throw new Exception($"{item.CurrencyName} " + Properties.Resources.BalanceDeductionError);

                            DatabaseHelper.RecordPurchase(connection, transaction, _currentUser.UserId, item.ItemName, 1, item.Price, item.CurrencyAssetId, item.ImagePath);
                        }

                        transaction.Commit();
                        KryptonMessageBox.Show(Properties.Resources.PurchaseCompletedSuccessfully, Properties.Resources.SuccessTitle, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Information);

                        _shoppingCart.Clear();
                        pnlCart.Visible = false;

                        await _owner.LoadAllData();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        KryptonMessageBox.Show(Properties.Resources.PurchaseError + ex.Message, Properties.Resources.TransactionFailed, KryptonMessageBoxButtons.OK, KryptonMessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}