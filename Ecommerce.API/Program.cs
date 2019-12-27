using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DAL;
using Ecommerce.DAL.DataModels;
using Ecommerce.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ecommerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var serviceScope = host.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<EcommerceDbContext>();

                await dbContext.Database.MigrateAsync();

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
            }


            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
