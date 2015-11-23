using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces;
using System;
using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Services;

namespace Jonas.BitcoinPriceNotification.Robot.Services
{
    internal class OutputService : IOutputService
    {
        public void OutputError(string message)
        {
            OutputMessage(message, ConsoleColor.Red);
        }

        public void OutputSuccess(string message)
        {
            OutputMessage(message, ConsoleColor.Green);
        }

        public void OutputInfo(string message)
        {
            OutputMessage(message, ConsoleColor.Cyan);
        }

        private static void OutputMessage(string message, ConsoleColor color)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = originalColor;
            Console.ReadLine();
        }
    }
}