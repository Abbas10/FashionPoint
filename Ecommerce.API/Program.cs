using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DAL;
using Ecommerce.DAL.DataModels;
using Ecommerce.DAL.Repositories;
using Ecommerce.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Ecommerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await SetPrerequisite(host);
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.ClearProviders();
                    // Enable NLog as one of the Logging Provider
                    logging.AddNLog();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        #region Helper Methods
        public static async Task SetPrerequisite(IHost host)
        {
            using (var serviceScope = host.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<EcommerceDbContext>();

                await dbContext.Database.MigrateAsync();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                if (!await roleManager.RoleExistsAsync(ApplicationConstant.ApplicationRoles.Administrator))
                {
                    var adminRole = new ApplicationRole(ApplicationConstant.ApplicationRoles.Administrator);
                    await roleManager.CreateAsync(adminRole);
                }

                if (!await roleManager.RoleExistsAsync(ApplicationConstant.ApplicationRoles.Retailer))
                {
                    var adminRole = new ApplicationRole(ApplicationConstant.ApplicationRoles.Retailer);
                    await roleManager.CreateAsync(adminRole);
                }

                if (!await roleManager.RoleExistsAsync(ApplicationConstant.ApplicationRoles.Customer))
                {
                    var adminRole = new ApplicationRole(ApplicationConstant.ApplicationRoles.Customer);
                    await roleManager.CreateAsync(adminRole);
                }
                var admin = await userManager.FindByEmailAsync("admin@fashionpoint.com");
                if (admin == null)
                {
                    var newUser = new ApplicationUser
                    {
                        UserName = "admin@fashionpoint.com",
                        Email = "admin@fashionpoint.com",
                        ContactNo = "",
                        AddressLine1 = "",
                        AddressLine2 = "",
                        City = "",
                        State = "",
                        Zipcode = ""
                    };
                    await userManager.CreateAsync(newUser, "Admin123*");
                    await userManager.AddToRoleAsync(newUser, ApplicationConstant.ApplicationRoles.Administrator);
                }
            }
        }
        #endregion
    }
}
 