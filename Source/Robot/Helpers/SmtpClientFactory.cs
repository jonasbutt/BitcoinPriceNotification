using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Helpers;
using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Services;
using System.Net.Mail;

namespace Jonas.BitcoinPriceNotification.Robot.Helpers
{
    internal class SmtpClientFactory : ISmtpClientFactory
    {
        private readonly ISettingsService settingsService;

        public SmtpClientFactory(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public SmtpClient CreateSmtpClient()
        {
            return new SmtpClient(this.settingsService.GetSmtpServer());
        }
    }
}