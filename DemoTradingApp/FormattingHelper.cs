using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTradingApp
{ 
    public static class FormattingHelper
    {
        public static CultureInfo GetCultureInfoFor(string currency)
        {
            switch (currency.ToUpper())
            {
                case "EUR": return new CultureInfo("de-DE");
                case "TRY": return new CultureInfo("tr-TR");
                default: return new CultureInfo("en-US"); // USD ve diğerleri için varsayılan
            }
        }
    }
}
