using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using MyRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace MyRestaurant.Services.Tests
{
    public class JwtTokenServiceFixture : IDisposable
    {
        private bool _disposed;
        public Mock<UserManager<User>> MockUserManager { get; private set; }
        public IOptions<JwtSettings> JwtSettings { get; private set; }
        public IList<Claim> Claims { get; private set; }

        public JwtTokenServiceFixture()
        {
            MockUserManager = GetUserManagerMock<User>();
            JwtSettings = Options.Create(new JwtSettings
            {
                AccessTokenSecret = "f801099f665327dcdf99cd88868efc409522170d5d209233a0241edbc7a01ec5e73ec1ed9294e35220739ab8501a59e8a220aa8fd0196bf91427be725224b3ebb0278c214fb91add0ee4cdf4062aedd352e32d24df613d7d6b20378db84a8d3258e16a35004558e9f6eea09e95507b6cc59d128cfbe775ec621a18fb1cf9dca19a2dd372f1cab04469b4b2622c418bbc1b5eefbe79f3bf694102cbc98414a68931ab6852a149ad5f612e69b9c9264a5d6bdb2f161403ce920981dd09d773796eebeb0c33c56574e72f508d0d5d7c4d608ee725a509e0bb057e6d66c86325f4e534a6d84f38f1dd65683533a7659b8f0fde123a597e13a426ccc49a500bf8805572c54b56e06d47b9ac94928a10e9f8511d831ce9a1caf2d859c8b5552bcf0d5c1f793ff9c505bdc4320563fd119fb285a72457e501a061d4716574b8d24c6e28e21e0c8a7db244ab05c90f1653e4ef20b17d8ca05600c40213fbcaecc0abf87744730fcaed0d1204a554edced7eea7d6bb8c4068063550a2c867a53f3484e79d80db4fe83769ec2b74353b658dd28dcb2295a283956590f74e0ba2db1899bacb9fa5a857100e5ff202b3cd7deafb1074db6fcc335104be6be4ce87cad42460e10cb6e8de9439ad0a40fd7a9e0077927d918d32e34e501cfa94e2ce861a596a959261ceca2825df3106ada34c1d7b789af3ed4eca32eda96159e4d0b38c273691",
                RefreshTokenSecret = "f801099f665327dcdf99cd88868efc409522170d5d209233a0241edbc7a01ec5e73ec1ed9294e35220739ab8501a59e8a220aa8fd0196bf91427be725224b3ebb0278c214fb91add0ee4cdf4062aedd352e32d24df613d7d6b20378db84a8d3258e16a35004558e9f6eea09e95507b6cc59d128cfbe775ec621a18fb1cf9dca19a2dd372f1cab04469b4b2622c418bbc1b5eefbe79f3bf694102cbc98414a68931ab6852a149ad5f612e69b9c9264a5d6bdb2f161403ce920981dd09d773796eebeb0c33c56574e72f508d0d5d7c4d608ee725a509e0bb057e6d66c86325f4e534a6d84f38f1dd65683533a7659b8f0fde123a597e13a426ccc49a500bf8805572c54b56e06d47b9ac94928a10e9f8511d831ce9a1caf2d859c8b5552bcf0d5c1f793ff9c505bdc4320563fd119fb285a72457e501a061d4716574b8d24c6e28e21e0c8a7db244ab05c90f1653e4ef20b17d8ca05600c40213fbcaecc0abf87744730fcaed0d1204a554edced7eea7d6bb8c4068063550a2c867a53f3484e79d80db4fe83769ec2b74353b658dd28dcb2295a283956590f74e0ba2db1899bacb9fa5a857100e5ff202b3cd7deafb1074db6fcc335104be6be4ce87cad42460e10cb6e8de9439ad0a40fd7a9e0077927d918d32e34e501cfa94e2ce861a596a959261ceca2825df3106ada34c1d7b789af3ed4eca32eda96159e4d0b38c273691",
                Issuer = "https://localhost:44301",
                Audience = "https://localhost:44301",
                AccessTokenExpirationInMinutes = 60,
                RefreshTokenExpirationInMinutes = 43200
            });

            Claims = new List<Claim> { };
        }
        static Mock<UserManager<TIDentityUser>> GetUserManagerMock<TIDentityUser>() where TIDentityUser : IdentityUser<Guid>
        {
            return new Mock<UserManager<TIDentityUser>>(
                    new Mock<IUserStore<TIDentityUser>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<TIDentityUser>>().Object,
                    Array.Empty<IUserValidator<TIDentityUser>>(),
                    Array.Empty<IPasswordValidator<TIDentityUser>>(),
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<TIDentityUser>>>().Object);
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
                    MockUserManager = null;
                    JwtSettings = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
                }

                _disposed = true;
            }
        }
    }
}
