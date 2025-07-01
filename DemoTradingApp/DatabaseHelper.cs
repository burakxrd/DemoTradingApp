using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;
using DemoTradingApp;

namespace DemoTradingApp
{
    public static class DatabaseHelper
    {
        public static readonly string ConnectionString = "Server=localhost;Database=DemoTrading;Trusted_Connection=True;TrustServerCertificate=True;";
        public static DataTable GetUserAssetsWithValues(int userId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var query = @"
        WITH AverageBuyPrice AS (
            SELECT
                asset_type_id,
                user_id,
                CASE 
                    WHEN SUM(CASE WHEN trade_type = 'BUY' THEN amount ELSE 0 END) = 0 THEN 0
                    ELSE SUM(CASE WHEN trade_type = 'BUY' THEN total_cost ELSE 0 END) / SUM(CASE WHEN trade_type = 'BUY' THEN amount ELSE 0 END)
                END AS AvgPrice
            FROM Trades
            WHERE user_id = @userId AND trade_type = 'BUY'
            GROUP BY asset_type_id, user_id
        )
        SELECT 
            w.wallet_name AS 'Wallet',
            at.asset_name AS 'Asset',
            a.amount AS 'Amount',
            a.wallet_id,
            a.asset_type_id,
            COALESCE(abp.AvgPrice * a.amount, 0) AS 'NetUSDCost'
        FROM Assets a
        INNER JOIN Wallets w ON a.wallet_id = w.wallet_id
        INNER JOIN AssetTypes at ON a.asset_type_id = at.asset_type_id
        LEFT JOIN AverageBuyPrice abp ON a.asset_type_id = abp.asset_type_id AND w.user_id = abp.user_id
        WHERE w.user_id = @userId AND a.amount > 0.000001
        ORDER BY w.wallet_name, at.asset_name";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    var adapter = new SqlDataAdapter(command);
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
        public static bool ExecuteTrade(int userId, int walletId, int fromAssetTypeId, decimal amountToSpend, int toAssetTypeId, decimal amountToReceive, decimal price, string tradeType)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        bool deductSuccess = DeductBalance(connection, transaction, walletId, fromAssetTypeId, amountToSpend);
                        if (!deductSuccess)
                        {
                            transaction.Rollback();
                            return false;
                        }

                        AddOrUpdateAssetBalance(connection, transaction, walletId, toAssetTypeId, amountToReceive);
                        RecordTrade(connection, transaction, userId, tradeType == "BUY" ? toAssetTypeId : fromAssetTypeId, tradeType, tradeType == "BUY" ? amountToReceive : amountToSpend, price, tradeType == "BUY" ? amountToSpend : amountToReceive);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
        public static bool DeductBalance(SqlConnection connection, SqlTransaction transaction, int walletId, int assetTypeId, decimal amount)
        {
            var query = "UPDATE Assets SET amount = amount - @amount WHERE wallet_id = @walletId AND asset_type_id = @assetTypeId AND amount >= @amount";
            using (var command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@amount", amount);
                command.Parameters.AddWithValue("@walletId", walletId);
                command.Parameters.AddWithValue("@assetTypeId", assetTypeId);
                return command.ExecuteNonQuery() > 0;
            }
        }
        public static bool DeductBalance(int walletId, int assetTypeId, decimal amount)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        bool success = DeductBalance(connection, transaction, walletId, assetTypeId, amount);
                        if (success)
                        {
                            transaction.Commit();
                            return true;
                        }
                        else
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
        private static void AddOrUpdateAssetBalance(SqlConnection connection, SqlTransaction transaction, int walletId, int assetTypeId, decimal amount)
        {
            var query = "UPDATE Assets SET amount = amount + @amount WHERE wallet_id = @walletId AND asset_type_id = @assetTypeId";
            using (var command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@amount", amount);
                command.Parameters.AddWithValue("@walletId", walletId);
                command.Parameters.AddWithValue("@assetTypeId", assetTypeId);

                if (command.ExecuteNonQuery() == 0)
                {
                    var insertQuery = "INSERT INTO Assets (wallet_id, asset_type_id, amount, created_at, updated_at) VALUES (@walletId, @assetTypeId, @amount, GETDATE(), GETDATE())";
                    using (var insertCommand = new SqlCommand(insertQuery, connection, transaction))
                    {
                        insertCommand.Parameters.AddWithValue("@walletId", walletId);
                        insertCommand.Parameters.AddWithValue("@assetTypeId", assetTypeId);
                        insertCommand.Parameters.AddWithValue("@amount", amount);
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }
        private static void RecordTrade(SqlConnection connection, SqlTransaction transaction, int userId, int assetTypeId, string tradeType, decimal amount, decimal price, decimal totalCost)
        {
            var query = "INSERT INTO Trades (user_id, asset_type_id, trade_type, amount, price, total_cost, trade_date) VALUES (@userId, @assetTypeId, @tradeType, @amount, @price, @totalCost, GETDATE())";
            using (var command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@assetTypeId", assetTypeId);
                command.Parameters.AddWithValue("@tradeType", tradeType);
                command.Parameters.AddWithValue("@amount", amount);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@totalCost", totalCost);
                command.ExecuteNonQuery();
            }
        }
        public static bool DeleteWallet(int walletId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var query = "DELETE FROM Wallets WHERE wallet_id = @walletId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@walletId", walletId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
        public static DataTable GetWalletTypes()
        {
            var dataTable = new DataTable();
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("SELECT wallet_type_id, type_name FROM WalletTypes ORDER BY type_name", connection);
                var adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }
        public static bool CreateWallet(int userId, string walletName, int walletTypeId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var query = "INSERT INTO Wallets (user_id, wallet_name, wallet_type_id, created_at, updated_at) VALUES (@userId, @walletName, @walletTypeId, GETDATE(), GETDATE())";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@walletName", walletName);
                    command.Parameters.AddWithValue("@walletTypeId", walletTypeId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
        public static User? AuthenticateUser(string username, string password)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("SELECT user_id, username, email, password_hash FROM Users WHERE username = @username", connection);
                command.Parameters.AddWithValue("@username", username);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string storedHash = reader["password_hash"]?.ToString() ?? "";
                        if (!string.IsNullOrEmpty(storedHash) && BCrypt.Net.BCrypt.Verify(password, storedHash))
                        {
                            return new User
                            {
                                UserId = (int)reader["user_id"],
                                Username = reader["username"]?.ToString() ?? "",
                                Email = reader["email"]?.ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }
        public static DataTable GetWalletsForUser(int userId)
        {
            var dataTable = new DataTable();
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(@"
            SELECT 
                w.wallet_id, 
                w.wallet_name, 
                wt.type_name 
            FROM Wallets w 
            JOIN WalletTypes wt ON w.wallet_type_id = wt.wallet_type_id 
            WHERE w.user_id = @user_id", connection);

                command.Parameters.AddWithValue("@user_id", userId);
                var adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            return dataTable;
        }
        public static void SavePriceToDatabase(int assetTypeId, decimal priceInUsd)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var checkCmd = new SqlCommand("SELECT price_id FROM Prices WHERE asset_type_id = @asset_type_id", connection);
                checkCmd.Parameters.AddWithValue("@asset_type_id", assetTypeId);

                object? existingPriceId = checkCmd.ExecuteScalar();

                SqlCommand finalCmd;
                if (existingPriceId != null)
                {
                    finalCmd = new SqlCommand("UPDATE Prices SET price = @price, price_date = GETDATE() WHERE price_id = @price_id", connection);
                    finalCmd.Parameters.AddWithValue("@price_id", existingPriceId);
                }
                else
                {
                    finalCmd = new SqlCommand("INSERT INTO Prices (asset_type_id, price, price_date) VALUES (@asset_type_id, @price, GETDATE())", connection);
                }

                finalCmd.Parameters.AddWithValue("@asset_type_id", assetTypeId);
                finalCmd.Parameters.AddWithValue("@price", priceInUsd);
                finalCmd.ExecuteNonQuery();
            }
        }
        public static decimal? GetPriceFromDatabase(int assetTypeId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("SELECT price FROM Prices WHERE asset_type_id = @asset_type_id", connection);
                command.Parameters.AddWithValue("@asset_type_id", assetTypeId);
                connection.Open();
                object? result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToDecimal(result);
                }
            }
            return null;
        }
        public static void AddOrUpdateAssetBalance(int walletId, decimal amount, string currency)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var getAssetTypeIdCmd = new SqlCommand("SELECT asset_type_id FROM AssetTypes WHERE UPPER(asset_name) = @currency", connection);
                getAssetTypeIdCmd.Parameters.AddWithValue("@currency", currency.ToUpper());
                object? assetTypeIdObj = getAssetTypeIdCmd.ExecuteScalar();
                if (assetTypeIdObj == null) throw new Exception($"{currency} adında bir varlık türü bulunamadı.");
                int assetTypeId = (int)assetTypeIdObj;
                using (var transaction = connection.BeginTransaction())
                {
                    AddOrUpdateAssetBalance(connection, transaction, walletId, assetTypeId, amount);
                    transaction.Commit();
                }
            }
        }
        public static bool RegisterUser(string username, string email, string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            using (var connection = new SqlConnection(ConnectionString))
            {
                var checkCommand = new SqlCommand("SELECT COUNT(1) FROM Users WHERE username = @username OR email = @email", connection);
                checkCommand.Parameters.AddWithValue("@username", username);
                checkCommand.Parameters.AddWithValue("@email", email);

                connection.Open();
                int userCount = (int)checkCommand.ExecuteScalar();
                if (userCount > 0) return false;

                var insertCommand = new SqlCommand(
                    "INSERT INTO Users (username, email, password_hash, created_at, updated_at) VALUES (@username, @email, @password_hash, GETDATE(), GETDATE())",
                    connection);
                insertCommand.Parameters.AddWithValue("@username", username);
                insertCommand.Parameters.AddWithValue("@email", email);
                insertCommand.Parameters.AddWithValue("@password_hash", hashedPassword);

                return insertCommand.ExecuteNonQuery() > 0;
            }
        }
        public static bool UpdatePasswordByEmail(string email, string newPassword)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("UPDATE Users SET password_hash = @password_hash, updated_at = GETDATE() WHERE email = @email", connection);
                command.Parameters.AddWithValue("@password_hash", hashedPassword);
                command.Parameters.AddWithValue("@email", email);
                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
        public static bool EmailExists(string email)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("SELECT COUNT(1) FROM Users WHERE email = @email", connection);
                command.Parameters.AddWithValue("@email", email.Trim());
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
        public static bool DeletePossession(int purchaseId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var query = "DELETE FROM PurchasedItems WHERE purchase_id = @purchaseId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@purchaseId", purchaseId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
        public static int? GetAssetTypeIdByName(string assetName)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var query = "SELECT asset_type_id FROM AssetTypes WHERE asset_name = @assetName";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@assetName", assetName);
                    var result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : null;
                }
            }
        }
        public static DataTable GetUserPossessions(int userId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var query = @"
            SELECT 
                p.purchase_id,
                p.item_name AS 'ProductName',
                p.quantity AS 'Quantity',
                p.purchase_price AS 'PurchasePrice',
                at.asset_name AS 'Currency',
                p.image_path,
                p.purchase_date
            FROM PurchasedItems p
            INNER JOIN AssetTypes at ON p.currency_asset_id = at.asset_type_id
            WHERE p.user_id = @userId
            ORDER BY p.purchase_date DESC";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    var adapter = new SqlDataAdapter(command);
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
        public static void RecordPurchase(SqlConnection connection, SqlTransaction transaction, int userId, string itemName, int quantity, decimal price, int currencyAssetTypeId, string imagePath)
        {
            var query = @"
        INSERT INTO PurchasedItems (user_id, item_name, quantity, purchase_price, currency_asset_id, image_path, purchase_date) 
        VALUES (@userId, @itemName, @quantity, @price, @currencyAssetTypeId, @imagePath, GETDATE())";

            using (var command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@itemName", itemName);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@currencyAssetTypeId", currencyAssetTypeId);
                command.Parameters.AddWithValue("@imagePath", imagePath);
                command.ExecuteNonQuery();
            }
        }
        public static DataTable GetRecentTrades(int userId)
        {
            var dataTable = new DataTable();
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand(@"
            SELECT TOP 10 
                t.trade_date AS 'Date',
                t.trade_type AS 'Transaction',
                at.asset_name AS 'Asset',
                t.amount AS 'Amount',
                t.price AS 'Price',
                t.total_cost AS 'TotalCost'
            FROM Trades t 
            JOIN AssetTypes at ON t.asset_type_id = at.asset_type_id
            WHERE t.user_id = @userId 
            ORDER BY t.trade_date DESC", connection);

                command.Parameters.AddWithValue("@userId", userId);
                var adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
    }
}