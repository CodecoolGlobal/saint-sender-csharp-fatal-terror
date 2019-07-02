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
        IMAPService authenticationService = new IMAPService();
        public void Sign_In(string username, string password)
        {
            authenticationService.ConnectToIMAPService(username, password);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
