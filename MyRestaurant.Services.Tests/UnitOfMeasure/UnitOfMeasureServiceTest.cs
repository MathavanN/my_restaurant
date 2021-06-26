using FluentAssertions;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyRestaurant.Services.Tests
{
    public class UnitOfMeasureServiceTest : MyRestaurantContextTestBase
    {
        public UnitOfMeasureServiceTest()
        {
            UnitOfMeasureInitializer.Initialize(_myRestaurantContext);
        }

        [Fact]
        public async Task GetUnitOfMeasuresAsync_Returns_UnitOfMeasures()
        {
            //Arrange
            var service = new UnitOfMeasureService(_myRestaurantContext);

            //Act
            var result = await service.GetUnitOfMeasuresAsync();

            //Assert
            result.Should().BeAssignableTo<IEnumerable<UnitOfMeasure>>();
            result.Should().HaveCount(5);
        }

        [Fact]
        public async Task GetUnitOfMeasureAsync_Returns_UnitOfMeasure()
        {
            //Arrange
            var id = 1;
            var service = new UnitOfMeasureService(_myRestaurantContext);

            //Act
            var result = await service.GetUnitOfMeasureAsync(d => d.Id == id);

            //Assert
            result.Should().BeAssignableTo<UnitOfMeasure>();
            result.Id.Should().Be(id);
            result.Code.Should().Be("kg");
            result.Description.Should().Be("kg units");
        }

        [Fact]
        public async Task GetUnitOfMeasureAsync_Returns_Null()
        {
            //Arrange
            var id = 10;
            var service = new UnitOfMeasureService(_myRestaurantContext);

            //Act
            var result = await service.GetUnitOfMeasureAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddUnitOfMeasureAsync_Returns_New_UnitOfMeasure()
        {
            //Arrange
            var service = new UnitOfMeasureService(_myRestaurantContext);

            //Act
            var result = await service.AddUnitOfMeasureAsync(new UnitOfMeasure { Code = "gal", Description = "gallon" });

            //Assert
            result.Should().BeAssignableTo<UnitOfMeasure>();
            result.Code.Should().Be("gal");
            result.Description.Should().Be("gallon");

            //Act
            var uoms = await service.GetUnitOfMeasuresAsync();

            //Assert
            uoms.Should().HaveCount(6);
        }

        [Fact]
        public async Task UpdateUnitOfMeasureAsync_Successfully_Updated()
        {
            //Arrange
            var id = 3;
            var service = new UnitOfMeasureService(_myRestaurantContext);

            //Act
            var dbUnitOfMeasure = await service.GetUnitOfMeasureAsync(d => d.Id == id);
            dbUnitOfMeasure.Code = "ml";
            dbUnitOfMeasure.Description = "ml units to add";

            await service.UpdateUnitOfMeasureAsync(dbUnitOfMeasure);

            var result = await service.GetUnitOfMeasureAsync(d => d.Id == id);

            //Assert
            result.Should().BeAssignableTo<UnitOfMeasure>();
            result.Id.Should().Be(id);
            result.Code.Should().Be("ml");
            result.Description.Should().Be("ml units to add");
        }

        [Fact]
        public async Task DeleteUnitOfMeasureAsync_Successfully_Deleted()
        {
            //Arrange
            var id = 1;
            var service = new UnitOfMeasureService(_myRestaurantContext);

            //Act
            var dbUnitOfMeasure = await service.GetUnitOfMeasureAsync(d => d.Id == id);

            await service.DeleteUnitOfMeasureAsync(dbUnitOfMeasure);

            var result = await service.GetUnitOfMeasureAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }
    }
}
