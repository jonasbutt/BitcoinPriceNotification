namespace Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces
{
    public interface ISettingsService
    {
        string GetBitonicUrl();

        string GetSmtpServer();
    }
}