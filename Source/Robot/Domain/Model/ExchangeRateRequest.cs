namespace Jonas.BitcoinPriceNotification.Robot.Domain.Model
{
    public class ExchangeRateRequest
    {
        public string OrderType { get; set; }

        public string Part { get; set; }

        public decimal? Btc { get; set; }

        public decimal? Euros { get; set; }

        public string Method { get; set; } = "ideal";
    }
}