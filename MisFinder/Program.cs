using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MisFinder.Data.Data.Context;

namespace MisFinder
{
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    var host = CreateWebHostBuilder(args);
        //    using (var scope = host.Services.CreateScope())
        //    using (var context = scope.ServiceProvider.GetService<AppDbContext>())
        //    {
        //        context.Database.EnsureCreated();
        //    }
        //    CreateWebHostBuilder(args).Build().Run();
        //}

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>()
        //    .UseDefaultServiceProvider(options=>options.ValidateScopes=false);

        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            using (var context = scope.ServiceProvider.GetService<MisFinderDbContext>())
            {
                context.Database.EnsureCreated();
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().UseDefaultServiceProvider(options => options.ValidateScopes = false).Build();
    }
}