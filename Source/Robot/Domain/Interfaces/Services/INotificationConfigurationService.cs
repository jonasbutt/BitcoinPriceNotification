using Jonas.BitcoinPriceNotification.Robot.Domain.Model;

namespace Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Services
{
    public interface INotificationConfigurationService
    {
        NotificationConfiguration CreateFromArguments(string[] arguments);
    }
}