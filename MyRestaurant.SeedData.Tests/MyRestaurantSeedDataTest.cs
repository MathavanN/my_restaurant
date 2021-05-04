using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRestaurant.Core;
using MyRestaurant.SeedData.Tests.Fixture;
using System.Linq;
using Xunit;

namespace MyRestaurant.SeedData.Tests
{
    [Collection("Database")]
    public class MyRestaurantSeedDataTest : IClassFixture<MyRestaurantWebApplicationFactory>
    {
        private readonly MyRestaurantWebApplicationFactory _factory;
        public MyRestaurantSeedDataTest(MyRestaurantWebApplicationFactory factory) => _factory = factory;

        [Fact]
        public async void Initialize_MyRestaurantDatabase_SeedData()
        {
            //Arrange
            var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var databaseInitializer = scope.ServiceProvider.GetRequiredService<IMyRestaurantSeedData>();
            await databaseInitializer.Initialize();

            var context = scope.ServiceProvider.GetRequiredService<MyRestaurantContext>();

            //Act
            var roles = await context.Roles.ToListAsync();
            var users = await context.Users.ToListAsync();

            //Assert
            roles.Count.Should().Be(4);
            roles[0].Name.Should().Be("SuperAdmin");

            users.Count.Should().Be(1);
        }

        [Fact]
        public async void Insert_New_MyRestaurant_Entity_Add_AduitLog()
        {
            //Arrange
            var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var databaseInitializer = scope.ServiceProvider.GetRequiredService<IMyRestaurantSeedData>();
            await databaseInitializer.Initialize();

            var context = scope.ServiceProvider.GetRequiredService<MyRestaurantContext>();

            //Act
            var unitOfMeasureLogs = await context.Audits.Where(x => x.TableName == "UnitOfMeasures").ToListAsync();
            
            //Assert
            unitOfMeasureLogs.Count.Should().Be(0);

            context.UnitOfMeasures.Add(new Models.UnitOfMeasure { Code = "kg", Description = "Test Description" });
            await context.SaveChangesAsync();

            unitOfMeasureLogs = await context.Audits.Where(x => x.TableName == "UnitOfMeasures").ToListAsync();

            //Assert
            unitOfMeasureLogs.Count().Should().Be(1);
            unitOfMeasureLogs[0].Action.Should().Be("Added");
            unitOfMeasureLogs[0].KeyValues.Should().Be("{\"Id\":1}");
            unitOfMeasureLogs[0].OldValues.Should().BeNull();
            unitOfMeasureLogs[0].NewValues.Should().Be("{\"Code\":\"kg\",\"Description\":\"Test Description\"}");
        }

        [Fact]
        public async void Update_Existing_MyRestaurant_Entity_Add_AduitLog()
        {
            //Arrange
            var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var databaseInitializer = scope.ServiceProvider.GetRequiredService<IMyRestaurantSeedData>();
            await databaseInitializer.Initialize();

            var context = scope.ServiceProvider.GetRequiredService<MyRestaurantContext>();
            
            //Act
            var stockTypeLogs = await context.Audits.Where(x => x.TableName == "StockTypes").ToListAsync();
            
            //Assert
            stockTypeLogs.Count.Should().Be(0);

            //Act
            context.StockTypes.Add(new Models.StockType { Type = "OfficeItems", Description = "Office Items" });
            await context.SaveChangesAsync();

            var stockType = await context.StockTypes.Where(x => x.Id == 1).FirstOrDefaultAsync();
            stockType.Type = "Stationery";
            await context.SaveChangesAsync();

            stockTypeLogs = await context.Audits.Where(x => x.TableName == "StockTypes").ToListAsync();

            //Assert
            stockTypeLogs.Count().Should().Be(2);
            stockTypeLogs[0].Action.Should().Be("Added");
            stockTypeLogs[0].KeyValues.Should().Be("{\"Id\":1}");
            stockTypeLogs[0].OldValues.Should().BeNull();
            stockTypeLogs[0].NewValues.Should().Be("{\"Description\":\"Office Items\",\"Type\":\"OfficeItems\"}");

            stockTypeLogs[1].Action.Should().Be("Modified");
            stockTypeLogs[1].KeyValues.Should().Be("{\"Id\":1}");
            stockTypeLogs[1].OldValues.Should().Be("{\"Id\":1,\"Description\":\"Office Items\",\"Type\":\"OfficeItems\"}");
            stockTypeLogs[1].NewValues.Should().Be("{\"Id\":1,\"Description\":\"Office Items\",\"Type\":\"Stationery\"}");
        }
    }
}
