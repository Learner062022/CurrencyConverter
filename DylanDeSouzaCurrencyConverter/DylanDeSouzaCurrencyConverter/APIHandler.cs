using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace DylanDeSouzaCurrencyConverter
{
    public class APIHandler
    {
        public string apiUrl = "https://openexchangerates.org/api/latest.json?app_id=744c0e65d9ae400eae78bcbf1151ff54";

        public async Task<Currency> FetchCurrencyDataAsync()
        {
            // Implement API call logic here
        }
    }
}
