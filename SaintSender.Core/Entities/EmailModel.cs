﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.Core.Entities
{
    public class EmailModel
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public Boolean Read { get; set; }
        public string Subject { get; set; }
    }
}