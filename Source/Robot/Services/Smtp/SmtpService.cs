﻿using Jonas.BitcoinPriceNotification.Robot.Domain.Interfaces.Smtp;
using System.Net.Mail;

namespace Jonas.BitcoinPriceNotification.Robot.Services.Smtp
{
    internal class SmtpService : ISmtpService
    {
        private readonly ISmtpClientFactory smtpClientFactory;

        public SmtpService(ISmtpClientFactory smtpClientFactory)
        {
            this.smtpClientFactory = smtpClientFactory;
        }

        public void SendEmail(string recipient, string subject)
        {
            var mailMessage = new MailMessage
            {
                Subject = subject,
                From = new MailAddress("noreply@notification")
            };
            mailMessage.To.Add(recipient);
            var smtpClient = this.smtpClientFactory.CreateSmtpClient();
            smtpClient.Send(mailMessage);
        }
    }
}