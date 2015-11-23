using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces;
using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Helpers;
using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Services;
using Jonas.BitcoinPriceNotification.Robot.Helpers;
using Jonas.BitcoinPriceNotification.Robot.Services;
using SimpleInjector;

namespace Jonas.BitcoinPriceNotification.Robot
{
    public static class IocContainer
    {
        public static Container Current { get; private set; }

        public static void Initialize()
        {
            var container = new Container();
            RegisterServices(container);
            #if DEBUG
            container.Verify();
            #endif
            Current = container;
        }

        private static void RegisterServices(Container container)
        {
            container.Register<IBitcoinPriceNotificationRobot, BitcoinPriceNotificationRobot>();
            container.Register<IBitcoinExchangeRatesService, BitcoinExchangeRatesService>();
            container.Register<INotificationConfigurationService, NotificationConfigurationService>();
            container.Register<IOutputService, OutputService>();
            container.Register<ISettingsService, SettingsService>();
            container.Register<IHttpService, HttpService>();
            container.Register<IHttpClientFactory, HttpClientFactory>();
            container.Register<ISerializationHelper, SerializationHelper>();
            container.Register<ISmtpService, SmtpService>();
            container.Register<ISmtpClientFactory, SmtpClientFactory>();
        }
    }
}