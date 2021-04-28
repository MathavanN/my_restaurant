using Microsoft.AspNetCore.Identity;
using MyRestaurant.Core;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Services.Tests
{
    public class UserInitializer
    {
        private static void RoleInitialize(MyRestaurantContext context)
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
        }

        private static void UserInitialize(MyRestaurantContext context)
        {
            var superAdminUser = new User { FirstName = "Golden", LastName = "Dining", Email = "abc@gmail.com" };
            var adminUser = new User { FirstName = "Admin", LastName = "Access", Email = "admin@gmail.com" };
            var reportUser = new User { FirstName = "Report", LastName = "Access", Email = "report@gmail.com" };
            var normalUser = new User { FirstName = "Normal", LastName = "Access", Email = "normal@gmail.com" };

            if (!context.Users.Any())
            {
                var dbRoles = context.Roles.ToList();
                superAdminUser.UserRoles.Add(new UserRole { User = superAdminUser, Role = dbRoles.FirstOrDefault(x => x.Name == "SuperAdmin") });
                adminUser.UserRoles.Add(new UserRole { User = adminUser, Role = dbRoles.FirstOrDefault(x => x.Name == "Admin") });
                reportUser.UserRoles.Add(new UserRole { User = reportUser, Role = dbRoles.FirstOrDefault(x => x.Name == "Report") });
                normalUser.UserRoles.Add(new UserRole { User = normalUser, Role = dbRoles.FirstOrDefault(x => x.Name == "Normal") });

                var users = new List<User> { superAdminUser, adminUser, reportUser, normalUser };
                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }

        private static void UserRoleInitialize(MyRestaurantContext context)
        {
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

        private static void UserClaimInitialize(MyRestaurantContext context)
        {
            if (!context.UserClaims.Any())
            {
                var dbUsers = context.Users.ToList();
                var claims = new List<IdentityUserClaim<Guid>> { };
                foreach (var user in dbUsers)
                {
                    if (user.FirstName == "Golden")
                    {
                        claims.Add(new IdentityUserClaim<Guid> { UserId = user.Id, ClaimType = "AccessCategory", ClaimValue = "SuperAdmin" });
                    }
                    if (user.FirstName == "Admin")
                    {
                        claims.Add(new IdentityUserClaim<Guid> { UserId = user.Id, ClaimType = "AccessCategory", ClaimValue = "Admin" });
                    }
                    if (user.FirstName == "Report")
                    {
                        claims.Add(new IdentityUserClaim<Guid> { UserId = user.Id, ClaimType = "AccessCategory", ClaimValue = "Report" });
                    }
                    if (user.FirstName == "Normal")
                    {
                        claims.Add(new IdentityUserClaim<Guid> { UserId = user.Id, ClaimType = "AccessCategory", ClaimValue = "Normal" });
                    }
                }

                context.UserClaims.AddRange(claims);
                context.SaveChanges();
            }
        }

        public static void Initialize(MyRestaurantContext context)
        {
            RoleInitialize(context);
            UserInitialize(context);
            UserRoleInitialize(context);
            UserClaimInitialize(context);
        }
    }
}