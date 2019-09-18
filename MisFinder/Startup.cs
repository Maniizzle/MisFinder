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
            routes.MapRoute(
                "default", "{controller=Home}/{action=Index}/{id?}"
                ));
            CreateAdmin.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();

        }
    }
}
