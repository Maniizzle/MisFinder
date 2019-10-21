using Microsoft.Extensions.Logging;
using MisFinder.Data.Notification.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MisFinder.Data.Notification 
{
    public class NotificationService:INotificationService
    {
        private readonly ILogger logger;

        public NotificationService(EmailConfiguration configuration,ILogger logger)
        {
            Configuration = configuration;
            this.logger = logger;
        }

        public EmailConfiguration Configuration { get; }

        public async Task SendEmail(string receiver, string subject, string body)
        {
           await  Task.Factory.StartNew(() => 
            {
                MailAddress from = new MailAddress(Configuration.SenderAddress, Configuration.SenderName);
                MailAddress mailto = new MailAddress(receiver);
                MailMessage message = new MailMessage(from, mailto);

                try{
                    using (SmtpClient server = new SmtpClient(Configuration.Host, Configuration.Port))
                    {

                        server.Credentials = new NetworkCredential(Configuration.SenderAddress, Configuration.Password);
                        server.EnableSsl = Configuration.EnableSSL;

                        message.Subject = subject;
                        message.Body = body;
                        message.IsBodyHtml = true;



                        server.Send(message);
                    }
                }
                catch(Exception ex)
                {
                   logger.LogError( ex.Message);
                }
            
            });
        }
    }
}
