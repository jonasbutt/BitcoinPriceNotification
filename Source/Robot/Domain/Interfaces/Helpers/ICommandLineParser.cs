namespace Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Helpers
{
    internal interface ICommandLineParser
    {
        TParseResult Parse<TParseResult>(string[] arguments) where TParseResult : class, new();
    }
}