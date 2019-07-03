using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaintSender.Core.Services;

namespace SaintSender.DesktopUI.ViewModels
{
    public class LoginViewModel
    {
        private IMAPService IMAPServiceObject = new IMAPService();
        public void Sign_In(string username, string password)
        {

            IMAPServiceObject.ConnectToIMAPService(username, password);
            IMAPServiceObject.SaveCredentials(username,password);
            MainWindow mainWindow = new MainWindow(IMAPServiceObject);
            mainWindow.Show();
        }
    }
}
