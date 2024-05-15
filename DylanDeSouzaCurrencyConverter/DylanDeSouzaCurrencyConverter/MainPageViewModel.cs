using System.ComponentModel;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System;

namespace DylanDeSouzaCurrencyConverter
{
    internal class MainPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Currency> _collectionCurrencies = new ObservableCollection<Currency>();
        private Currency _currency;
        private APIHandler _apiHandler;
        

        private List<(string, string)> _operations = new List<(string, string)>();
        private string _currentOperation;
        private string _currentValue = "";
        private bool _deletable;
        public enum Operation { Add, Subtract, Multiply, Divide }
        public Dictionary<string, Operation> OperationMap { get; } = new Dictionary<string, Operation>
        {
            { "+", Operation.Add },
            { "-", Operation.Subtract },
            { "x", Operation.Multiply },
            { "DIV", Operation.Divide }
        };

        public MainPageViewModel(APIHandler apiHandler)
        {
            _apiHandler = apiHandler;
            _currency = new Currency();
            _ = LoadCurrencies();
        }

        void ResetCurrentValue() => _currentValue = "";

        void AddOperationAndPrepareForNew(string operation)
        {
            if (!string.IsNullOrEmpty(_currentValue))
            {
                AddOperationAndNumber(_currentOperation, _currentValue);
                ResetCurrentValue();
            }
            _currentOperation = operation;
        }

        public void ClearAll()
        {
            ResetCurrentValue();
            _currency.Reset();
            _operations.Clear();
            _currentOperation = null;
            _deletable = true;
        }

        decimal ApplyOperation(decimal prevValue, Operation operation, decimal currentValue)
        {
            decimal result = 0m;
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
                    result = currentValue == 0m ? 0m : prevValue / currentValue;
                    break;
            }
            return result;
        }

        public Currency Currency
        {
            get => _currency;
            set
            {
                if (_currency != value)
                {
                    _currency = value;
                    OnPropertyChanged();  
                }
            }
        }

        public bool Deletable
        {
            get => _deletable;
            set
            {
                if (_deletable != value)
                {
                    _deletable = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Currency> CollectionCurrencies
        {
            get => _collectionCurrencies;
            set
            {
                if (_collectionCurrencies != value)
                {
                    _collectionCurrencies = value;
                    OnPropertyChanged();
                }
            }
        }

        async Task LoadCurrencies()
        {
            var rates = await _apiHandler.GetRates();
            if (rates != null)
            {
                foreach (var rate in rates)
                {
                    string key = rate.Key;
                    decimal value = rate.Value;
                    if (key == "AUD")
                    {
                        _currency.USDToAUD = value; 
                    }
                    else
                    {
                        CollectionCurrencies.Add(new Currency { Name = key, Rate = value });
                    }
                }
            }
            else
            {
                Debug.WriteLine("Failed to load rates: API returned null.");
            }
        }

        public void AddOperationAndNumber(string operation, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                _operations.Add((operation, value));
            }
        }

        public void CalculateResult()
        {
            if (_operations.Count > 0)
            {
                decimal result = Convert.ToDecimal(_operations.First().Item2);
                string lastOperation = _operations.First().Item1;

                for (int i = 1; i < _operations.Count; i++)
                {
                    var (operation, value) = _operations[i];
                    if (!string.IsNullOrEmpty(operation))
                    {
                        result = ApplyOperation(result, OperationMap[operation], Convert.ToDecimal(value));
                        lastOperation = operation;
                    }
                }
                _currentValue = result.ToString();
                _currency.AmountAustralianDollars = Convert.ToDecimal(_currentValue);
                _operations.Clear();
            }
        }

        public void AppendButtonText(string buttonText)
        {
            if (!IsNumericOrDecimal(buttonText)) return;

            if (_currentValue == "")
            {
                _currentValue = buttonText == "." ? "0." : buttonText;
            }
            else if (_currentValue == "0" && buttonText != ".")
            {
                _currentValue = buttonText;
            }
            else if (buttonText == "." && !_currentValue.Contains("."))
            {
                _currentValue += ".";
            }
            else if (int.TryParse(buttonText, out _))
            {
                if (!(_currentValue == "0" && buttonText == "0"))
                {
                    if (_currentValue.Contains("."))
                    {
                        int decimalIndex = _currentValue.IndexOf(".");
                        int digitsAfterDecimal = _currentValue.Length - decimalIndex - 1;
                        if (digitsAfterDecimal >= 2) return;
                    }
                    _currentValue += buttonText;
                }
            }
        }

        public void ProcessEquals()
        {
            if (!string.IsNullOrEmpty(_currentValue))
            {
                AddOperationAndPrepareForNew(null);
                CalculateResult();
            }
            Deletable = false;
        }

        public void HandleOperations(string operation)
        {
            if (!string.IsNullOrEmpty(_currentValue) || _operations.Count > 0)
            {
                AddOperationAndPrepareForNew(operation);
            }
        }

        public void AppendAndConvert(string input)
        {
            AppendButtonText(input);
            _currency.AmountAustralianDollars = Convert.ToDecimal(_currentValue);
            Deletable = true;
        }

        public bool IsNumericOrDecimal(string input) => int.TryParse(input, out _) || input == ".";

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}