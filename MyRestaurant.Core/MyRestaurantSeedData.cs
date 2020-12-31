using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant.Core
{
    public class SuperAdminAccount
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
    public class MyRestaurantSeedData
    {
        public static async Task Initialize(IServiceProvider services)
        {
            var context = services.GetRequiredService<MyRestaurantContext>();
            var logger = services.GetRequiredService<ILogger<MyRestaurantSeedData>>();
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<Role>>();
            var superAdminSettings = services.GetRequiredService<IOptions<SuperAdminAccount>>().Value;

            context.Database.Migrate();
            context.Database.EnsureCreated();

            var roleTypes = new List<Roles> { Roles.SuperAdmin, Roles.Admin, Roles.Report, Roles.Normal };
            if (!roleManager.Roles.Any())
            {
                var roles = roleTypes.Select(x => new Role { Name = x.ToString(), NormalizedName = x.ToString().ToUpper() });

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            if (!userManager.Users.Any())
            {
                var user = new User
                {
                    FirstName = superAdminSettings.FirstName,
                    LastName = superAdminSettings.LastName,
                    Email = superAdminSettings.Email,
                    UserName = superAdminSettings.Email
                };

                var result = await userManager.CreateAsync(user, "superAdmin1@#");
                if (result.Succeeded)
                    await userManager.AddToRolesAsync(user, roleTypes.Select(x => x.ToString()));
            }
        }
    }
}
