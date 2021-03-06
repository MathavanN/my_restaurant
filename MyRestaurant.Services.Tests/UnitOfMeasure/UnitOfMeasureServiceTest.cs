using FluentAssertions;
using MyRestaurant.Models;
using System.Collections.Generic;
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
        public async void GetUnitOfMeasuresAsync_Returns_UnitOfMeasures()
        {
            //Arrange
            var service = new UnitOfMeasureService(_myRestaurantContext);

            //Act
            var result = await service.GetUnitOfMeasuresAsync();

            //Assert
            var uoms = result.Should().BeAssignableTo<IEnumerable<UnitOfMeasure>>().Subject;
            uoms.Should().HaveCount(5);
        }

        [Fact]
        public async void GetUnitOfMeasureAsync_Returns_UnitOfMeasure()
        {
            //Arrange
            var id = 1;
            var service = new UnitOfMeasureService(_myRestaurantContext);

            //Act
            var result = await service.GetUnitOfMeasureAsync(d => d.Id == id);

            //Assert
            var unitOfMeasure = result.Should().BeAssignableTo<UnitOfMeasure>().Subject;
            unitOfMeasure.Id.Should().Be(id);
            unitOfMeasure.Code.Should().Be("kg");
            unitOfMeasure.Description.Should().Be("kg units");
        }

        [Fact]
        public async void GetUnitOfMeasureAsync_Returns_Null()
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
        public async void AddUnitOfMeasureAsync_Returns_New_UnitOfMeasure()
        {
            //Arrange
            var service = new UnitOfMeasureService(_myRestaurantContext);

            //Act
            var result = await service.AddUnitOfMeasureAsync(new UnitOfMeasure { Code = "gal", Description = "gallon" });

            //Assert
            var uom = result.Should().BeAssignableTo<UnitOfMeasure>().Subject;
            uom.Code.Should().Be("gal");
            uom.Description.Should().Be("gallon");

            //Act
            var uoms = await service.GetUnitOfMeasuresAsync();

            //Assert
            uoms.Should().HaveCount(6);
        }

        [Fact]
        public async void UpdateUnitOfMeasureAsync_Successfully_Updated()
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
            var uom = result.Should().BeAssignableTo<UnitOfMeasure>().Subject;
            uom.Id.Should().Be(id);
            uom.Code.Should().Be("ml");
            uom.Description.Should().Be("ml units to add");
        }

        [Fact]
        public async void DeleteUnitOfMeasureAsync_Successfully_Deleted()
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
