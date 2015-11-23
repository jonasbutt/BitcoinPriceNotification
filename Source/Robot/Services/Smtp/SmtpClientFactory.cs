using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces;
using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Smtp;
using System.Net.Mail;

namespace Jonas.BitcoinPriceNotification.Robot.Services.Smtp
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