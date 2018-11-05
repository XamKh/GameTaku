using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Services
{
    public class EmailSender : IEmailSender
    {
        private SendGridClient sendGridClient;

        public EmailSender(SendGridClient sendGridClient)
        {
            this.sendGridClient = sendGridClient;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SendGrid.Helpers.Mail.SendGridMessage message = new SendGrid.Helpers.Mail.SendGridMessage
            {
                From = new SendGrid.Helpers.Mail.EmailAddress("chopchop6677@gmail.com", "GameTaku Store Admin"),
                Subject = subject,
                HtmlContent = htmlMessage,
                PlainTextContent = htmlMessage
            };

            message.TemplateId = "d-7fde3e373b154a56b251cfe4ad572741";
            message.SetTemplateData(new
            {

            });

            message.AddTo(email);
            return this.sendGridClient.SendEmailAsync(message); 
        }
    }
}
