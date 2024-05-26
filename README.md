# CurrencyConverter

## Description

This app allows users to enter an amount in Australian dollars (AUD) and convert it to another currency selected from a scrolling list view. Exchange rates are fetched from the Open Exchange Rates API. The base currency of the exchange rates is the US dollar (USD).

## Installation

1. **Clone the repository:**
    ```sh
    git clone https://github.com/Learner062022/CurrencyConverter.git
    ```
2. **Navigate to the project directory:**
    ```sh
    cd CurrencyConverter
    ```
3. **Install the necessary dependencies:**
    ```sh
    npm install
    ```

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

## Contributing

1. **Fork the repository.**
2. **Create a new branch:**
    ```bash
    git checkout -b feature/your-feature-name
    ```
3. **Commit your changes:**
    ```bash
    git commit -m 'Add some feature'
    ```
4. **Push to the branch:**
    ```bash
    git push origin feature/your-feature-name
    ```
5. **Create a new pull request.

# License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Notice

This repository can be forked and modified, but cannot be claimed as your own. Credit the original creator when sharing or distributing modified versions.

## Acknowledgements

- [Xamarin](https://dotnet.microsoft.com/apps/xamarin)
- [Open Exchange Rates](https://openexchangerates.org/)
- [Newtonsoft.Json](https://www.newtonsoft.com/json)
