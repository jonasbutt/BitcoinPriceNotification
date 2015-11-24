namespace Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Helpers
{
    public interface ICommandLineParser
    {
        TParseResult Parse<TParseResult>(string[] arguments) where TParseResult : class, new();
    }
}