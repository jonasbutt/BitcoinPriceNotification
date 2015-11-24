using FakeItEasy;
using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Helpers;
using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Services;
using Jonas.BitcoinPriceNotification.Robot.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jonas.BitcoinPriceNotification.Robot.Test
{
    [TestClass]
    public class NotificationConfigurationServiceTest
    {
        // Faked dependencies
        private ICommandLineParser commandLineParser;

        // Test subject
        private INotificationConfigurationService notificationConfigurationService;

        [TestInitialize]
        public void InitializeTest()
        {
            // Create fake dependencies
            this.commandLineParser = A.Fake<ICommandLineParser>();

            // Create test subject
            this.notificationConfigurationService = new NotificationConfigurationService(this.commandLineParser);
        }

        [TestMethod]
        public void CanTest()
        {
            // Arrange
            const string priceType = "Buy";
            const string priceThreshold = "300";
            const string currency = "Euro";
            const string emailAddress = "foo@bar.com";
            string[] arguments =
            {
                "--priceType",
                priceType,
                "--priceThreshold",
                priceThreshold,
                "--currency",
                currency,
                "--emailAddres",
                emailAddress
            };

            // Act
            // TODO JB

            // Assert
            // TODO JB
        }
    }
}