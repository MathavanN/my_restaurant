using Microsoft.EntityFrameworkCore;
using MyRestaurant.Core;
using System;

namespace MyRestaurant.Services.Tests
{
    public class MyRestaurantContextTestBase : IDisposable
    {
        protected readonly MyRestaurantContext _myRestaurantContext;
        private bool _disposed;

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
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // remove the temp db from the server once all tests are done
                    _myRestaurantContext.Database.EnsureDeleted();
                    _myRestaurantContext.Dispose();
                }

                _disposed = true;
            }
        }
    }
}
