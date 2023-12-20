using WIL_DesktopApp.Models;

namespace WIL_DesktopApp.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly PastelApi pastelApi;

        // Constructor
        public QuoteService(string apiUrl)
        {
            pastelApi = new PastelApi(apiUrl);
        }

        public bool SaveQuote(Quote quote)
        {
            return pastelApi.SaveQuoteAsync(quote).GetAwaiter().GetResult();
        }
    }
}
