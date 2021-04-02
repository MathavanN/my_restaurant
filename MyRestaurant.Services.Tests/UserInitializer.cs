using MyRestaurant.Core;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Services.Tests
{
    public class UserInitializer
    {
        public static void Initialize(MyRestaurantContext context)
        {
            var superAdmin = new Role { Name = "SuperAdmin" };
            var admin = new Role { Name = "Admin" };
            var report = new Role { Name = "Report" };
            var normal = new Role { Name = "Normal" };

            if (!context.Roles.Any())
            {
                var roles = new List<Role> { superAdmin, admin, report, normal };
                context.Roles.AddRange(roles);
                context.SaveChanges();
            }

            var superAdminUser = new User { FirstName = "Golden", LastName = "Dining", Email = "abc@gmail.com" };
            var adminUser = new User { FirstName = "Admin", LastName = "Access", Email = "admin@gmail.com" };
            var reportUser = new User { FirstName = "Report", LastName = "Access", Email = "report@gmail.com" };
            var normalUser = new User { FirstName = "Normal", LastName = "Access", Email = "normal@gmail.com" };

            if (!context.Users.Any())
            {
                superAdminUser.UserRoles.Add(new UserRole { User = superAdminUser, Role = superAdmin });
                adminUser.UserRoles.Add(new UserRole { User = adminUser, Role = admin });
                reportUser.UserRoles.Add(new UserRole { User = reportUser, Role = report });
                normalUser.UserRoles.Add(new UserRole { User = normalUser, Role = normal });

                var users = new List<User> { superAdminUser, adminUser, reportUser, normalUser };
                context.Users.AddRange(users);
                context.SaveChanges();
            }

            if (!context.UserRoles.Any())
            {
                var dbRoles = context.Roles.ToList();
                var dbUsers = context.Users.ToList();
                var userRoles = new List<UserRole> { };
                foreach (var user in dbUsers)
                {
                    if (user.FirstName == "Golden")
                    {
                        foreach (var role in dbRoles)
                        {
                            userRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
                        }
                    }
                    if (user.FirstName == "Admin")
                    {
                        userRoles.Add(new UserRole { UserId = user.Id, RoleId = dbRoles.First(d => d.Name == "Admin").Id });
                    }
                    if (user.FirstName == "Report")
                    {
                        userRoles.Add(new UserRole { UserId = user.Id, RoleId = dbRoles.First(d => d.Name == "Report").Id });
                    }
                    if (user.FirstName == "Normal")
                    {
                        userRoles.Add(new UserRole { UserId = user.Id, RoleId = dbRoles.First(d => d.Name == "Normal").Id });
                    }
                }
                context.UserRoles.AddRange(userRoles);
                context.SaveChanges();
            }
        }
    }
}