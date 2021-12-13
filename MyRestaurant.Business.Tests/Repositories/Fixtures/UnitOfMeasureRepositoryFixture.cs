using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class UnitOfMeasureRepositoryFixture : IDisposable
    {
        private bool _disposed;
        public Mock<IUnitOfMeasureService> MockUnitOfMeasureService { get; private set; }
        public IEnumerable<UnitOfMeasure> UnitOfMeasures { get; private set; }
        public CreateUnitOfMeasureDto CreateUnitOfMeasureDto { get; private set; }
        public EditUnitOfMeasureDto EditUnitOfMeasureDto { get; private set; }
        public UnitOfMeasure CreatedNewUnitOfMeasure { get; private set; }

        public UnitOfMeasureRepositoryFixture()
        {
            MockUnitOfMeasureService = new Mock<IUnitOfMeasureService>();

            UnitOfMeasures = new List<UnitOfMeasure> {
                new UnitOfMeasure { Id = 1, Code = "kg", Description = "kg units" },
                new UnitOfMeasure { Id = 2, Code = "g", Description = "g units" },
                new UnitOfMeasure { Id = 3, Code = "ml", Description = "ml units" }
            };

            CreateUnitOfMeasureDto = new CreateUnitOfMeasureDto { Code = "l", Description = "l units" };

            CreatedNewUnitOfMeasure = new UnitOfMeasure { Id = 4, Code = CreateUnitOfMeasureDto.Code, Description = CreateUnitOfMeasureDto.Description };

            EditUnitOfMeasureDto = new EditUnitOfMeasureDto { Code = "ml", Description = "ml units to add" };
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
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                    MockUnitOfMeasureService = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
                }

                _disposed = true;
            }
        }
    }
}
