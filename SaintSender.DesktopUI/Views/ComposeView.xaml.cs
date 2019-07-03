using MahApps.Metro.Controls;
using SaintSender.Core.Entities;
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
using SaintSender.Core.Services;
using SaintSender.DesktopUI.ViewModels;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ComposeView : MetroWindow
    {

        public ComposeView()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            ComposeViewViewModel composeView = new ComposeViewViewModel();
            composeView.ComposeMail(txtboxMessage.Text,txtboxAddress.Text,txtboxSubject.Text);

            Close();
        }
    }
}
