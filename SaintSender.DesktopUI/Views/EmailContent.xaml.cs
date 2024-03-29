﻿using MahApps.Metro.Controls;
using SaintSender.Core.Entities;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class EmailContent : MetroWindow
    {
        private EmailContentViewModel _email;

        public EmailContent(EmailModel email)
        {
            InitializeComponent();

            _email = new EmailContentViewModel(email);
            this.DataContext = _email.Email;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
