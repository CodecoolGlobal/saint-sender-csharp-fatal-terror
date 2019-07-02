using MailKit.Net.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.Core.Services
{
    public class AuthenticationService
    {
        private ImapClient _client;

        public AuthenticationService()
        {
            this._client = new ImapClient();
        }

        public void ConnectToIMAPService(string username, string password)
        {
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

        public bool IsConnected()
        {
            return _client.IsAuthenticated;
        }

        public void DisconnectFromGmailService()
        {
            _client.Disconnect(true);
        }
    }
}
