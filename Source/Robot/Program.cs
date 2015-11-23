using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Services;
using Nito.AsyncEx;

namespace Jonas.BitcoinPriceNotification.Robot
{
    class Program
    {
        static void Main(string[] arguments)
        {
            AsyncContext.Run(() => MainAsync(arguments));
        }

        static async void MainAsync(string[] arguments)
        {
            IocContainer.Initialize();
            var robot = IocContainer.Current.GetInstance<IBitcoinPriceNotificationRobot>();
            await robot.Execute(arguments);
        }
    }
}