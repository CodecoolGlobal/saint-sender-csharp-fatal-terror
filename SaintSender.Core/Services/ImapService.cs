using MailKit.Net.Imap;
using SaintSender.Core.Entities;
using System;
using System.Collections.Generic;
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

                for (int i = 0; i < inbox.Count; i++)
                {
                    var message = await inbox.GetMessageAsync(i);
                    emails.Add(new EmailModel(message.From.ToString(), message.Subject, message.Date.DateTime, false, message.GetTextBody(MimeKit.Text.TextFormat.RichText)));
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
