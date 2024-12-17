using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace ECommerce.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mail = "baba.omer72@hotmail.com";
            var pw = "icugcsbjtzunnfcs";
            var client = new SmtpClient("smtp-mail.outlook.com", 465)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            return client.SendMailAsync(new MailMessage(from: mail, to: email, subject, htmlMessage));
        }
    }
}
