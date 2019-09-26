using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MisFinder.Domain.Models;
using MisFinder.Data.Persistence;
using MisFinder.Data.Data.Context;
using MisFinder.Data.Persistence.Repositories;
using MisFinder.Data.Persistence.IRepositories;

namespace MisFinder
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }   
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MisFinderDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("MisFinder")));
            services.AddTransient<IFoundItemRepository, FoundItemRepository>();
            services.AddTransient<ILostItemRepository, LostItemRepository>();
            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<ILocalGovernmentRepository, LocalGovernmentRepository>();
            services.AddIdentity<ApplicationUser, IdentityRole>(opts =>
            {   
                opts.Lockout.AllowedForNewUsers = true;
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                opts.Lockout.MaxFailedAccessAttempts = 5;
                opts.SignIn.RequireConfirmedEmail = true;
                opts.Tokens.EmailConfirmationTokenProvider = "emailconf";
            }
            ).
                AddEntityFrameworkStores<MisFinderDbContext>()
                .AddDefaultTokenProviders() 
                .AddTokenProvider<EmailConfirmationTokenProvider<ApplicationUser>>("emailconf");//registering a new tokenprovideroption
            services.AddMvc();//.SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromHours(3));
            //configuring the token sent for email confirmation to be valid for a day
            services.Configure<EmailConfirmationTokenProviderOptions>(opts =>
            opts.TokenLifespan = TimeSpan.FromDays(1)); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //   name: "default",
                //   template: "{controller=Home}/{action=Index}/{id?}");
                //routes.MapRoute("areaRoute", "{area=exists}/{controller=Dashboard}/{action=index}/{id?}");
                
                
                    routes.MapRoute(
                      name: "areas",
                      template: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                    routes.MapRoute(
                       name: "default",
                       template: "{controller=Home}/{action=Index}/{id?}");
                
                //routes.MapRoute(
                // name: "default",
                // template: "{controller=Home}/{action=Index}/{id?}");

                //routes.MapAreaRoute(
                //    name: "default",
                //    areaName: "Admin",
                //    template: "{controller=Home}/{action=Index}/{id?}");

            }
            );


            CreateAdmin.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();

        }
    }
}
