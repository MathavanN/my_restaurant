using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Business.Tests.Repositories.Fixtures
{
    public class AccountRepositoryFixture : IDisposable
    {
        private bool _disposed;
        public Mock<IJwtTokenService> MockJwtTokenService { get; private set; }
        public Mock<IUserAccessorService> MockUserAccessorService { get; private set; }
        public Mock<UserManager<User>> MockUserManager { get; private set; }
        public CurrentUser CurrentUser { get; private set; }
        public IEnumerable<User> Users { get; private set; }
        public IEnumerable<RefreshToken> RefreshTokens { get; private set; }
        public RevokeDto RevokeDto { get; private set; }
        public AccountRepositoryFixture()
        {
            MockUserAccessorService = new Mock<IUserAccessorService>();
            MockJwtTokenService = new Mock<IJwtTokenService>();
            MockUserManager = GetUserManagerMock<User>();

            CurrentUser = new CurrentUser
            {
                Email = "abc@gmail.com",
                Roles = new List<string> { "SuperAdmin", "Admin", "Report", "Normal" },
                UserId = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                FirstName = "Golden",
                LastName = "Dining",
            };
            var superAdmin = new Role { Id = Guid.Parse("77d8500b-dd97-4b6d-ce43-18d8aa3916b9"), Name = "SuperAdmin" };
            var admin = new Role { Id = Guid.Parse("77d8500b-dd97-4b6d-ce43-28d8aa3916b9"), Name = "Admin" };
            var report = new Role { Id = Guid.Parse("77d8500b-dd97-4b6d-ce43-38d8aa3916b9"), Name = "Report" };
            var normal = new Role { Id = Guid.Parse("77d8500b-dd97-4b6d-ce43-48d8aa3916b9"), Name = "Normal" };

            var superAdminUser = new User
            {
                Id = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa3916b9"),
                FirstName = "Golden",
                LastName = "Dining",
                Email = "abc@gmail.com",
                PasswordHash = "AAAeQhwKjV/UynrkBur+6NUp0P92MWWlSsGtj3uEkN72pojCpK1lNXbMOi37O3REiw=="
            };
            var adminUser = new User
            {
                Id = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa391610"),
                FirstName = "Admin",
                LastName = "Access",
                Email = "admin@gmail.com"
            };
            var reportUser = new User
            {
                Id = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa391611"),
                FirstName = "Report",
                LastName = "Access",
                Email = "report@gmail.com"
            };
            var normalUser = new User
            {
                Id = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa391612"),
                FirstName = "Normal",
                LastName = "Access",
                Email = "normal@gmail.com"
            };
            superAdminUser.UserRoles.Add(new UserRole { User = superAdminUser, Role = superAdmin });
            adminUser.UserRoles.Add(new UserRole { User = adminUser, Role = admin });
            reportUser.UserRoles.Add(new UserRole { User = reportUser, Role = report });
            normalUser.UserRoles.Add(new UserRole { User = normalUser, Role = normal });

            Users = new List<User> { superAdminUser, adminUser, reportUser, normalUser };

            RefreshTokens = new List<RefreshToken>
            {
                new RefreshToken
                {
                    Id = Guid.Parse("ED124716-61DB-4FA9-5B00-08D8ACA5C10C"),
                    User = superAdminUser,
                    UserId = Guid.Parse("77d8500b-dd97-4b6d-ce43-18d8aa3916b9"),
                    Token = "token1",
                    Expires = DateTime.Now.AddHours(2),
                    Created = DateTime.Now,
                    CreatedByIp = "0.0.0.1",
                },
                new RefreshToken
                {
                    Id = Guid.Parse("87555B8E-EE09-4DBE-5B02-08D8ACA5C10C"),
                    User = adminUser,
                    UserId = Guid.Parse("77d8500b-dd97-4b6d-ce43-28d8aa3916b9"),
                    Token = "token2",
                    Expires = DateTime.Now.AddDays(-10).AddHours(2),
                    Created = DateTime.Now.AddDays(-10),
                    CreatedByIp = "0.0.0.1",
                },
                new RefreshToken
                {
                    Id = Guid.Parse("FC1779B6-5BC5-431E-B803-08D8AED9DA7D"),
                    User = normalUser,
                    UserId = Guid.Parse("77d8500b-dd97-4b6d-ce43-08d8aa391612"),
                    Token = "token3",
                    Expires = DateTime.Now.AddHours(2),
                    Created = DateTime.Now,
                    CreatedByIp = "0.0.0.1",
                },
                new RefreshToken
                {
                    Id = Guid.Parse("FC1779B6-5BC5-431E-B803-08D8AED9DA7D"),
                    UserId = Guid.Parse("77d8500b-dd97-4b6d-9e43-48d8aa3916b9"),
                    Token = "token4",
                    Expires = DateTime.Now.AddHours(2),
                    Created = DateTime.Now,
                    CreatedByIp = "0.0.0.1",
                }
            };

            RevokeDto = new RevokeDto
            {
                RefreshToken = "token1"
            };
        }

        static Mock<UserManager<TIDentityUser>> GetUserManagerMock<TIDentityUser>() where TIDentityUser : IdentityUser<Guid> => new(
                    new Mock<IUserStore<TIDentityUser>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<TIDentityUser>>().Object,
                    Array.Empty<IUserValidator<TIDentityUser>>(),
                    Array.Empty<IPasswordValidator<TIDentityUser>>(),
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<TIDentityUser>>>().Object);

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
                    MockJwtTokenService = null;
                    MockUserAccessorService = null;
                    MockUserManager = null;
                }

                _disposed = true;
            }
        }
    }
}
