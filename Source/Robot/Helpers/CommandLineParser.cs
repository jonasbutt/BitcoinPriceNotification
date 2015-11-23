using CommandLine;
using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Helpers;

namespace Jonas.BitcoinPriceNotification.Robot.Helpers
{
    internal class CommandLineParser : ICommandLineParser
    {
        public TParseResult Parse<TParseResult>(string[] arguments) where TParseResult : class, new()
        {
            var parseResult = new TParseResult();
            Parser.Default.ParseArguments(arguments, parseResult);
            return parseResult;
        }
    }
}