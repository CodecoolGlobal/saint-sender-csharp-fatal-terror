using MailKit;
using MailKit.Net.Imap;
using SaintSender.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.Core.Services
{
    public class IMAPService
    {
        private ImapClient _client;


        public void ConnectToIMAPService(string username, string password)
        {
            this._client = new ImapClient();

            try
            {
                _client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                _client.Connect("imap.gmail.com", 993, true);
                _client.Authenticate(username, password);
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Failed to connect to IMAP service: {e}");
            }
        }

        public async Task<List<EmailModel>> GetInboxEmailsAsync()
        {
            var emails = new List<EmailModel>();

            if (IsConnected())
            {
                var inbox = _client.Inbox;
                inbox.Open(MailKit.FolderAccess.ReadOnly);

                var emailSummaries = inbox.Fetch(0, -1, MessageSummaryItems.UniqueId | MessageSummaryItems.Size | MessageSummaryItems.Flags);

                foreach (var summary in emailSummaries)
                {
                    IList<IMessageSummary> info = inbox.Fetch(new[] { summary.UniqueId }, MessageSummaryItems.Flags | MessageSummaryItems.GMailLabels);

                    var email = await inbox.GetMessageAsync(summary.UniqueId);

                    emails.Add(
                        new EmailModel(
                            email.From.ToString(), email.Subject, email.Date.DateTime,
                            GetEmailStatus(info), email.GetTextBody(MimeKit.Text.TextFormat.Plain)));
                }
            }
            return emails;
        }

        private string GetEmailStatus(IList<IMessageSummary> info)
        {
            string status;

            if (info[0].Flags.Value.HasFlag(MessageFlags.Seen))
            {
                status = "seen";
            }
            else
            {
                status = "unseen";
            }

            return status;
        }

        public bool IsConnected()
        {
            if (_client != null)
            {
                return _client.IsAuthenticated;
            }
            else
            {
                return false;
            }
        }

        public void DisconnectFromIMAPService()
        {
            _client.Disconnect(true);
        }
    }
}
