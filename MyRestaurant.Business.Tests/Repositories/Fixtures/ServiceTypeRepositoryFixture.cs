using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class ServiceTypeRepositoryFixture : IDisposable
    {
        public Mock<IServiceTypeService> MockServiceTypeService { get; private set; }
        public IEnumerable<ServiceType> ServiceTypes { get; private set; }
        public CreateServiceTypeDto CreateServiceTypeDto { get; private set; }
        public EditServiceTypeDto EditServiceTypeDto { get; private set; }
        public ServiceType CreatedNewServiceType { get; private set; }

        public ServiceTypeRepositoryFixture()
        {
            MockServiceTypeService = new Mock<IServiceTypeService>();

            ServiceTypes = new List<ServiceType>
            {
                new ServiceType { Id = 1, Type = "Take Away" },
                new ServiceType { Id = 2, Type = "Dine In" }
            };

            CreateServiceTypeDto = new CreateServiceTypeDto
            {
                Type = "Buffet"
            };

            CreatedNewServiceType = new ServiceType
            {
                Id = 3,
                Type = CreateServiceTypeDto.Type
            };

            EditServiceTypeDto = new EditServiceTypeDto
            {
                Type = "Take Out"
            };
        }
        public void Dispose()
        {
            MockServiceTypeService = null;
        }
    }
}
