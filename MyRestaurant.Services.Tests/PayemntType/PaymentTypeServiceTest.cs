using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MyRestaurant.Core;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace MyRestaurant.Services.Tests.PayemntType
{
    public class PaymentTypeServiceTest
    {
        public MyRestaurantContext _myRestaurantContext;
        public PaymentTypeServiceTest()
        {
            var options = new DbContextOptionsBuilder<MyRestaurantContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            _myRestaurantContext = new MyRestaurantContext(options);

            SeedData();
        }

        private async void SeedData()
        {
            _myRestaurantContext.PaymentTypes.Add(new PaymentType { Name = "Cash", CreditPeriod = 0 });
            _myRestaurantContext.PaymentTypes.Add(new PaymentType { Name = "Credit", CreditPeriod = 30 });
            _myRestaurantContext.PaymentTypes.Add(new PaymentType { Name = "Credit100", CreditPeriod = 100 });

            await _myRestaurantContext.CommitAsync();
        }

        [Fact]
        public async void GetPaymentTypesAsync_Returns_PaymentTypes()
        {
            //Arrange
            var service = new PaymentTypeService(_myRestaurantContext);

            //Act
            var result = await service.GetPaymentTypesAsync();

            //Assert
            var paymentTypes = result.Should().BeAssignableTo<IEnumerable<PaymentType>>().Subject;
            paymentTypes.Should().HaveCount(3);
        }

        [Fact]
        public async void GetPaymentTypeAsync_Returns_PaymentType()
        {
            //Arrange
            var id = 1;
            var service = new PaymentTypeService(_myRestaurantContext);

            //Act
            var result = await service.GetPaymentTypeAsync(d => d.Id == id);

            //Assert
            var paymentType = result.Should().BeAssignableTo<PaymentType>().Subject;
            paymentType.Id.Should().Be(id);
            paymentType.Name.Should().Be("Cash");
            paymentType.CreditPeriod.Should().Be(0);
        }

        [Fact]
        public async void AddPaymentTypeAsync_Returns_New_PaymentType()
        {
            //Arrange
            var service = new PaymentTypeService(_myRestaurantContext);

            //Act
            var result = await service.AddPaymentTypeAsync(new PaymentType { Name = "Credit1", CreditPeriod = 15 });

            //Assert
            var paymentType = result.Should().BeAssignableTo<PaymentType>().Subject;
            paymentType.Name.Should().Be("Credit1");
            paymentType.CreditPeriod.Should().Be(15);
        }

        //[Fact]
        //public async void UpdatePaymentTypeAsync_Successfully_Updated()
        //{
        //    //Arrange
        //    var id = 2;
        //    var service = new PaymentTypeService(_myRestaurantContext);

        //    //Act
        //    await service.UpdatePaymentTypeAsync(new PaymentType { Id = id, Name = "Credit", CreditPeriod = 45 });
        //    var result = await service.GetPaymentTypeAsync(d => d.Id == id);

        //    //Assert
        //    var paymentType = result.Should().BeAssignableTo<PaymentType>().Subject;
        //    paymentType.Id.Should().Be(id);
        //    paymentType.Name.Should().Be("Credit");
        //    paymentType.CreditPeriod.Should().Be(45);
        //}

        //[Fact]
        //public async void DeletePaymentTypeAsync_Successfully_Deleted()
        //{
        //    //Arrange
        //    var service = new PaymentTypeService(_myRestaurantContext);

        //    //Act
        //    await service.DeletePaymentTypeAsync(new PaymentType { Id = 1, Name = "Credit1", CreditPeriod = 15 });
        //    var result = await service.GetPaymentTypesAsync();

        //    //Assert
        //    var paymentTypes = result.Should().BeAssignableTo<IEnumerable<PaymentType>>().Subject;
        //    paymentTypes.Should().HaveCount(2);
        //}
    }
}
