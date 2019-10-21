using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisFinder.Data.Notification.Email
{
    
    
        public static class EmailTemplate
        {
            static readonly object synclock = new object();

            public static string ParseTemplate(this string messageTemplate, Dictionary<string, string> messages)
            {
                lock (synclock)
                {
                    foreach (KeyValuePair<string, string> item in messages)
                    {
                        messageTemplate = messageTemplate.Replace(item.Key, item.Value);
                    }

                    return messageTemplate;

                }

            }
        }

    
}
