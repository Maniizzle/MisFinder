using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MisFinder.Data.Notification.Email
{
    public class Notifier : IEmailNotifier
    {
        private readonly IHostingEnvironment environment;
        private readonly INotificationService notificationService;

        public Notifier(IHostingEnvironment environment, INotificationService notificationService)
        {
            this.environment = environment;
            this.notificationService = notificationService;
        }
        public  Task<string> ReadTemplate(string template = "text")
        {
            string filepath = $@"{ environment.WebRootPath}/file";
            string htmlPath = $@"{filepath}/testmail.html";
            string contentPath = $@"{filepath}/{template.ToLower()}.txt";
            string html = string.Empty;
            string body = string.Empty;

            // get global html template
            if (File.Exists(htmlPath))
                html = File.ReadAllText(htmlPath);
            else
                return null;

            // get specific message content
            if (File.Exists(contentPath))
                 body = File.ReadAllText(contentPath);
            else return null;

            string msgBody = html.Replace("{Body}", body);
            return Task.FromResult(msgBody);
        }

        public async Task SendEmailAsync(string to, string subject, Dictionary<string, string> messages)
        {
            var msgBody = await ReadTemplate();
          var body=  msgBody.ParseTemplate(messages);
            await notificationService.SendEmail(to, subject, body);
        }

        public async Task SendManyEmailAsync(List<string> to, string subject, Dictionary<string, string> messages)
        {
            var msgBody = await ReadTemplate();
            var body = msgBody.ParseTemplate(messages);
            List<Task> job = new List<Task>();
            to.ForEach(num => job.Add(Task.Run(() =>  notificationService.SendEmail(num, subject, body))));
            await Task.WhenAll(job);
        }
    }
}
