using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MyRestaurant.Api.Tests.Swagger.Fixture
{
    public class MyRestaurantProdEnvTestFixture : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Production");
        }
    }
}
