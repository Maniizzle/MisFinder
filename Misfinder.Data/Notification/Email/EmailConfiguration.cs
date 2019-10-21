using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisFinder.Data.Notification.Email
{
    public class EmailConfiguration
    {
        
            public string Host { get; set; }
            public string SenderAddress { get; set; }
                 public int Port { get; set; }

            public string Password { get; set; }
            public string SenderName { get; set; }
            public bool EnableSSL { get; set; }
            //public string AdminSenderEmail { get; set; }
        
    }
}
