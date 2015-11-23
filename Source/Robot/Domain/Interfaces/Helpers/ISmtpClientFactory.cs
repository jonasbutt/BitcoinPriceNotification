using System.Net.Mail;

namespace Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Helpers
{
    public interface ISmtpClientFactory
    {
        SmtpClient CreateSmtpClient();
    }
}