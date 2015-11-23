using System.Net.Http;
using System.Net.Http.Headers;
using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Http;

namespace Jonas.BitcoinPriceNotification.Robot.Helpers
{
    internal class HttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }
    }
}