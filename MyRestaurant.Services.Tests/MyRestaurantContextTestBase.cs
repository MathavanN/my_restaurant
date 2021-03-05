using Microsoft.EntityFrameworkCore;
using MyRestaurant.Core;
using System;

namespace MyRestaurant.Services.Tests
{
    public class MyRestaurantContextTestBase : IDisposable
    {
        protected readonly MyRestaurantContext _myRestaurantContext;

        public MyRestaurantContextTestBase()
        {
            var options = new DbContextOptionsBuilder<MyRestaurantContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _myRestaurantContext = new MyRestaurantContext(options);

            _myRestaurantContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _myRestaurantContext.Database.EnsureDeleted();

            _myRestaurantContext.Dispose();
        }
    }
}
