using System.Net.Http;

namespace Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Helpers
{
    public interface IHttpClientFactory
    {
        HttpClient CreateHttpClient();
    }
}