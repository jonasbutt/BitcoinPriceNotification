using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces;
using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Smtp;
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
                decimal rate = 0;
                string currency = string.Empty;
                if (notificationConfiguration.Currency == Currency.Euro)
                {
                    currency = "EUR";
                    rate = await this.bitcoinExchangeRatesService.RetrieveBuyRateInEuro();
                }
                else if (notificationConfiguration.Currency == Currency.Bitcoin)
                {
                    currency = "BTC";
                    rate = await this.bitcoinExchangeRatesService.RetrieveBuyRateInBtc();
                }

                if (rate < Convert.ToDecimal(notificationConfiguration.PriceThreshold))
                {
                    var notification = $"Buy now! Current rate is {rate} {currency}";
                    this.smtpService.SendEmail(notificationConfiguration.EmailAddress, notification);
                    this.outputService.OutputSuccess("Notification e-mail sent to " + notificationConfiguration.EmailAddress);
                    return;
                }
            }
            else if (notificationConfiguration.PriceType == PriceType.Sell)
            {
                decimal rate = 0;
                string currency = string.Empty;
                if (notificationConfiguration.Currency == Currency.Euro)
                {
                    currency = "EUR";
                    rate = await this.bitcoinExchangeRatesService.RetrieveSellRateInEuro();
                }
                else if (notificationConfiguration.Currency == Currency.Bitcoin)
                {
                    currency = "BTC";
                    rate = await this.bitcoinExchangeRatesService.RetrieveSellRateInBtc();
                }

                if (rate > Convert.ToDecimal(notificationConfiguration.PriceThreshold))
                {
                    var notification = $"Sell now! Current rate is {rate} {currency}";
                    this.smtpService.SendEmail(notificationConfiguration.EmailAddress, notification);
                    this.outputService.OutputSuccess("Notification e-mail sent to " + notificationConfiguration.EmailAddress);
                    return;
                }
            }

            this.outputService.OutputInfo("Finished without notification.");
        }
    }
}