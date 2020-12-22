using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyRestaurant.Core.Configurations.Mapping;
using MyRestaurant.Core.Models;
using System;

namespace MyRestaurant.Core
{
    public class MyRestaurantContext : IdentityDbContext<User, Role, Guid>
    {
        public MyRestaurantContext(DbContextOptions<MyRestaurantContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ServiceTypeMapping());
        }
    }
}
