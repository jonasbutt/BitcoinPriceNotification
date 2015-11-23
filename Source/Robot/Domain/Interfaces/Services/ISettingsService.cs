namespace Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Services
{
    public interface ISettingsService
    {
        string GetBitonicUrl();

        string GetSmtpServer();
    }
}