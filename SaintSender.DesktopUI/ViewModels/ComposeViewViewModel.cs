using MimeKit;
using SaintSender.Core.Services;

namespace SaintSender.DesktopUI.ViewModels
{
    class ComposeViewViewModel
    {
        public void ComposeMail(string content, string to, string subject)
        {
            string footer = "Sent via Saint Sender";
            string[] credentials = SMTPService.GetSavedCredentials();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(credentials[0], credentials[0]));
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = subject;

            var result = System.Environment.NewLine + content + System.Environment.NewLine + System.Environment.NewLine + footer;

            var builder = new BodyBuilder();

            builder.TextBody = result;

            message.Body = builder.ToMessageBody();

            SMTPService.SendEmail(message);
        }
    }
}
