using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Api.Tests.Controllers.V1.Fixtures
{
    public class ServiceTypeControllerFixture : IDisposable
    {
        private bool _disposed;
        public ApiVersion ApiVersion { get; private set; }
        public Mock<IServiceTypeRepository> MockServiceTypeRepository { get; private set; }
        public IEnumerable<GetServiceTypeDto> ServiceTypes { get; private set; }
        public CreateServiceTypeDto ValidCreateServiceTypeDto { get; private set; }
        public GetServiceTypeDto CreateServiceTypeDtoResult { get; private set; }
        public EditServiceTypeDto ValidUpdateServiceTypeDto { get; private set; }
        public GetServiceTypeDto EditServiceTypeDtoResult { get; set; }

        public ServiceTypeControllerFixture()
        {
            ApiVersion = new ApiVersion(1, 0);

            MockServiceTypeRepository = new Mock<IServiceTypeRepository>();

            ServiceTypes = new List<GetServiceTypeDto> {
                new GetServiceTypeDto { Id = 1, Type = "Take Away" },
            };

            ValidCreateServiceTypeDto = new CreateServiceTypeDto { Type = "Dine In" };

            CreateServiceTypeDtoResult = new GetServiceTypeDto { Id = 2, Type = "Dine In" };

            ValidUpdateServiceTypeDto = new EditServiceTypeDto { Type = "Take Out" };

            EditServiceTypeDtoResult = new GetServiceTypeDto { Id = 1, Type = "Take Out" };
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
                    MockServiceTypeRepository = null;
                }

                _disposed = true;
            }
        }
    }
}
