using System.Threading.Tasks;

namespace Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces
{
    public interface IBitcoinExchangeRatesService
    {
        Task<decimal> RetrieveBuyRateInEuro();

        Task<decimal> RetrieveBuyRateInBtc();

        Task<decimal> RetrieveSellRateInEuro();

        Task<decimal> RetrieveSellRateInBtc();
    }
}