using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using DemoTradingApp;

namespace DemoTradingApp
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = "";
        public string? Email { get; set; }
    }


    public class PriceInfo
    {
        [JsonPropertyName("usd")]
        public decimal Usd { get; set; }

        [JsonPropertyName("eur")]
        public decimal Eur { get; set; }

        [JsonPropertyName("try")]
        public decimal Try { get; set; }
    }
}