using CommandLine;

namespace Jonas.BitcoinPriceNotification.Robot.Domain.Model
{
    public class NotificationConfiguration
    {
        public static readonly NotificationConfiguration Empty = new NotificationConfiguration();

        [Option('p', "priceType", DefaultValue = PriceType.None)]
        public PriceType PriceType { get; set; }

        [Option('t', "priceThreshold", DefaultValue = 0)]
        public double PriceThreshold { get; set; }

        [Option('c', "currency", DefaultValue = Currency.None)]
        public Currency Currency { get; set; }

        [Option('e', "emailAddress", DefaultValue = null)]
        public string EmailAddress { get; set; }
    }
}