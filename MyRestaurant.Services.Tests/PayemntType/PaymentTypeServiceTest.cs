﻿using FluentAssertions;
using MyRestaurant.Models;
using System.Collections.Generic;
using Xunit;

namespace MyRestaurant.Services.Tests
{
    public class PaymentTypeServiceTest : MyRestaurantContextTestBase
    {
        public PaymentTypeServiceTest()
        {
            PaymentTypeInitializer.Initialize(_myRestaurantContext);
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
        public async void GetPaymentTypeAsync_Returns_Null()
        {
            //Arrange
            var id = 10;
            var service = new PaymentTypeService(_myRestaurantContext);

            //Act
            var result = await service.GetPaymentTypeAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
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

        [Fact]
        public async void UpdatePaymentTypeAsync_Successfully_Updated()
        {
            //Arrange
            var id = 2;
            var service = new PaymentTypeService(_myRestaurantContext);

            //Act
            var dbPaymentType = await service.GetPaymentTypeAsync(d => d.Id == id);
            dbPaymentType.Name = "Credit";
            dbPaymentType.CreditPeriod = 45;

            await service.UpdatePaymentTypeAsync(dbPaymentType);

            var result = await service.GetPaymentTypeAsync(d => d.Id == id);

            //Assert
            var paymentType = result.Should().BeAssignableTo<PaymentType>().Subject;
            paymentType.Id.Should().Be(id);
            paymentType.Name.Should().Be("Credit");
            paymentType.CreditPeriod.Should().Be(45);
        }

        [Fact]
        public async void DeletePaymentTypeAsync_Successfully_Deleted()
        {
            //Arrange
            var id = 1;
            var service = new PaymentTypeService(_myRestaurantContext);

            //Act
            var dbPaymentType = await service.GetPaymentTypeAsync(d => d.Id == id);

            await service.DeletePaymentTypeAsync(dbPaymentType);

            var result = await service.GetPaymentTypeAsync(d => d.Id == id);

            //Assert
            result.Should().BeNull();
        }
    }
}
