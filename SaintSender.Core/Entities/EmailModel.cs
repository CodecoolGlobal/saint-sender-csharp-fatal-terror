using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.Core.Entities
{
    public class EmailModel
    {
        public EmailModel(string sender, string subject, DateTime date, string read, string message)
        {
            Sender = sender;
            Message = message;
            Date = date;
            Read = read;
            Subject = subject;
        }

        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string Read { get; set; }
        public string Subject { get; set; }
    }
}
