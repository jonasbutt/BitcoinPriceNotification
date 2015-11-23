using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Http;

namespace Jonas.BitcoinPriceNotification.Robot.Services
{
    internal class HttpService : IHttpService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ISerializationHelper serializationHelper;

        public HttpService(
            ISerializationHelper serializationHelper, 
            IHttpClientFactory httpClientFactory)
        {
            this.serializationHelper = serializationHelper;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<T> GetAsync<T>(string requestUrl)
        {
            var httpClient = this.httpClientFactory.CreateHttpClient();
            var json = await httpClient.GetStringAsync(requestUrl);
            return this.serializationHelper.DeserializeJson<T>(json);
        }

        public async Task<T> GetAsync<T>(string requestUrl, object data)
        {
            var queryString = this.serializationHelper.SerializeToQueryString(data);
            var httpClient = this.httpClientFactory.CreateHttpClient();
            var json = await httpClient.GetStringAsync($"{requestUrl}?{queryString}");
            return this.serializationHelper.DeserializeJson<T>(json);
        }

        public async Task<T> PostAsync<T>(string requestUrl, object data)
        {
            var httpClient = this.httpClientFactory.CreateHttpClient();
            var dataAsJson = this.serializationHelper.SerializeToJson(data);
            var requestContent = new StringContent(dataAsJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(requestUrl, requestContent);
            var json = await response.Content.ReadAsStringAsync();
            return this.serializationHelper.DeserializeJson<T>(json);
        }
    }
}