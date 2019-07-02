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
        private ObservableCollection<EmailModel> emails;
        private IMAPService iMAPServiceObject;

        public MainWindowViewModel(IMAPService iMAPServiceObject)
        {
            this.iMAPServiceObject = iMAPServiceObject;
            emails = GetEmailsAsync().Result;
        }

        public ObservableCollection<EmailModel> Emails { get => emails; set => emails = value; }

        private async Task<ObservableCollection<EmailModel>> GetEmailsAsync()
        {
            ObservableCollection<EmailModel> emailList;

            var result = await Task.Run(() => iMAPServiceObject.GetInboxEmailsAsync());
            emailList = new ObservableCollection<EmailModel>(result);
            return emailList;
        }
    }
}