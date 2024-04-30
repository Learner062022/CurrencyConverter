using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DylanDeSouzaCurrencyConverter
{
    public class Currency
    {
        public string ConvertTo {  get; set; }
        public string AmountAustralianDollars { get; set; }
        public double ConversionRate { get; set; }
        public double AmountForiegnCurrency { get; set; }
    }
}
