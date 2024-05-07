using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static DylanDeSouzaCurrencyConverter.APIHandler;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using static DylanDeSouzaCurrencyConverter.MainPage;
using System.Windows.Input;
using Xamarin.Forms;

namespace DylanDeSouzaCurrencyConverter
{
    internal class MainPageViewModel : INotifyPropertyChanged
    {
        private APIHandler _apiHandler = new APIHandler();
        private Currency _currency;
        public ObservableCollection<Currency> CurrencyList = new ObservableCollection<Currency>();
        public ICommand ButtonCommand { get; private set; } // Add this line
       

        public MainPageViewModel()
        {
            _currency = new Currency();
        }

        public Currency Currency
        {
            get => _currency;
            set
            {
                if (_currency != value)
                {
                    _currency = value;
                    OnPropertyChanged();  // Notify the view of the change
                }
            }
        }

        public async Task ConvertCurrency()
        {
            // Conversion logic
            // Currency = await _apiHandler.FetchCurrencyDataAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}