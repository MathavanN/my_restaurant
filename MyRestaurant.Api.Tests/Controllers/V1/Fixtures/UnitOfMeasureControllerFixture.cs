using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Api.Tests.Controllers.V1.Fixtures
{
    public class UnitOfMeasureControllerFixture : IDisposable
    {
        private bool _disposed;
        public ApiVersion ApiVersion { get; private set; }
        public Mock<IUnitOfMeasureRepository> MockUnitOfMeasureRepository { get; private set; }
        public IEnumerable<GetUnitOfMeasureDto> UnitOfMeasures { get; private set; }
        public CreateUnitOfMeasureDto ValidCreateUnitOfMeasureDto { get; private set; }
        public GetUnitOfMeasureDto CreateUnitOfMeasureDtoResult { get; private set; }
        public EditUnitOfMeasureDto ValidEditUnitOfMeasureDto { get; private set; }
        public GetUnitOfMeasureDto EditUnitOfMeasureDtoResult { get; private set; }

        public UnitOfMeasureControllerFixture()
        {
            ApiVersion = new ApiVersion(1, 0);

            MockUnitOfMeasureRepository = new Mock<IUnitOfMeasureRepository>();

            UnitOfMeasures = new List<GetUnitOfMeasureDto>
            {
                new GetUnitOfMeasureDto { Id = 1, Code = "kg", Description = "Kilogram" },
                new GetUnitOfMeasureDto { Id = 2, Code = "g", Description = "Gram" }
            };

            ValidCreateUnitOfMeasureDto = new CreateUnitOfMeasureDto
            {
                Code = "l",
                Description = "Liter"
            };

            CreateUnitOfMeasureDtoResult = new GetUnitOfMeasureDto
            {
                Id = 3,
                Code = "l",
                Description = "Liter"
            };

            ValidEditUnitOfMeasureDto = new EditUnitOfMeasureDto
            {
                Code = "g",
                Description = "Gram test"
            };

            EditUnitOfMeasureDtoResult = new GetUnitOfMeasureDto
            {
                Id = 2,
                Code = "g",
                Description = "Gram test"
            };
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
                    MockUnitOfMeasureRepository = null;
                }

                _disposed = true;
            }
        }
    }
}
