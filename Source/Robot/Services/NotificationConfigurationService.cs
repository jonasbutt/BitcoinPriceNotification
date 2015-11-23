using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Helpers;
using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Services;
using Jonas.BitcoinPriceNotification.Robot.Domain.Model;

namespace Jonas.BitcoinPriceNotification.Robot.Services
{
    internal class NotificationConfigurationService : INotificationConfigurationService
    {
        private readonly ICommandLineParser commandLineParser;

        public NotificationConfigurationService(ICommandLineParser commandLineParser)
        {
            this.commandLineParser = commandLineParser;
        }

        public NotificationConfiguration CreateFromArguments(string[] arguments)
        {
            var notificationConfiguration = this.commandLineParser.Parse<NotificationConfiguration>(arguments);
            if (notificationConfiguration.PriceType == PriceType.None
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