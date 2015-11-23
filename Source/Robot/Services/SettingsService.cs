using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Services;
using System.Configuration;

namespace Jonas.BitcoinPriceNotification.Robot.Services
{
    internal class SettingsService : ISettingsService
    {
        public string GetBitonicUrl()
        {
            return ConfigurationManager.AppSettings["BitonicUri"];
        }

        public string GetSmtpServer()
        {
            return ConfigurationManager.AppSettings["SmtpServer"];
        }
    }
}