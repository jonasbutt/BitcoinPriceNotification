namespace Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Services
{
    public interface ISmtpService
    {
        void SendEmail(string recipient, string subject);
    }
}