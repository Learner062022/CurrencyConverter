using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static DylanDeSouzaCurrencyConverter.APIHandler;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace DylanDeSouzaCurrencyConverter
{
    // Handle interactions between View and Model
    internal class MainPageViewModel : INotifyPropertyChanged
    {
        private APIHandler apiHandler = new APIHandler();
        private Currency currency;

        public Currency Currency
        {
            get => currency;
            set
            {
                if (currency != value)
                {
                    currency = value;
                    OnPropertyChanged();  // Notify the view of the change
                }
            }
        }

        public async Task ConvertCurrency()
        {
            // Conversion logic
            Currency = await apiHandler.FetchCurrencyDataAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
