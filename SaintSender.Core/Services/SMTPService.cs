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

        public static void SendEmail(MimeMessage message)
        {
            string[] credentials = GetSavedCredentials();
            var client = new SmtpClient();

            client.Connect("smtp.gmail.com", 465, true);
            client.Authenticate(credentials[0], credentials[1]);
            client.Send(message);
            client.Disconnect(true);
        }

        public static string[] GetSavedCredentials()
        {
            if (File.Exists("login_cred.txt"))
            {
                using (StreamReader streamReader = new StreamReader("login_cred.txt"))
                {
                    string[] cred = streamReader.ReadLine().Split('/');
                    return cred;
                }
            }
            else { return null; }
        }

    }
}
