using System.Net.Mail;

namespace Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Smtp
{
    public interface ISmtpClientFactory
    {
        SmtpClient CreateSmtpClient();
    }
}