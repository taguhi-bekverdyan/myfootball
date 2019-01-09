using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MyFootballMvc.Services
{
    public class EmailSender: IEmailSender
    {
        // Our private configuration variables
        private string host;
        private int port;
        private bool enableSSL;
        private string userName;
        private string password;

        // Get our parameterized configuration
        public EmailSender(string host, int port, bool enableSSL, string userName, string password)
        {
            this.host = host;
            this.port = port;
            this.enableSSL = enableSSL;
            this.userName = userName;
            this.password = password;
        }

        // Use our configuration to send the email by using SmtpClient
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = enableSSL
            };

            return client.SendMailAsync(
                new MailMessage(userName, email, subject, htmlMessage) { IsBodyHtml = true }
            );

        }

        //public Task SendEmailAsync(string email, string subject, string htmlMessage)
        //{
        //    var client = new SmtpClient("smtp.zoho.com", 465)
        //    {
        //        //Host = "smtp.zoho.com",
        //        //Port = 465,
        //        //EnableSsl = true,
        //        //DeliveryMethod = SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential("myfootball.info@soulstudio.club", "WA8r`*@`Y9N6{3x")
        //    };

        //    var mailMessage = new MailMessage
        //    {
        //        From = new MailAddress("myfootball.info@soulstudio.club")
        //    };
        //    mailMessage.To.Add("tagnvard@gmail.com");
        //    mailMessage.Subject = "Hi";
        //    mailMessage.IsBodyHtml = true;
        //    mailMessage.Body = "hi";
        //    mailMessage.Priority = MailPriority.High;

        //    return client.SendMailAsync(mailMessage);
        //}
    }
}
