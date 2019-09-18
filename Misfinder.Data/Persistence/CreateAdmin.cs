using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MisFinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence
{
    public class CreateAdmin
    {
        public static async Task CreateAdminAccount(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = configuration["Data:AdminUser:Name"];
            string email = configuration["Data:AdminUser:Email"];
            string password = configuration["Data:AdminUser:Password"];
            string role = configuration["Data:AdminUser:Role"];
            string role2 = configuration["Data:AdminUser:Role2"];
            if (await userManager.FindByNameAsync(username) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null && await roleManager.FindByNameAsync(role2)==null)
                {
                    
                    await roleManager.CreateAsync(new IdentityRole(role));
                    await roleManager.CreateAsync(new IdentityRole(role2));
                }
                
                ApplicationUser user = new ApplicationUser
                {
                    UserName = email,
                    Email = email
                };
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                   
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
