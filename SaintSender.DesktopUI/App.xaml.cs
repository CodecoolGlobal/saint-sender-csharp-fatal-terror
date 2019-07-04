using SaintSender.Core.Services;
using SaintSender.DesktopUI.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SaintSender.DesktopUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private CredentialService CredentialServiceObject = new CredentialService();
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string[] cred = CredentialServiceObject.GetSavedCredentials();
            if (cred == null || cred.Length!=2)
            {
                CreateLoginForm();
            }
            else
            {
                try
                {
                    IMAPService IMAPServiceObject = AttemptLogin(cred);
                    CreateMainWindow(IMAPServiceObject);
                }
                catch (ArgumentException)
                {
                    CreateLoginForm();
                }
            }
        }

        private void CreateMainWindow(IMAPService IMAPServiceObject)
        {
            MainWindow mainWindow = new MainWindow(IMAPServiceObject);
            mainWindow.Show();
        }

        private  void CreateLoginForm()
        {
            LoginView loginView = new LoginView();
            loginView.Show();
        }

        private IMAPService AttemptLogin(string[] cred)
        {
            string username = cred[0];
            string password = cred[1];
            IMAPService IMAPServiceObject = new IMAPService();
            IMAPServiceObject.ConnectToIMAPService(username, password);
            return IMAPServiceObject;
        }

       
    }
}
