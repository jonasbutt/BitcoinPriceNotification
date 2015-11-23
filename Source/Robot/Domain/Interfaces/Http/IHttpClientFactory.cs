using System.Net.Http;

namespace Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Http
{
    public interface IHttpClientFactory
    {
        HttpClient CreateHttpClient();
    }
}