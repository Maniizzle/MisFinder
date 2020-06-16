using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisFinder.Data.Notification.Email
{
    public interface IEmailNotifier
    {
        Task<string> ReadTemplate(string template = "text");//MessageTypes messageTypes);

        Task<bool> SendEmailAsync(string to, string subject, Dictionary<string, string> message, string template = "Text");

        Task<bool> SendManyEmailAsync(List<string> to, string subject, Dictionary<string, string> messages, string template = "Text");
    }
}