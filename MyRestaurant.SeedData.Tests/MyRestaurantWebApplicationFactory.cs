using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using MyRestaurant.Api;
using System.Collections.Generic;
using Xunit;

namespace MyRestaurant.SeedData.Tests
{
    [Collection("Database")]
    public class MyRestaurantWebApplicationFactory : WebApplicationFactory<Startup>
    {
        private readonly DatabaseFixture _dbFixture;

        public MyRestaurantWebApplicationFactory(DatabaseFixture dbFixture) => _dbFixture = dbFixture;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string, string>(
                        "ConnectionStrings:RestaurantConnectionString", _dbFixture.ConnectionString)
                });
            });
        }
    }
}
