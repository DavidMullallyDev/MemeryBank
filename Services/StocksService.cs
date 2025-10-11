using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ServiceContracts;

namespace Services
{
    public class StocksService: IStocksService //IDisposable 
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public Dictionary<string, object> StockPriceQuote;
        private readonly IConfiguration _configuration;

        public StocksService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol) 
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient()) 
            {
                HttpRequestMessage httpRequestMessage = new()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get,
                    //Headers = new Dictionary<string, string>() { },
                };

                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                Stream stream = httpResponseMessage.Content.ReadAsStream();

                StreamReader streamReader= new StreamReader(stream);
                string response = streamReader.ReadToEnd();

                Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);
                if (responseDictionary == null) throw new InvalidOperationException("No response from Finnhub server");
                if (responseDictionary.ContainsKey("error")) throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));
                return responseDictionary;
            }
        }
    }
}
