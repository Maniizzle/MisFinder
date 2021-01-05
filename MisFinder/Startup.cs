using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MisFinder.CustomIdentity;
using MisFinder.Data.Data.Context;
using MisFinder.Data.Persistence;
using MisFinder.Domain.Models;
using System;///////////////////////////////////////////////////////////////////////////////////////////

namespace MisFinder
{
    public partial class Startup
    {
        public static IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(configg =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();

                configg.Filters.Add(new AuthorizeFilter(policy));
            });

            services.Configure<CookiePolicyOptions>(o =>
            {
                o.CheckConsentNeeded = context => true;
                o.MinimumSameSitePolicy = SameSiteMode.None;
            });
            // services.AddDbContext<MisFinderDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("MisFinder")));

            string envVar = Environment.GetEnvironmentVariable("DATABASE_URL");
            string connectionString = null;

            if (string.IsNullOrEmpty(envVar))
            {
                connectionString = Configuration["ConnectionStrings:database"];
            }
            else
            {
                //parse database URL. Format is postgres://<username>:<password>@<host>/<dbname>
                var uri = new Uri(envVar);
                var ddd = uri.ToString().Split('@');
                var host = ddd[1].Split(':')[0];
                var username = uri.UserInfo.Split(':')[0];
                var password = uri.UserInfo.Split(':')[1];
                connectionString = "Server=" + host + "; Database=" + uri.AbsolutePath.Substring(1) + "; Username=" + username +
                "; Password=" + password + "; Port=" + uri.Port +
                "; SSL Mode=Require; Trust Server Certificate=true;";
            }
            services.AddDbContext<MisFinderDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            //services.AddDbContext<MisFinderDbContext>(opt => opt.UseSqlite(Configuration.GetConnectionString("Default"))).AddEntityFrameworkSqlite();
            //services.AddHangfire(
            // x => x.UseSqlServerStorage(Configuration.GetConnectionString("MisFinder"))
            //.. );
            services.Configure<CookieTempDataProviderOptions>(opts =>
            {
                opts.Cookie.IsEssential = true;
            });

            ConfigureDI(services);

            services.AddIdentity<ApplicationUser, IdentityRole>(opts =>
            {
                opts.Lockout.AllowedForNewUsers = true;
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                opts.Lockout.MaxFailedAccessAttempts = 5;
                opts.SignIn.RequireConfirmedEmail = true;
                opts.Tokens.EmailConfirmationTokenProvider = "emailconf";
            }
            ).AddDefaultTokenProviders()
            .AddEntityFrameworkStores<MisFinderDbContext>()
                .AddTokenProvider<EmailConfirmationTokenProvider<ApplicationUser>>("emailconf");//registering a new tokenprovideroption

            //services.AddAuthentication().AddGoogle(o =>
            //{
            //    o.ClientId = Configuration["Authentication:Google:ClientId"];
            //    o.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            //});
            services.AddAuthentication();
            //.AddGoogle(options =>
            //{
            //    IConfigurationSection googleAuthNSection =
            //        Configuration.GetSection("Authentication:Google");

            //    options.ClientId = googleAuthNSection["ClientId"];
            //    options.ClientSecret = googleAuthNSection["ClientSecret"];
            //});

            services.AddMvc(configg =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();

                configg.Filters.Add(new AuthorizeFilter(policy));
            });
            //.SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
            ////  config.Filters.Add(new AuthorizeFilter(policy));//.SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromHours(3));
            //configuring the token sent for email confirmation to be valid for a day
            services.Configure<EmailConfirmationTokenProviderOptions>(opts =>
            opts.TokenLifespan = TimeSpan.FromDays(1));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCookiePolicy();
            //app.UseHangfireDashboard();
            // app.UseHangfireServer();
            app.UseStaticFiles();

            //app.UseSession();
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "areas",
                     pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //      name: "areas",
            //      template: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

            //    routes.MapRoute(
            //       name: "default",
            //       template: "{controller=Home}/{action=Index}/{id?}");
            //}
            //);

            CreateAdmin.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
        }
    }
}