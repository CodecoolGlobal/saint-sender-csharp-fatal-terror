using MimeKit;
using SaintSender.Core.Services;

namespace SaintSender.DesktopUI.ViewModels
{
    public class ComposeViewViewModel
    {
        public void ComposeMail(string content, string to, string subject)
        {
            string footer = "Sent via Saint Sender" + System.Environment.NewLine + "Isten fizesse meg!";
            
            SMTPService SMTPServiceObject = new SMTPService();

            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = subject;

            var result = System.Environment.NewLine + content + System.Environment.NewLine + System.Environment.NewLine + footer;

            var builder = new BodyBuilder();

            builder.TextBody = result;

            message.Body = builder.ToMessageBody();

            SMTPServiceObject.SendEmail(message);
        }
    }
}
