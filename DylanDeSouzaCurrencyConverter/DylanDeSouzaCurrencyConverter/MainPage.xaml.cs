using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static DylanDeSouzaCurrencyConverter.MainPage;
using static DylanDeSouzaCurrencyConverter.MainPageViewModel;

namespace DylanDeSouzaCurrencyConverter
{
    public partial class MainPage : ContentPage
    {
        private Currency _currency;
        public enum Operation { Add, Subtract, Multiply, Divide }
        private string _currentOperation;
        private readonly List<(string, string)> _operations = new List<(string, string)>();
        private string _currentValue = "";
        private readonly Dictionary<string, Operation> _operationMap = new Dictionary<string, Operation>
        {
            { "+", Operation.Add },
            { "-", Operation.Subtract },
            { "x", Operation.Multiply },
            { "DIV", Operation.Divide }
        };
        private bool _deletable;

        public MainPage()
        {
            InitializeComponent();
            _currency = new Currency();
            BindingContext = _currency;
        }

        void AddOperationAndNumber(string operation, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _operations.Add((operation, value));
            }
        }

        double ApplyOperation(double prevValue, Operation operation, double currentValue)
        {
            double result = 0;
            switch (operation)
            {
                case Operation.Add:
                    result = prevValue + currentValue;
                    break;
                case Operation.Subtract:
                    result = prevValue - currentValue;
                    break;
                case Operation.Multiply:
                    result = prevValue * currentValue;
                    break;
                case Operation.Divide:
                    result = currentValue == 0 ? double.NaN : prevValue / currentValue;
                    break;
            }
            return result;
        }

        void CalculateResult()
        {
            if (_operations.Count > 0)
            {
                double result = double.Parse(_operations.First().Item2);
                string lastOperation = _operations.First().Item1;

                for (int i = 1; i < _operations.Count; i++)
                {
                    var (operation, value) = _operations[i];
                    if (!string.IsNullOrEmpty(operation))
                    {
                        result = ApplyOperation(result, _operationMap[operation], double.Parse(value));
                        lastOperation = operation;
                    }
                }
                _currentValue = result.ToString();
                _currency.AmountAustralianDollars = _currentValue;
                _operations.Clear();
            }
        }

        void AppendButtonText(string buttonText)
        {
            if (int.TryParse(buttonText, out int _) || buttonText == ".")
            {
                if (_currentValue == "" || _currentValue == "0")
                {
                    _currentValue = buttonText == "." ? "0." : buttonText;
                }
                else if (_currentValue != "" && !_currentValue.Contains("."))
                {
                    _currentValue += buttonText;
                }
                else if (buttonText != ".")
                {
                    _currentValue += buttonText;
                }
            }
        }

        void Button_Clicked(object sender, EventArgs e) 
        {
            Button button = (Button)sender;
            string buttonText = button.Text;
            switch (buttonText)
            {
                case "C":
                    _currentValue = "0";
                    _currency.AmountAustralianDollars = _currentValue;
                    _operations.Clear();
                    _currentOperation = null;
                    _deletable = true;
                    return;
                case "DEL":
                    if (_deletable && _currentValue.Length > 1)
                    {
                        _currentValue = _currentValue.Substring(0, _currentValue.Length - 1);
                    }
                    else
                    {
                        _currentValue = "0";
                    }
                    _currency.AmountAustralianDollars = _currentValue;
                    return;
                case "=":
                    if (!string.IsNullOrEmpty(_currentValue))
                    {
                        AddOperationAndNumber(_currentOperation, _currentValue);
                        CalculateResult();
                        _currentOperation = null;
                    }
                    _deletable = false;
                    return;
            }

            if (int.TryParse(buttonText, out int _) || buttonText == ".")
            {
                AppendButtonText(buttonText);
                _currency.AmountAustralianDollars = _currentValue;
                _deletable = true;
                return;
            }

            if (_operationMap.ContainsKey(buttonText))
            {
                if (!string.IsNullOrEmpty(_currentValue))
                {
                    AddOperationAndNumber(_currentOperation, _currentValue);
                    _currentOperation = buttonText;
                    _currentValue = "";
                    _deletable = true;
                }
                else if (_operations.Count > 0)
                {
                    _operations[_operations.Count - 1] = (buttonText, _operations.Last().Item2);
                }
                _currentOperation = buttonText;
                return;
            }
        }

        void CurrenciesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            _currency.ConvertTo = e.Item.ToString();
        }
    }
}