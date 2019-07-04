using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SaintSender.Core.Entities;
using SaintSender.Core.Services;

namespace SaintSender.DesktopUI
{
    public class MainWindowViewModel
    {
        private IMAPService IMAPServiceObject;
        private CredentialService CredentialServiceObject;


        public MainWindowViewModel(IMAPService iMAPServiceObject)
        {
            this.IMAPServiceObject = iMAPServiceObject;
            CredentialServiceObject = new CredentialService();
            Emails = Task.Run(() => GetEmailsAsync()).Result;
        }

        public ObservableCollection<EmailModel> Emails { get; set; }

        private async Task<ObservableCollection<EmailModel>> GetEmailsAsync()
        {
            ObservableCollection<EmailModel> emailList;

            var result = await Task.Run(() => IMAPServiceObject.GetInboxEmailsAsync());
            emailList = new ObservableCollection<EmailModel>(result);
            return emailList;
        }

        public void Logout()
        {
            CredentialServiceObject.DeleteCredentials();
        }
    }
}