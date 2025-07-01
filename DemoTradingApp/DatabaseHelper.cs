// DatabaseHelper.cs dosyasının içeriğini bu kod ile tamamen değiştirin.

using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;
using DemoTradingApp;

public static class DatabaseHelper
{
    public const string ConnectionString = "Server=DESKTOP-P0H8NU6;Database=DemoTrading;Trusted_Connection=True;TrustServerCertificate=True;";// burada kendi veritabanı bağlantı dizesini kullanmalısınız.

    // DÜZELTİLEN SORGU: Artık fiyatlar için veritabanına bağlı değil. Sadece varlık ve miktar bilgisini çeker.
    public static DataTable GetUserAssetsWithValues(int userId)
    {
        var dataTable = new DataTable();
        using (var connection = new SqlConnection(ConnectionString))
        {
            var command = new SqlCommand(@"
            WITH CostBasis AS (
                SELECT
                    asset_type_id,
                    SUM(CASE WHEN trade_type = 'BUY' THEN amount * price ELSE 0 END) AS TotalUSDCost,
                    SUM(CASE WHEN trade_type = 'SELL' THEN amount * price ELSE 0 END) AS TotalUSDProceeds
                FROM Trades
                WHERE user_id = @user_id
                GROUP BY asset_type_id
            )
            SELECT 
                w.wallet_id,
                a.asset_id,
                at.asset_type_id,
                w.wallet_name AS 'Cüzdan',
                at.asset_name AS 'Varlık', 
                a.amount AS 'Miktar',
                ISNULL(cb.TotalUSDCost, 0) - ISNULL(cb.TotalUSDProceeds, 0) AS 'NetUSDMaliyet'
            FROM Assets a
            JOIN AssetTypes at ON a.asset_type_id = at.asset_type_id
            JOIN Wallets w ON a.wallet_id = w.wallet_id
            LEFT JOIN CostBasis cb ON a.asset_type_id = cb.asset_type_id
            WHERE w.user_id = @user_id AND a.amount > 0.000001", connection);

            command.Parameters.AddWithValue("@user_id", userId);

            var adapter = new SqlDataAdapter(command);
            adapter.Fill(dataTable);
        }
        return dataTable;
    }
    // DatabaseHelper.cs içine eklenecek YENİ metotlar

    public static bool DeductBalance(SqlConnection connection, SqlTransaction transaction, int walletId, int assetTypeId, decimal amountToDeduct)
    {
        var command = new SqlCommand(
            // Sadece yeterli bakiye varsa güncelleme yap
            "UPDATE Assets SET amount = amount - @amount WHERE wallet_id = @wallet_id AND asset_type_id = @asset_type_id AND amount >= @amount",
            connection,
            transaction);

        command.Parameters.AddWithValue("@amount", amountToDeduct);
        command.Parameters.AddWithValue("@wallet_id", walletId);
        command.Parameters.AddWithValue("@asset_type_id", assetTypeId);

        // Eğer 1 satır etkilendiyse (yani bakiye yeterliyse ve düşüldüyse) true döner.
        return command.ExecuteNonQuery() > 0;
    }

    public static void RecordPurchase(SqlConnection connection, SqlTransaction transaction, int userId, string itemName, int quantity, decimal price, int currencyAssetId, string imagePath)
    {
        var command = new SqlCommand(
            "INSERT INTO PurchasedItems (user_id, item_name, quantity, purchase_price, currency_asset_id, image_path, purchase_date) VALUES (@user_id, @item_name, @quantity, @price, @currency_asset_id, @image_path, GETDATE())",
            connection,
            transaction);

        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@item_name", itemName);
        command.Parameters.AddWithValue("@quantity", quantity);
        command.Parameters.AddWithValue("@price", price);
        command.Parameters.AddWithValue("@currency_asset_id", currencyAssetId);
        command.Parameters.AddWithValue("@image_path", imagePath);

        command.ExecuteNonQuery();
    }
    public static bool EmailExists(string email)
    {
        using (var connection = new SqlConnection(ConnectionString))
        {
            var command = new SqlCommand("SELECT COUNT(1) FROM Users WHERE email = @email", connection);
            command.Parameters.AddWithValue("@email", email.Trim());
            connection.Open();
            int count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }
    }
    public static DataTable GetUserPossessions(int userId)
    {
        var dataTable = new DataTable();
        using (var connection = new SqlConnection(ConnectionString))
        {
            var command = new SqlCommand(@"
            SELECT 
                p.purchase_id, -- BU SATIRI YENİ EKLEDİK
                p.item_name AS 'Ürün Adı',
                p.quantity AS 'Adet',
                p.purchase_price AS 'Alış Fiyatı',
                at.asset_name AS 'Para Birimi',
                p.purchase_date AS 'Alış Tarihi',
                p.image_path
            FROM PurchasedItems p
            JOIN AssetTypes at ON p.currency_asset_id = at.asset_type_id
            WHERE p.user_id = @user_id
            ORDER BY p.purchase_date DESC", connection);

            command.Parameters.AddWithValue("@user_id", userId);
            var adapter = new SqlDataAdapter(command);
            adapter.Fill(dataTable);
        }
        return dataTable;
    }
    public static bool DeletePossession(int purchaseId)
    {
        using (var connection = new SqlConnection(ConnectionString))
        {
            var command = new SqlCommand("DELETE FROM PurchasedItems WHERE purchase_id = @purchase_id", connection);
            command.Parameters.AddWithValue("@purchase_id", purchaseId);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    public static int? GetAssetTypeIdByName(string assetName)
    {
        using (var connection = new SqlConnection(ConnectionString))
        {
            var command = new SqlCommand("SELECT asset_type_id FROM AssetTypes WHERE UPPER(asset_name) = @assetName", connection);
            command.Parameters.AddWithValue("@assetName", assetName.ToUpper());
            connection.Open();
            object result = command.ExecuteScalar();
            return result != null ? (int?)Convert.ToInt32(result) : null;
        }
    }
    public static bool DeleteWallet(int walletId)
    {
        using (var connection = new SqlConnection(ConnectionString))
        {
            // ON DELETE CASCADE sayesinde, bu komut hem cüzdanı hem de içindeki tüm varlıkları silecektir.
            var command = new SqlCommand("DELETE FROM Wallets WHERE wallet_id = @wallet_id", connection);
            command.Parameters.AddWithValue("@wallet_id", walletId);

            connection.Open();
            int result = command.ExecuteNonQuery();

            // Eğer 1 veya daha fazla satır etkilendiyse işlem başarılıdır.
            return result > 0;
        }
    }
    public static bool DeductBalance(int walletId, int assetTypeId, decimal amountToDeduct)
    {
        using (var connection = new SqlConnection(ConnectionString))
        {
            // Sadece yeterli bakiye varsa güncelleme yapan güvenli SQL komutu
            var command = new SqlCommand(
                "UPDATE Assets SET amount = amount - @amount WHERE wallet_id = @wallet_id AND asset_type_id = @asset_type_id AND amount >= @amount",
                connection);

            command.Parameters.AddWithValue("@amount", amountToDeduct);
            command.Parameters.AddWithValue("@wallet_id", walletId);
            command.Parameters.AddWithValue("@asset_type_id", assetTypeId);

            connection.Open();

            // Eğer komut 1 satırı etkilediyse (yani bakiye başarıyla düşürüldüyse), true döner.
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    public static DataTable GetRecentTrades(int userId)
    {
        var dataTable = new DataTable();
        using (var connection = new SqlConnection(ConnectionString))
        {
            var command = new SqlCommand(@"
                SELECT TOP 10 t.trade_type AS 'İşlem', at.asset_name AS 'Varlık', t.amount AS 'Miktar', t.trade_date AS 'Tarih'
                FROM Trades t JOIN AssetTypes at ON t.asset_type_id = at.asset_type_id
                WHERE t.user_id = @user_id ORDER BY t.trade_date DESC", connection);
            command.Parameters.AddWithValue("@user_id", userId);
            var adapter = new SqlDataAdapter(command);
            adapter.Fill(dataTable);
        }
        return dataTable;
    }

    // --- Geri kalan tüm metotlar aynı kalabilir, tamlık adına eklenmiştir ---
    #region Unchanged Methods

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
                    string storedHash = reader["password_hash"].ToString()!;
                    if (BCrypt.Net.BCrypt.Verify(password, storedHash))
                    {
                        return new User
                        {
                            UserId = (int)reader["user_id"],
                            Username = reader["username"].ToString(),
                            Email = reader["email"].ToString()
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

            // Bu asset_type_id için zaten bir kayıt var mı diye kontrol et
            var checkCmd = new SqlCommand("SELECT price_id FROM Prices WHERE asset_type_id = @asset_type_id", connection);
            checkCmd.Parameters.AddWithValue("@asset_type_id", assetTypeId);

            object? existingPriceId = checkCmd.ExecuteScalar();

            SqlCommand finalCmd;
            if (existingPriceId != null)
            {
                // Varsa, fiyatı ve tarihi güncelle
                finalCmd = new SqlCommand(
                    "UPDATE Prices SET price = @price, price_date = GETDATE() WHERE price_id = @price_id",
                    connection);
                finalCmd.Parameters.AddWithValue("@price_id", existingPriceId);
            }
            else
            {
                // Yoksa, yeni kayıt ekle
                finalCmd = new SqlCommand(
                    "INSERT INTO Prices (asset_type_id, price, price_date) VALUES (@asset_type_id, @price, GETDATE())",
                    connection);
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
        return null; // Kayıt bulunamadı
    }
    public static void AddOrUpdateAssetBalance(int walletId, decimal amount, string currency)
    {
        using (var connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            var getAssetTypeIdCmd = new SqlCommand("SELECT asset_type_id FROM AssetTypes WHERE UPPER(asset_name) = @currency", connection);
            getAssetTypeIdCmd.Parameters.AddWithValue("@currency", currency.ToUpper());
            object assetTypeIdObj = getAssetTypeIdCmd.ExecuteScalar();
            if (assetTypeIdObj == null) throw new Exception($"{currency} adında bir varlık türü bulunamadı.");
            int assetTypeId = (int)assetTypeIdObj;

            var checkAssetCmd = new SqlCommand("SELECT asset_id FROM Assets WHERE wallet_id = @wallet_id AND asset_type_id = @asset_type_id", connection);
            checkAssetCmd.Parameters.AddWithValue("@wallet_id", walletId);
            checkAssetCmd.Parameters.AddWithValue("@asset_type_id", assetTypeId);
            object existingAssetId = checkAssetCmd.ExecuteScalar();

            SqlCommand finalCmd;
            if (existingAssetId != null)
            {
                finalCmd = new SqlCommand("UPDATE Assets SET amount = amount + @amount, updated_at = GETDATE() WHERE asset_id = @asset_id", connection);
                finalCmd.Parameters.AddWithValue("@asset_id", existingAssetId);
            }
            else
            {
                finalCmd = new SqlCommand("INSERT INTO Assets (wallet_id, asset_type_id, amount, created_at, updated_at) VALUES (@wallet_id, @asset_type_id, @amount, GETDATE(), GETDATE())", connection);
                finalCmd.Parameters.AddWithValue("@wallet_id", walletId);
                finalCmd.Parameters.AddWithValue("@asset_type_id", assetTypeId);
            }
            finalCmd.Parameters.AddWithValue("@amount", amount);
            finalCmd.ExecuteNonQuery();
        }
    }

    public static DataTable GetWalletTypes()
    {
        var dataTable = new DataTable();
        using (var connection = new SqlConnection(ConnectionString))
        {
            var command = new SqlCommand("SELECT wallet_type_id, type_name FROM WalletTypes", connection);
            var adapter = new SqlDataAdapter(command);
            adapter.Fill(dataTable);
        }
        return dataTable;
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

            int result = insertCommand.ExecuteNonQuery();
            return result > 0;
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
            int result = command.ExecuteNonQuery();
            return result > 0;
        }
    }

    public static bool ExecuteTrade(int userId, int fromWalletId, int fromAssetTypeId, decimal fromAmount, int toWalletId, int toAssetTypeId, decimal toAmount, decimal price, string tradeType)
    {
        using (var connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var decreaseCmd = new SqlCommand("UPDATE Assets SET amount = amount - @fromAmount WHERE wallet_id = @fromWalletId AND asset_type_id = @fromAssetTypeId AND amount >= @fromAmount", transaction.Connection, transaction);
                    decreaseCmd.Parameters.AddWithValue("@fromAmount", fromAmount);
                    decreaseCmd.Parameters.AddWithValue("@fromWalletId", fromWalletId);
                    decreaseCmd.Parameters.AddWithValue("@fromAssetTypeId", fromAssetTypeId);

                    int rowsAffected = decreaseCmd.ExecuteNonQuery();
                    if (rowsAffected == 0) throw new Exception("Yetersiz bakiye veya varlık bulunamadı.");

                    object? existingToAssetId = new SqlCommand("SELECT asset_id FROM Assets WHERE wallet_id = @toWalletId AND asset_type_id = @toAssetTypeId", transaction.Connection, transaction)
                    { Parameters = { new SqlParameter("@toWalletId", toWalletId), new SqlParameter("@toAssetTypeId", toAssetTypeId) } }.ExecuteScalar();

                    SqlCommand addOrUpdateCmd;
                    if (existingToAssetId != null && existingToAssetId != DBNull.Value)
                    {
                        addOrUpdateCmd = new SqlCommand("UPDATE Assets SET amount = amount + @toAmount, updated_at = GETDATE() WHERE asset_id = @asset_id", transaction.Connection, transaction);
                        addOrUpdateCmd.Parameters.AddWithValue("@asset_id", existingToAssetId);
                    }
                    else
                    {
                        addOrUpdateCmd = new SqlCommand("INSERT INTO Assets (wallet_id, asset_type_id, amount, created_at, updated_at) VALUES (@wallet_id, @asset_type_id, @toAmount, GETDATE(), GETDATE())", transaction.Connection, transaction);
                        addOrUpdateCmd.Parameters.AddWithValue("@wallet_id", toWalletId);
                        // DÜZELTME: SQL'deki @asset_type_id parametresini burada ekliyoruz.
                        addOrUpdateCmd.Parameters.AddWithValue("@asset_type_id", toAssetTypeId);
                    }

                    // ESKİ HATALI KODDA BU SATIR YANLIŞ YERDEYDİ VE INSERT İÇİN ÇALIŞMIYORDU.
                    // addOrUpdateCmd.Parameters.AddWithValue("@toAssetTypeId", toAssetTypeId);

                    addOrUpdateCmd.Parameters.AddWithValue("@toAmount", toAmount);
                    addOrUpdateCmd.ExecuteNonQuery();

                    var logTradeCmd = new SqlCommand("INSERT INTO Trades (user_id, asset_type_id, trade_type, amount, price, total_cost, trade_date) VALUES (@user_id, @asset_type_id, @trade_type, @amount, @price, @total_cost, GETDATE())", transaction.Connection, transaction);
                    logTradeCmd.Parameters.AddWithValue("@user_id", userId);
                    logTradeCmd.Parameters.AddWithValue("@asset_type_id", tradeType == "BUY" ? toAssetTypeId : fromAssetTypeId);
                    logTradeCmd.Parameters.AddWithValue("@trade_type", tradeType);
                    logTradeCmd.Parameters.AddWithValue("@amount", tradeType == "BUY" ? toAmount : fromAmount);
                    logTradeCmd.Parameters.AddWithValue("@price", price);
                    logTradeCmd.Parameters.AddWithValue("@total_cost", tradeType == "BUY" ? fromAmount : toAmount);
                    logTradeCmd.ExecuteNonQuery();

                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }

    public static bool CreateWallet(int userId, string walletName, int walletTypeId)
    {
        using (var connection = new SqlConnection(ConnectionString))
        {
            var command = new SqlCommand(
                "INSERT INTO Wallets (user_id, wallet_name, wallet_type_id, created_at, updated_at) VALUES (@user_id, @wallet_name, @wallet_type_id, GETDATE(), GETDATE())",
                connection);
            command.Parameters.AddWithValue("@user_id", userId);
            command.Parameters.AddWithValue("@wallet_name", walletName);
            command.Parameters.AddWithValue("@wallet_type_id", walletTypeId);
            connection.Open();
            int result = command.ExecuteNonQuery();
            return result > 0;
        }
    }

    #endregion
}