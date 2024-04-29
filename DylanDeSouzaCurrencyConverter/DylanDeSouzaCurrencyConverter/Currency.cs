using System;
using System.Collections.Generic;
using System.Text;

namespace DylanDeSouzaCurrencyConverter
{
    public class Currency
    {
        public string ConvertTo {  get; set; }
        public string AmountAustralianDollars { get; set; }
        public double ConversionRate { get; set; }
        public double AmountForiegnCurrency { get; set; }

        // This will give you all the currency data as compared to the US dollar (USD) as JSON. You will need to extract the 
        // information you want by creating a class that matches the API data and then deserialising it into an object (or objects)
        // of that class. You may need to do some research into JSON and may also find it useful to serialise an object of your
        // class in order to compare its JSON with what you get from the API.If they match, you should be good
    }
}
