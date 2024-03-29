﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
            lstvEmails.AddHandler(ListViewItem.MouseDoubleClickEvent, new MouseButtonEventHandler(listView_MouseDoubleClick));
        }

        private void ComposeBtn_Click(object sender, RoutedEventArgs e)
        {
            ComposeView composeView = new ComposeView();
            composeView.Show();
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.Logout();
            LoginView loginView = new LoginView();
            loginView.Show();
            this.Close();
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
                
            var col1 = 0.35;
            var col2 = 0.35;
            var col3 = 0.20;
            var col4 = 0.10;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
            gView.Columns[2].Width = workingWidth * col3;
            gView.Columns[3].Width = workingWidth * col4;
        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // navigate to the list view item 
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            ListViewItem item = (ListViewItem)dep;
            EmailModel myDataObject = (EmailModel)item.Content;

            Dispatcher.BeginInvoke(new Action(() => {
                var emailContentWindow = new EmailContent(myDataObject);
                emailContentWindow.Show();
            }));
        }
    }
}
