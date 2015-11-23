using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Services;
using Jonas.BitcoinPriceNotification.Robot.Domain.Model;
using System.Threading.Tasks;

namespace Jonas.BitcoinPriceNotification.Robot.Services
{
    internal class BitcoinExchangeRatesService : IBitcoinExchangeRatesService
    {
        private readonly ISettingsService settingsService;
        private readonly IHttpService httpService;

        public BitcoinExchangeRatesService(
            ISettingsService settingsService, IHttpService httpService)
        {
            this.settingsService = settingsService;
            this.httpService = httpService;
        }

        public async Task<decimal> RetrieveBuyRateInEuro()
        {
            var rateReponse = await this.httpService.GetAsync<ExchangeRateResponse>(
                this.settingsService.GetBitonicUrl(), 
                new ExchangeRateRequest { OrderType = "buy", Part = "rate_convert", Btc = 1 });
            return rateReponse.Euros;
        }

        public async Task<decimal> RetrieveBuyRateInBtc()
        {
            var rateReponse = await this.httpService.GetAsync<ExchangeRateResponse>(
                this.settingsService.GetBitonicUrl(), 
                new ExchangeRateRequest { OrderType = "buy", Part = "rate_convert", Euros = 100 });
            return rateReponse.Btc;
        }

        public async Task<decimal> RetrieveSellRateInEuro()
        {
            var rateReponse = await this.httpService.GetAsync<ExchangeRateResponse>(
                this.settingsService.GetBitonicUrl(), 
                new ExchangeRateRequest { OrderType = "sell", Part = "offer", Btc = 1 });
            return rateReponse.Euros;
        }

        public async Task<decimal> RetrieveSellRateInBtc()
        {
            var rateReponse = await this.httpService.GetAsync<ExchangeRateResponse>(
                this.settingsService.GetBitonicUrl(), 
                new ExchangeRateRequest { OrderType = "sell", Part = "offer", Euros = 100 });
            return rateReponse.Btc;
        }
    }
}