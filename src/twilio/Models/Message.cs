﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace twilio.Models
{
    public class Message
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public string Status { get; set; }
    }
}
