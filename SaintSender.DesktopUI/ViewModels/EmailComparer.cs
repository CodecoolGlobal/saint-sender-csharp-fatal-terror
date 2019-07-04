using SaintSender.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.DesktopUI.ViewModels
{
    public class EmailComparer : IEqualityComparer<EmailModel>
    {
        public bool Equals(EmailModel x, EmailModel y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;
            else if (x.Date == y.Date && x.Message == y.Message
                                && x.Sender == y.Sender && x.Subject == y.Subject)
                return true;
            else
                return false;
        }
        public int GetHashCode(EmailModel obj)
        {
            return obj.Date.GetHashCode() * 17 + obj.Message.GetHashCode() * 17 + obj.Sender.GetHashCode() * 17 + obj.Subject.GetHashCode() * 17;
        }
    }

}
