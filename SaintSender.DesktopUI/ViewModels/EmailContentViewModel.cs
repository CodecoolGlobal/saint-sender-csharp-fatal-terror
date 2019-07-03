using SaintSender.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.DesktopUI.ViewModels
{
    class EmailContentViewModel
    {
        public EmailModel Email{get;set;}

        public EmailContentViewModel(EmailModel ObjectEmail)
        {
            Email = ObjectEmail;
        }
    }
}
