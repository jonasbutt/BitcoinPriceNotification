﻿namespace Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces
{
    public interface IOutputService
    {
        void OutputError(string message);

        void OutputSuccess(string message);

        void OutputInfo(string message);
    }
}