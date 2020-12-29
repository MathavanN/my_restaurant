using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Errors;
using MyRestaurant.Business.Repositories.Contracts;
using MyRestaurant.Models;
using MyRestaurant.Services.Account;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _account;
        private readonly UserManager<User> _userManager;
        public AccountRepository(IMapper mapper, UserManager<User> userManager, IAccountService account)
        {
            _mapper = mapper;
            _userManager = userManager;
            _account = account;
        }
        public async Task<RegisterResultDto> RegisterAdminAsync(RegisterDto registerDto)
        {
            var dbUser = await _userManager.FindByNameAsync(registerDto.Email);
            if (dbUser != null)
                throw new RestException(HttpStatusCode.Conflict, $"Email {registerDto.Email } is already registered.");

            var user = _mapper.Map<User>(registerDto);

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, new List<string> { Roles.Admin.ToString() });


            return new RegisterResultDto
            {
                Status = result.Succeeded == true ? "Success" : "Failed",
                Message = result.Succeeded == true ? "User created successfully, grant Admin access." : $"Failed to create new user. {result.Errors}"
            };
        }

        public async Task<TokenResultDto> LoginAsync(LoginDto loginDto, string ipAddress)
        {
            var dbUser = await _userManager.FindByNameAsync(loginDto.Email);
            if (dbUser == null)
                throw new RestException(HttpStatusCode.NotFound, $"No accounts registered with email {loginDto.Email }.");

            ////if(!dbUser.EmailConfirmed)
            ////    throw new RestException(HttpStatusCode.Forbidden, $"Email is not confirmed for {loginDto.Email }.");

            var result = await _userManager.CheckPasswordAsync(dbUser, loginDto.Password);
            if (!result)
                throw new RestException(HttpStatusCode.BadRequest, "Username or password is incorrect.");

            var accessToken = await _account.GenerateAccessToken(dbUser);
            var refreshToken = await _account.GenerateRefreshToken(dbUser.Id, ipAddress);

            return new TokenResultDto { AccessToken = accessToken, RefreshToken = refreshToken.Token };
        }
    }
}
