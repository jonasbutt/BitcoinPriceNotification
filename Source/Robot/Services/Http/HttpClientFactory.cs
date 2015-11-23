using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Http;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Jonas.BitcoinPriceNotification.Robot.Services.Http
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