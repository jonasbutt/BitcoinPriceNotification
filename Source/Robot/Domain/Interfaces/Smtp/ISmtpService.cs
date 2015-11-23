namespace Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Smtp
{
    public interface ISmtpService
    {
        void SendEmail(string recipient, string subject);
    }
}