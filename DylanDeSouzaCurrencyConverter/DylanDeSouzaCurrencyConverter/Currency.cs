using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DylanDeSouzaCurrencyConverter
{
    public class Currency : INotifyPropertyChanged
    {
        private decimal _amountAustralianDollars = 0m; 
        private decimal _amountForeignCurrency; 
        private decimal _rate;
        private string _name;
        private decimal _USDToAUD;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public decimal AmountAustralianDollars
        {
            get => _amountAustralianDollars;
            set
            {
                if (_amountAustralianDollars != value)
                {
                    _amountAustralianDollars = value;
                    OnPropertyChanged();
                    UpdateForeignCurrency();
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                    UpdateForeignCurrency();
                }
            }
        }

        public decimal Rate
        {
            get => _rate;
            set
            {
                if (_rate != value)
                {
                    _rate = value;
                    OnPropertyChanged();
                    UpdateForeignCurrency();
                }
            }
        }

        public decimal USDToAUD
        {
            get => _USDToAUD;
            set
            {
                if (_USDToAUD != value)
                {
                    _USDToAUD = value;
                    OnPropertyChanged();
                    UpdateForeignCurrency();
                }
            }
        }

        public decimal AmountForeignCurrency
        {
            get => _amountForeignCurrency;
            private set
            {
                if (_amountForeignCurrency != value)
                {
                    _amountForeignCurrency = value;
                    OnPropertyChanged();
                }
            }
        }

        public void Reset()
        {
            AmountAustralianDollars = 0m;
            Rate = 0;
            Name = null;
        } 

        public void DeleteLastDigit()
        {
            string amount = _amountAustralianDollars.ToString();
            int lengthAmount = amount.Length;
            if (lengthAmount == 1 && _amountAustralianDollars != 0m)
            {
                Reset();
            }
            else if (lengthAmount > 1)
            {
                amount = amount.Substring(0, lengthAmount - 1);
                AmountAustralianDollars = Convert.ToDecimal(amount);
            }
        }


        public void FindRateForCurrency(ObservableCollection<Currency> collection)
        {
            var currency = collection.FirstOrDefault(c => c.Name == _name);
            if (currency != null)
            {
                Rate = currency.Rate;
                UpdateForeignCurrency();
            }
        }

        void UpdateForeignCurrency()
        {
            decimal result = _amountAustralianDollars * _USDToAUD * _rate;
            AmountForeignCurrency = decimal.Round(result, 2, MidpointRounding.AwayFromZero);
        }
    }
}