using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DylanDeSouzaCurrencyConverter
{
    public class APIHandler
    {
        private const string _baseUrl = "https://openexchangerates.org/api/";
        private const string _appId = "744c0e65d9ae400eae78bcbf1151ff54";
        private const string _endpoint = "latest.json";
        private static HttpClient _client = new HttpClient();

        public async Task<Dictionary<string, decimal>> GetRates()
        {
            string apiCall = $"{_baseUrl}{_endpoint}?app_id={_appId}";
            try
            {
                HttpResponseMessage response = await _client.GetAsync(apiCall);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    try
                    {
                        CurrencyApiResponse apiResponse = JsonConvert.DeserializeObject<CurrencyApiResponse>(content);
                        if (apiResponse?.Rates != null)
                        {
                            return apiResponse.Rates;
                        }
                        else
                        {
                            Debug.WriteLine("No rates data found or deserialization failed.");
                            return null;
                        }
                    }
                    catch (JsonException jsonEx)
                    {
                        Debug.WriteLine($"JSON parsing error: {jsonEx.Message}");
                        return null;
                    }
                }
                else
                {
                    if ((int)response.StatusCode == 429)
                    {
                        Debug.WriteLine("API rate limit exceeded.");
                        return null;
                    }
                    else
                    {
                        Debug.WriteLine($"API call failed: {response.StatusCode}");
                        return null; 
                    }
                }
            }
            catch (HttpRequestException httpEx)
            {
                Debug.WriteLine($"HTTP request error: {httpEx.Message}");
                return null; 
            }
            catch (TaskCanceledException tcEx)
            {
                Debug.WriteLine($"Request canceled: {tcEx.Message}");
                return null;
            }
        }
    }
}