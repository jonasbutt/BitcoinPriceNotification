using System.Threading.Tasks;

namespace Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces
{
    public interface IBitcoinPriceNotificationRobot
    {
        Task Execute(string[] arguments);
    }
}