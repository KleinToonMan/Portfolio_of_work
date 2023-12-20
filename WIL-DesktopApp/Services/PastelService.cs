using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WIL_DesktopApp.Models;

namespace WIL_DesktopApp.Services
{
    public class PastelApi
    {
        private readonly HttpClient httpClient; // Used to send http requests


        // Constructor
        public PastelApi(string apiUrl)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(apiUrl)
            };
        }

        // Send POST request to save a quote
        public async Task<bool> SaveQuoteAsync(Quote quote)
        {
            // Creates the HTTP request content with the serialized JSON data 
            // Using POST QuoteAttachment/Save API from Sage Accounting
            // Link: https://accounting.sageone.co.za/api/2.0.0/Help/Api/POST-QuoteAttachment-Save

            var requestBody = JsonSerializer.Serialize(quote);
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("QuoteAttachment/Save", content);

            return response.IsSuccessStatusCode;
        }
    }
}
