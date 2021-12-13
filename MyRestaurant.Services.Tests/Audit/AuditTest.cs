using FluentAssertions;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyRestaurant.Services.Tests
{
    public class AuditTest : MyRestaurantContextTestBase
    {
        public AuditTest()
        {
        }

        [Fact]
        public async Task Verify_Audit_Has_UnitOfMeasure_Insert_History_Data()
        {
            //Arrange
            _myRestaurantContext.Create(new UnitOfMeasure { Code = "Test", Description = "Audit test" });
            await _myRestaurantContext.CommitAsync();

            //Act
            var result = _myRestaurantContext.Audits.ToList().Where(d => d.TableName == "UnitOfMeasures");

            //Assert
            result.Should().BeAssignableTo<IEnumerable<Audit>>();
            result.First().Id.Should().NotBeEmpty();
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task Verify_Audit_Has_UnitOfMeasure_Update_History_Data()
        {
            //Arrange
            _myRestaurantContext.Create(new UnitOfMeasure { Code = "Test", Description = "Audit test" });
            await _myRestaurantContext.CommitAsync();

            var dbUOM = _myRestaurantContext.UnitOfMeasures.FirstOrDefault(d => d.Code == "Test");
            dbUOM!.Description = "Audit test updated";
            _myRestaurantContext.Modify(dbUOM);

            await _myRestaurantContext.CommitAsync();

            //Act
            var result = _myRestaurantContext.Audits.ToList().Where(d => d.TableName == "UnitOfMeasures");

            //Assert
            result.Should().BeAssignableTo<IEnumerable<Audit>>();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task Verify_DeleteRange_Deleted_Successfully()
        {
            //Arrange
            UnitOfMeasureInitializer.Initialize(_myRestaurantContext);

            //Act
            var uoms = await _myRestaurantContext.GetAllAsync<UnitOfMeasure>();

            //Assert
            uoms.Should().HaveCount(5);

            //Act
            _myRestaurantContext.DeleteRange(uoms);
            await _myRestaurantContext.CommitAsync();
            uoms = await _myRestaurantContext.GetAllAsync<UnitOfMeasure>();

            //Assert
            uoms.Should().HaveCount(0);
        }
    }
}
