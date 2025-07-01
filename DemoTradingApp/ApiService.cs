using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using DemoTradingApp;
namespace DemoTradingApp
{
    public static class ApiService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<Dictionary<string, PriceInfo>?> GetCryptoPricesAsync(List<string> coinIds)
        {
            string ids = string.Join(",", coinIds);
            string url = $"https://api.coingecko.com/api/v3/simple/price?ids={ids}&vs_currencies=usd,eur,try";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var prices = JsonSerializer.Deserialize<Dictionary<string, PriceInfo>>(jsonResponse);

                if (prices != null)
                {
                    foreach (var priceEntry in prices)
                    {
                        int? assetTypeId = DatabaseHelper.GetAssetTypeIdByName(priceEntry.Key);
                        if (assetTypeId.HasValue)
                        {

                            DatabaseHelper.SavePriceToDatabase(assetTypeId.Value, priceEntry.Value.Usd);
                        }
                    }
                }
                return prices;
            }
            catch (Exception e)
            {
                Console.WriteLine($"API isteği hatası, veritabanından okunuyor: {e.Message}");
                var pricesFromDb = new Dictionary<string, PriceInfo>();
                foreach (var id in coinIds)
                {
                    int? assetTypeId = DatabaseHelper.GetAssetTypeIdByName(id);
                    if (assetTypeId.HasValue)
                    {
                        decimal? priceUsd = DatabaseHelper.GetPriceFromDatabase(assetTypeId.Value);
                        if (priceUsd.HasValue)
                        {

                            pricesFromDb[id] = new PriceInfo { Usd = priceUsd.Value, Eur = 0, Try = 0 };
                        }
                    }
                }
                return pricesFromDb.Count > 0 ? pricesFromDb : null;
            }
        }
        public static async Task<PriceInfo?> GetExchangeRatesAsync()
        {
            string url = "https://api.coingecko.com/api/v3/simple/price?ids=usd&vs_currencies=usd,eur,try";
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var rawData = JsonSerializer.Deserialize<Dictionary<string, PriceInfo>>(jsonResponse);
                if (rawData != null && rawData.ContainsKey("usd"))
                {
                    var exchangeRates = rawData["usd"];

                    int? eurAssetTypeId = DatabaseHelper.GetAssetTypeIdByName("EUR");
                    int? tryAssetTypeId = DatabaseHelper.GetAssetTypeIdByName("TRY");
                    if (eurAssetTypeId.HasValue)
                        DatabaseHelper.SavePriceToDatabase(eurAssetTypeId.Value, exchangeRates.Eur);
                    if (tryAssetTypeId.HasValue)
                        DatabaseHelper.SavePriceToDatabase(tryAssetTypeId.Value, exchangeRates.Try);

                    return exchangeRates;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kur bilgisi alınırken hata, veritabanından okunuyor: {ex.Message}");
                int? eurAssetTypeId = DatabaseHelper.GetAssetTypeIdByName("EUR");
                int? tryAssetTypeId = DatabaseHelper.GetAssetTypeIdByName("TRY");
                decimal? eurRate = eurAssetTypeId.HasValue ? DatabaseHelper.GetPriceFromDatabase(eurAssetTypeId.Value) : null;
                decimal? tryRate = tryAssetTypeId.HasValue ? DatabaseHelper.GetPriceFromDatabase(tryAssetTypeId.Value) : null;
                if (eurRate.HasValue && tryRate.HasValue)
                {
                    return new PriceInfo { Usd = 1, Eur = eurRate.Value, Try = tryRate.Value };
                }
                return null;
            }
        }
    }
}