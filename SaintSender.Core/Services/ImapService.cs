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

                var items = inbox.Fetch(0, -1, MessageSummaryItems.UniqueId | MessageSummaryItems.Size | MessageSummaryItems.Flags);

                foreach (var item in items)
                {
                    IList<IMessageSummary> info = inbox.Fetch(new[] { item.UniqueId }, MessageSummaryItems.Flags | MessageSummaryItems.GMailLabels);

                    string status;

                    if (info[0].Flags.Value.HasFlag(MessageFlags.Seen))
                    {
                        status = "seen";
                    }
                    else
                    {
                        status = "unseen";
                    }


                    var message = await inbox.GetMessageAsync(item.UniqueId);
                    emails.Add(new EmailModel(message.From.ToString(), message.Subject, message.Date.DateTime, status, message.GetTextBody(MimeKit.Text.TextFormat.Plain)));
                }
            }

            return emails;
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
