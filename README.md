# CurrencyConverter

## Description

This app allows users to enter an amount in Australian dollars (AUD) and convert it to another currency selected from a scrolling list view. Exchange rates are fetched from the Open Exchange Rates API. The base currency of the exchange rates is the US dollar (USD).

## Installation

1. **Clone the repository:**
    ```sh
    git clone https://github.com/yourusername/DylanDeSouzaSimpleCurrencyConverter.git
    ```
2. **Navigate to the project directory:**
    ```sh
    cd DylanDeSouzaSimpleCurrencyConverter
    ```
3. **Open the solution in Visual Studio:**
    ```sh
    start DylanDeSouzaSimpleCurrencyConverter.sln
    ```
4. **Run the project:**
    - In Visual Studio, select `Debug > Start Debugging`.

## Usage

1. **Enter an amount in AUD using the number pad.**
2. **Select a currency to convert to from the scrolling list view.**
3. **The converted amount will be displayed below.**

## Handling API Data

1. **Create a class to match the API data:**
    ```csharp
    public class ExchangeRates
    {
        public string Base { get; set; }
        public Dictionary<string, double> Rates { get; set; }
    }
    ```

2. **Deserialize the JSON data:**
    ```csharp
    var json = await httpClient.GetStringAsync(apiUrl);
    var exchangeRates = JsonConvert.DeserializeObject<ExchangeRates>(json);
    ```

3. **Perform the conversion:**
    - Convert AUD to USD using the `AUD` to `USD` rate from the `Rates` dictionary.
    - Convert the resulting USD amount to the selected currency using the `USD` to selected currency rate from the `Rates` dictionary.

## Error Handling

The app gracefully handles the following scenarios:
- **No internet connection:** Displays an error message.
- **Incorrect URL:** Displays an error message.
- **Incorrect API call:** Displays an error message.

## Testing

Ensure the app works correctly by performing the following tests:

1. **No internet connection:** Disable your internet connection and attempt to use the app.
2. **Incorrect URL:** Modify the API URL to an incorrect one and check for error handling.
3. **Incorrect API call:** Modify the API call path to an incorrect one and check for error handling.
4. **Verify conversions:** Use Google to convert the same amounts and compare the results. Slight differences are acceptable due to API update intervals.

## Acknowledgements

- [Xamarin](https://dotnet.microsoft.com/apps/xamarin)
- [Open Exchange Rates](https://openexchangerates.org/)
- [Newtonsoft.Json](https://www.newtonsoft.com/json)
