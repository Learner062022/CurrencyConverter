using System;
using Xamarin.Forms;

namespace DylanDeSouzaCurrencyConverter
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainPageViewModel(new APIHandler());
            BindingContext = _viewModel;
        }

        void Button_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Text;

            switch (buttonText)
            {
                case "C":
                    _viewModel.ClearAll();
                    break;
                case "DEL":
                    if (_viewModel.Deletable)
                        _viewModel.Currency.DeleteLastDigit(); 
                    break;
                case "=":
                    _viewModel.ProcessEquals();
                    break;
                default:
                    if (_viewModel.IsNumericOrDecimal(buttonText))
                    {
                        _viewModel.AppendAndConvert(buttonText);
                    }
                    else if (_viewModel.OperationMap.ContainsKey(buttonText))
                    {
                        _viewModel.HandleOperations(buttonText);
                    }
                    break;
            }
        }

        void CurrencySelected(object sender, SelectedItemChangedEventArgs e)
        {
            Currency selectedCurrency = (Currency)e.SelectedItem;
            _viewModel.Currency.Name = selectedCurrency.Name;
            _viewModel.Currency.FindRateForCurrency(_viewModel.CollectionCurrencies);
        }
    }
}