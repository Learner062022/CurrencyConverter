using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DylanDeSouzaCurrencyConverter
{
    public class Currency : INotifyPropertyChanged
    {
        private string _amountAustralianDollars = "0";
        private string _amountForeignCurrency = "0";
        private string _conversionRate;
        private string _convertTo;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AppendDigit(string digit)
        {
            if (_amountAustralianDollars == "0")
            {
                AmountAustralianDollars = digit;
            }
            else
            {
                AmountAustralianDollars += digit;
            }
        }

        public void Clear() => AmountAustralianDollars = "0";

        public void DeleteLastDigit()
        {
            if (_amountAustralianDollars.Length == 1 && _amountAustralianDollars != "0")
            {
                AmountAustralianDollars = "0";
            }
            else if (_amountAustralianDollars.Length > 1)
            {
                AmountAustralianDollars = AmountAustralianDollars.Substring(0, AmountAustralianDollars.Length - 1);
            }
        }

        public void AddDecimal()
        {
            if (!_amountAustralianDollars.Contains("."))
            {
                AmountAustralianDollars += ".";
            }
        }

        public string AmountAustralianDollars
        {
            get => _amountAustralianDollars;
            set
            {
                if (_amountAustralianDollars != value)
                {
                    _amountAustralianDollars = value;
                    OnPropertyChanged(nameof(AmountAustralianDollars));
                    UpdateForeignCurrency();
                }
            }
        }

        public string ConvertTo
        {
            get => _convertTo;
            set
            {
                if (_convertTo != value)
                {
                    _convertTo = value;
                    OnPropertyChanged(nameof(_convertTo));
                }
            }
        }

        public string AmountForeignCurrency
        {
            get => _amountForeignCurrency;
            private set
            {
                if (_amountForeignCurrency != value)
                {
                    _amountForeignCurrency = value;
                    OnPropertyChanged(nameof(AmountForeignCurrency));
                }
            }
        }

        public string ConversionRate
        {
            get => _conversionRate;
            set
            {
                if (_conversionRate != value)
                {
                    _conversionRate = value;
                    OnPropertyChanged(nameof(ConversionRate));
                    UpdateForeignCurrency();
                }
            }
        }

        void UpdateForeignCurrency()
        {
            if (double.TryParse(_amountAustralianDollars, out double amountAustralianDollars) &&
                double.TryParse(_conversionRate, out double conversionRate))
            {
                AmountForeignCurrency = (amountAustralianDollars * conversionRate).ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                AmountForeignCurrency = "0";
            }
        }
    }
}