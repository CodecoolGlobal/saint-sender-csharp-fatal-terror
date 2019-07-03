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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using SaintSender.Core.Entities;
using SaintSender.Core.Services;
using SaintSender.DesktopUI.Views;

namespace SaintSender.DesktopUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private MainWindowViewModel mainWindowViewModel;
        public MainWindow(IMAPService IMAPServiceObject)
        {
            InitializeComponent();
            mainWindowViewModel = new MainWindowViewModel(IMAPServiceObject);
            lstvEmails.ItemsSource = mainWindowViewModel.Emails;
        }

        private void ComposeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            Resize_ListView(listView, gView);
        }

        private void Resize_ListView(ListView listView, GridView gView)
        {
            double workingWidth = BigGrid.ActualWidth;
                
            var col1 = 0.50;
            var col2 = 0.20;
            var col3 = 0.15;
            var col4 = 0.15;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
            gView.Columns[2].Width = workingWidth * col3;
            gView.Columns[3].Width = workingWidth * col4;
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => {
                var emailContentWindow = new EmailContent();
                emailContentWindow.Show();
            }));
        }
    }
}
