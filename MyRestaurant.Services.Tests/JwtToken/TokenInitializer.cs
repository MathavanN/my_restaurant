using MyRestaurant.Core;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Services.Tests
{
    public class TokenInitializer
    {
        public static void Initialize(MyRestaurantContext context)
        {
            if (!context.RefreshTokens.Any())
            {
                var refreshTokens = new List<RefreshToken>
                {
                    new RefreshToken
                    {
                        UserId = context.Users.First(d => d.FirstName == "Admin").Id,
                        Token = "token1",
                        Expires = DateTime.Now.AddHours(2),
                        Created = DateTime.Now,
                        CreatedByIp = "0.0.0.1",
                    },
                    new RefreshToken
                    {
                        UserId = context.Users.First(d => d.FirstName == "Normal").Id,
                        Token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2MDk3NDc5ODAsImV4cCI6MTYwOTc2OTU4MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzMDEiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo0NDMwMSJ9.kvX-GbWYLkgEY3Kl9RaRvESbNRkl8NDBxGNTcTFUGBpaLwSM8oWt9U6bKQNoPbcAbNui3ubvbCapkmc3SWVmfg",
                        Expires = DateTime.Now.AddDays(-10).AddHours(2),
                        Revoked = null,
                        Created = DateTime.Now.AddDays(-10),
                        CreatedByIp = "0.0.0.1",
                    },
                    new RefreshToken
                    {
                        UserId = context.Users.First(d => d.FirstName == "Report").Id,
                        Token = "token3",
                        Expires = DateTime.Now.AddHours(2),
                        Created = DateTime.Now,
                        CreatedByIp = "0.0.0.1",
                    },
                    new RefreshToken
                    {
                        UserId = context.Users.First(d => d.FirstName == "Golden").Id,
                        Token = "token4",
                        Expires = DateTime.Now.AddHours(2),
                        Created = DateTime.Now,
                        CreatedByIp = "0.0.0.1",
                    }
                };

                context.RefreshTokens.AddRange(refreshTokens);
                context.SaveChanges();
            }
        }
    }
}
