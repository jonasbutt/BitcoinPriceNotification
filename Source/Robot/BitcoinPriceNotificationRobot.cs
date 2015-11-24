using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Services;
using Jonas.BitcoinPriceNotification.Robot.Domain.Model;
using System;
using System.Threading.Tasks;

namespace Jonas.BitcoinPriceNotification.Robot
{
    internal class BitcoinPriceNotificationRobot : IBitcoinPriceNotificationRobot
    {
        private readonly INotificationConfigurationService notificationConfigurationService;
        private readonly IBitcoinExchangeRatesService bitcoinExchangeRatesService;
        private readonly ISmtpService smtpService;
        private readonly IOutputService outputService;

        public BitcoinPriceNotificationRobot(
            INotificationConfigurationService notificationConfigurationService, 
            IBitcoinExchangeRatesService bitcoinExchangeRatesService, 
            ISmtpService smtpService, 
            IOutputService outputService)
        {
            this.notificationConfigurationService = notificationConfigurationService;
            this.bitcoinExchangeRatesService = bitcoinExchangeRatesService;
            this.smtpService = smtpService;
            this.outputService = outputService;
        }

        public async Task Execute(string[] arguments)
        {
            var notificationConfiguration = this.notificationConfigurationService.CreateFromArguments(arguments);
            if (notificationConfiguration == NotificationConfiguration.Empty)
            {
                this.outputService.OutputError("Invalid notification configuration!");
                return;
            }

            if (notificationConfiguration.PriceType == PriceType.Buy)
            {
                await ExecuteBuyRequest(notificationConfiguration);
            }
            else if (notificationConfiguration.PriceType == PriceType.Sell)
            {
                await ExecuteSellRequest(notificationConfiguration);
            }
        }

        private async Task ExecuteBuyRequest(NotificationConfiguration notificationConfiguration)
        {
            var shouldBuy = false;
            decimal rate = 0;
            var currency = string.Empty;
            if (notificationConfiguration.Currency == Currency.Euro)
            {
                currency = "EUR for 1 BTC";
                rate = await this.bitcoinExchangeRatesService.RetrieveBuyRateInEuro();
                shouldBuy = rate < Convert.ToDecimal(notificationConfiguration.PriceThreshold);
            }
            else if (notificationConfiguration.Currency == Currency.Bitcoin)
            {
                currency = "BTC for 100 EUR";
                rate = await this.bitcoinExchangeRatesService.RetrieveBuyRateInBtc();
                shouldBuy = rate > Convert.ToDecimal(notificationConfiguration.PriceThreshold);
            }

            if (shouldBuy)
            {
                var notification = $"Buy now! Current rate is {rate} {currency}";
                this.smtpService.SendEmail(notificationConfiguration.EmailAddress, notification);
                this.outputService.OutputSuccess("Notification e-mail sent to " + notificationConfiguration.EmailAddress);
            }
            else
            {
                this.outputService.OutputInfo("Finished without notification.");
            }
        }

        private async Task ExecuteSellRequest(NotificationConfiguration notificationConfiguration)
        {
            var shouldSell = false;
            decimal rate = 0;
            var currency = string.Empty;
            if (notificationConfiguration.Currency == Currency.Euro)
            {
                currency = "EUR for 1 BTC";
                rate = await this.bitcoinExchangeRatesService.RetrieveSellRateInEuro();
                shouldSell = rate > Convert.ToDecimal(notificationConfiguration.PriceThreshold);
            }
            else if (notificationConfiguration.Currency == Currency.Bitcoin)
            {
                currency = "BTC for 100 EUR";
                rate = await this.bitcoinExchangeRatesService.RetrieveSellRateInBtc();
                shouldSell = rate < Convert.ToDecimal(notificationConfiguration.PriceThreshold);
            }

            if (shouldSell)
            {
                var notification = $"Sell now! Current rate is {rate} {currency}";
                this.smtpService.SendEmail(notificationConfiguration.EmailAddress, notification);
                this.outputService.OutputSuccess("Notification e-mail sent to " + notificationConfiguration.EmailAddress);
            }
            else
            {
                this.outputService.OutputInfo("Finished without notification.");
            }
        }
    }
}