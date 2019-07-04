using MahApps.Metro.Controls;
using SaintSender.DesktopUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : MetroWindow
    {
        LoginViewModel loginViewModel = new LoginViewModel();
        public LoginView()
        {
            InitializeComponent();
        }
        public void Btn_Sign_in(object sender, RoutedEventArgs routedEventArgs)
        {
            try
            {
                loginViewModel.Sign_In(txtUsername.Text, txtPassword.Text);
                this.Close();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Login failed");
            }

        }

    }
}
