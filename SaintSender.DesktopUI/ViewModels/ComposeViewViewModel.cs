using MimeKit;
using SaintSender.Core.Services;

namespace SaintSender.DesktopUI.ViewModels
{
    class ComposeViewViewModel
    {
        public void ComposeMail(string content, string to, string subject)
        {
            string[] credentials = SMTPService.GetSavedCredentials();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(credentials[0], credentials[0]));
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = subject;

            var builder = new BodyBuilder();

            builder.TextBody = content;

            message.Body = builder.ToMessageBody();

            SMTPService.SendEmail(message);
        }
    }
}
