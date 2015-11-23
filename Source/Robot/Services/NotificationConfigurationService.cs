using CommandLine;
using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces;
using Jonas.BitcoinPriceNotification.Robot.Domain.Model;

namespace Jonas.BitcoinPriceNotification.Robot.Services
{
    internal class NotificationConfigurationService : INotificationConfigurationService
    {
        public NotificationConfiguration CreateFromArguments(string[] arguments)
        {
            var notificationConfiguration = new NotificationConfiguration();
            var parseSucceeded = Parser.Default.ParseArguments(arguments, notificationConfiguration);
            if (parseSucceeded == false
                || notificationConfiguration.PriceType == PriceType.None
                || notificationConfiguration.PriceThreshold <= 0
                || notificationConfiguration.Currency == Currency.None
                || string.IsNullOrWhiteSpace(notificationConfiguration.EmailAddress))
            {
                return NotificationConfiguration.Empty;
            }
            return notificationConfiguration;
        }
    }
}