using System.Threading.Tasks;

namespace Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Services
{
    public interface IBitcoinPriceNotificationRobot
    {
        Task Execute(string[] arguments);
    }
}