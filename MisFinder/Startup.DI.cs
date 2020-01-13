using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MisFinder.CustomIdentity;
using MisFinder.Data.Notification;
using MisFinder.Data.Notification.Email;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Data.Persistence.Repositories;
using MisFinder.Domain.Models;
using MisFinder.Utility;

namespace MisFinder
{
    public partial class Startup
    {
        public static void ConfigureDI(IServiceCollection services)
        {
            services.AddTransient<IUserClaimsPrincipalFactory<ApplicationUser>, MisfinderUserClaimsPrincipalFactory>();
            services.AddTransient<IFoundItemRepository, FoundItemRepository>();
            services.AddTransient<ILostItemRepository, LostItemRepository>();
            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<ILocalGovernmentRepository, LocalGovernmentRepository>();
            services.AddTransient<ILostItemClaimRepository, LostItemClaimRepository>();
            services.AddTransient<IFoundItemClaimRepository, FoundItemClaimRepository>();
            services.AddTransient<IUtility, UtilityService>();
            services.AddTransient<IEmailNotifier, Notifier>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IMeetingRepository, MeetingRepository>();
            var config = new EmailConfiguration();
            Configuration.Bind("EmailData", config);
            services.AddSingleton(config);
        }
    }
}