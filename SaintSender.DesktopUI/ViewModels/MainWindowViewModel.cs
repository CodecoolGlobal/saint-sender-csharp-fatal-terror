﻿using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SaintSender.Core.Entities;
using SaintSender.Core.Services;
using SaintSender.DesktopUI.ViewModels;

namespace SaintSender.DesktopUI
{
    public class MainWindowViewModel
    {
        private IMAPService IMAPServiceObject;
        private CredentialService CredentialServiceObject;
        private bool refreshEmails;


        public MainWindowViewModel(IMAPService iMAPServiceObject)
        {
            this.IMAPServiceObject = iMAPServiceObject;
            CredentialServiceObject = new CredentialService();
            Emails = new ObservableCollection<EmailModel>();
            refreshEmails = true;
            Task.Run(() => GetEmailsAsync());
        }

        public ObservableCollection<EmailModel> Emails { get; }

        private async Task GetEmailsAsync()
        {
            while (refreshEmails)
            {
                var result = await Task.Run(() => IMAPServiceObject.GetInboxEmailsAsync());
                foreach (var item in from email in result
                                     where Emails.Contains(email,new EmailComparer()) == false
                                     select email)
                {
                    Application.Current.Dispatcher.Invoke(() => Emails.Add(item));
                }
                await Task.Delay(5000);
            }
        }
        public void Logout()
        {
            refreshEmails = false;
            CredentialServiceObject.DeleteCredentials();
        }
    }
}