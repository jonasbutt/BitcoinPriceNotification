using System.Threading.Tasks;

namespace Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Http
{
    public interface IHttpService
    {
        Task<T> GetAsync<T>(string requestUrl);

        Task<T> GetAsync<T>(string requestUrl, object data);

        Task<T> PostAsync<T>(string requestUrl, object data);
    }
}