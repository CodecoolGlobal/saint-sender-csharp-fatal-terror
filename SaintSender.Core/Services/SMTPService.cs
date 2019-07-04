using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.Core.Services
{
    public class SMTPService
    {
        CredentialService CredentialServiceObject = new CredentialService();
        public void SendEmail(MimeMessage message)
        {
            string[] credentials = CredentialServiceObject.GetSavedCredentials();
            message.From.Add(new MailboxAddress(credentials[0], credentials[0]));
            var client = new SmtpClient();

            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate(credentials[0], credentials[1]);
            client.Send(message);
            client.Disconnect(true);
        }

        

    }
}
