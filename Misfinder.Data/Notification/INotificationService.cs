using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisFinder.Data.Notification 
{
    public interface INotificationService
    {
        Task SendEmail(string receiver, string subject, string body);
    }
}
