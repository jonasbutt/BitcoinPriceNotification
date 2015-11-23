using FakeItEasy;
using FakeItEasy.ExtensionSyntax.Full;
using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Services;
using Jonas.BitcoinPriceNotification.Robot.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Jonas.BitcoinPriceNotification.Robot.Test
{
    [TestClass]
    public class BitcoinPriceNotificationRobotTest
    {
        // Faked dependencies
        private INotificationConfigurationService notificationConfigurationService;
        private IBitcoinExchangeRatesService bitcoinExchangeRatesService;
        private ISmtpService smtpService;
        private IOutputService outputService;

        // Test subject
        private IBitcoinPriceNotificationRobot bitcoinPriceNotificationRobot;

        [TestInitialize]
        public void InitializeTest()
        {
            // Create fake dependencies
            this.notificationConfigurationService = A.Fake<INotificationConfigurationService>();
            this.bitcoinExchangeRatesService = A.Fake<IBitcoinExchangeRatesService>();
            this.smtpService = A.Fake<ISmtpService>();
            this.outputService = A.Fake<IOutputService>();

            // Create test subject
            this.bitcoinPriceNotificationRobot = new BitcoinPriceNotificationRobot(
                this.notificationConfigurationService,
                this.bitcoinExchangeRatesService,
                this.smtpService,
                this.outputService);
        }

        [TestMethod]
        public async Task CanOutputErrorForInvalidNotificationConfiguration()
        {
            // Arrange
            string[] arguments = {};
            A.CallTo(() => this.notificationConfigurationService.CreateFromArguments(arguments)).Returns(NotificationConfiguration.Empty);

            // Act
            await bitcoinPriceNotificationRobot.Execute(arguments);

            // Assert
            A.CallTo(() => this.outputService.OutputError(A<string>.Ignored)).MustHaveHappened();
            this.bitcoinExchangeRatesService.AnyCall().MustNotHaveHappened();
            this.smtpService.AnyCall().MustNotHaveHappened();
        }

        [TestMethod]
        public async Task CanOutputInfoMessageWhenNoEuroBuyNotificationWasSent()
        {
            // Arrange
            const PriceType priceType = PriceType.Buy;
            const double priceThreshold = 300;
            const Currency currency = Currency.Euro;
            const string emailAddress = "foo@bar.com";
            const decimal rate = 400;
            string[] arguments = { };
            A.CallTo(() => this.notificationConfigurationService.CreateFromArguments(arguments)).Returns(
                new NotificationConfiguration
                {
                    PriceType = priceType,
                    PriceThreshold = priceThreshold,
                    Currency = currency,
                    EmailAddress = emailAddress
                });

            A.CallTo(() => this.bitcoinExchangeRatesService.RetrieveBuyRateInEuro()).Returns(Task.FromResult(rate));

            // Act
            await bitcoinPriceNotificationRobot.Execute(arguments);

            // Assert
            A.CallTo(() => this.outputService.OutputInfo(A<string>.Ignored)).MustHaveHappened();
            A.CallTo(() => this.bitcoinExchangeRatesService.RetrieveBuyRateInEuro()).MustHaveHappened();
            this.smtpService.AnyCall().MustNotHaveHappened();
        }

        [TestMethod]
        public async Task CanOutputInfoMessageWhenNoBtcBuyNotificationWasSent()
        {
            // Arrange
            const PriceType priceType = PriceType.Buy;
            const double priceThreshold = 300;
            const Currency currency = Currency.Bitcoin;
            const string emailAddress = "foo@bar.com";
            const decimal rate = 400;
            string[] arguments = { };
            A.CallTo(() => this.notificationConfigurationService.CreateFromArguments(arguments)).Returns(
                new NotificationConfiguration
                {
                    PriceType = priceType,
                    PriceThreshold = priceThreshold,
                    Currency = currency,
                    EmailAddress = emailAddress
                });

            A.CallTo(() => this.bitcoinExchangeRatesService.RetrieveBuyRateInBtc()).Returns(Task.FromResult(rate));

            // Act
            await bitcoinPriceNotificationRobot.Execute(arguments);

            // Assert
            A.CallTo(() => this.outputService.OutputInfo(A<string>.Ignored)).MustHaveHappened();
            A.CallTo(() => this.bitcoinExchangeRatesService.RetrieveBuyRateInBtc()).MustHaveHappened();
            this.smtpService.AnyCall().MustNotHaveHappened();
        }

        [TestMethod]
        public async Task CanOutputInfoMessageWhenNoEuroSellNotificationWasSent()
        {
            // Arrange
            const PriceType priceType = PriceType.Sell;
            const double priceThreshold = 300;
            const Currency currency = Currency.Euro;
            const string emailAddress = "foo@bar.com";
            const decimal rate = 200;
            string[] arguments = { };
            A.CallTo(() => this.notificationConfigurationService.CreateFromArguments(arguments)).Returns(
                new NotificationConfiguration
                {
                    PriceType = priceType,
                    PriceThreshold = priceThreshold,
                    Currency = currency,
                    EmailAddress = emailAddress
                });

            A.CallTo(() => this.bitcoinExchangeRatesService.RetrieveSellRateInEuro()).Returns(Task.FromResult(rate));

            // Act
            await bitcoinPriceNotificationRobot.Execute(arguments);

            // Assert
            A.CallTo(() => this.outputService.OutputInfo(A<string>.Ignored)).MustHaveHappened();
            A.CallTo(() => this.bitcoinExchangeRatesService.RetrieveSellRateInEuro()).MustHaveHappened();
            this.smtpService.AnyCall().MustNotHaveHappened();
        }

        [TestMethod]
        public async Task CanOutputInfoMessageWhenNoBtcSellNotificationWasSent()
        {
            // Arrange
            const PriceType priceType = PriceType.Sell;
            const double priceThreshold = 300;
            const Currency currency = Currency.Bitcoin;
            const string emailAddress = "foo@bar.com";
            const decimal rate = 200;
            string[] arguments = { };
            A.CallTo(() => this.notificationConfigurationService.CreateFromArguments(arguments)).Returns(
                new NotificationConfiguration
                {
                    PriceType = priceType,
                    PriceThreshold = priceThreshold,
                    Currency = currency,
                    EmailAddress = emailAddress
                });

            A.CallTo(() => this.bitcoinExchangeRatesService.RetrieveSellRateInBtc()).Returns(Task.FromResult(rate));

            // Act
            await bitcoinPriceNotificationRobot.Execute(arguments);

            // Assert
            A.CallTo(() => this.outputService.OutputInfo(A<string>.Ignored)).MustHaveHappened();
            A.CallTo(() => this.bitcoinExchangeRatesService.RetrieveSellRateInBtc()).MustHaveHappened();
            this.smtpService.AnyCall().MustNotHaveHappened();
        }

        [TestMethod]
        public async Task CanSendBuyNotification()
        {
            // Arrange
            const PriceType priceType = PriceType.Buy;
            const double priceThreshold = 300;
            const Currency currency = Currency.Euro;
            const string emailAddress = "foo@bar.com";
            const decimal rate = 200;
            string[] arguments = { };
            A.CallTo(() => this.notificationConfigurationService.CreateFromArguments(arguments)).Returns(
                new NotificationConfiguration
                {
                    PriceType = priceType,
                    PriceThreshold = priceThreshold,
                    Currency = currency,
                    EmailAddress = emailAddress
                });

            A.CallTo(() => this.bitcoinExchangeRatesService.RetrieveBuyRateInEuro()).Returns(Task.FromResult(rate));

            // Act
            await bitcoinPriceNotificationRobot.Execute(arguments);

            // Assert
            A.CallTo(() => this.outputService.OutputSuccess(A<string>.Ignored)).MustHaveHappened();
            A.CallTo(() => this.bitcoinExchangeRatesService.RetrieveBuyRateInEuro()).MustHaveHappened();
            A.CallTo(() => this.smtpService.SendEmail(emailAddress, A<string>.That.Matches(s => s.StartsWith("Buy now!")))).MustHaveHappened();
        }

        [TestMethod]
        public async Task CanSendSellNotification()
        {
            // Arrange
            const PriceType priceType = PriceType.Sell;
            const double priceThreshold = 300;
            const Currency currency = Currency.Euro;
            const string emailAddress = "foo@bar.com";
            const decimal rate = 500;
            string[] arguments = { };
            A.CallTo(() => this.notificationConfigurationService.CreateFromArguments(arguments)).Returns(
                new NotificationConfiguration
                {
                    PriceType = priceType,
                    PriceThreshold = priceThreshold,
                    Currency = currency,
                    EmailAddress = emailAddress
                });

            A.CallTo(() => this.bitcoinExchangeRatesService.RetrieveSellRateInEuro()).Returns(Task.FromResult(rate));

            // Act
            await bitcoinPriceNotificationRobot.Execute(arguments);

            // Assert
            A.CallTo(() => this.outputService.OutputSuccess(A<string>.Ignored)).MustHaveHappened();
            A.CallTo(() => this.bitcoinExchangeRatesService.RetrieveSellRateInEuro()).MustHaveHappened();
            A.CallTo(() => this.smtpService.SendEmail(emailAddress, A<string>.That.Matches(s => s.StartsWith("Sell now!")))).MustHaveHappened();
        }
    }
}