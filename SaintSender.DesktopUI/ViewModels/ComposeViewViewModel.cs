using MimeKit;
using SaintSender.Core.Services;

namespace SaintSender.DesktopUI.ViewModels
{
    public class ComposeViewViewModel
    {
        public void ComposeMail(string content, string to, string subject)
        {
            SMTPService SMTPServiceObject = new SMTPService();

            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = subject;

            var builder = new BodyBuilder();

            builder.TextBody = content;

            message.Body = builder.ToMessageBody();

            SMTPServiceObject.SendEmail(message);
        }
    }
}
