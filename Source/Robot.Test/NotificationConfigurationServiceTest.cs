using FakeItEasy;
using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Helpers;
using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Services;
using Jonas.BitcoinPriceNotification.Robot.Domain.Model;
using Jonas.BitcoinPriceNotification.Robot.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

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
        public void CanCreateFromArguments()
        {
            // Arrange
            const PriceType priceType = PriceType.Buy;
            const double priceThreshold = 200;
            const Currency currency = Currency.Bitcoin;
            const string emailAddress = "foo@bar.com";
            string[] arguments = {};

            A.CallTo(() => this.commandLineParser.Parse<NotificationConfiguration>(arguments)).Returns(
                new NotificationConfiguration
                {
                    PriceType = priceType,
                    PriceThreshold = priceThreshold,
                    Currency = currency,
                    EmailAddress = emailAddress
                });

            // Act
            var notification = this.notificationConfigurationService.CreateFromArguments(arguments);

            // Assert
            notification.ShouldNotEqual(NotificationConfiguration.Empty);
            notification.PriceType.ShouldEqual(priceType);
            notification.PriceThreshold.ShouldEqual(priceThreshold);
            notification.Currency.ShouldEqual(currency);
            notification.EmailAddress.ShouldEqual(emailAddress);
        }

        [TestMethod]
        public void CanReturnEmptyNotificationConfigurationForInvalidArguments()
        {
            // Arrange
            const PriceType priceType = PriceType.None; // Invalid value.
            const double priceThreshold = 200;
            const Currency currency = Currency.Bitcoin;
            const string emailAddress = "foo@bar.com";
            string[] arguments = { };

            A.CallTo(() => this.commandLineParser.Parse<NotificationConfiguration>(arguments)).Returns(
                new NotificationConfiguration
                {
                    PriceType = priceType,
                    PriceThreshold = priceThreshold,
                    Currency = currency,
                    EmailAddress = emailAddress
                });

            // Act
            var notification = this.notificationConfigurationService.CreateFromArguments(arguments);

            // Assert
            notification.ShouldEqual(NotificationConfiguration.Empty);
        }
    }
}