using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Errors;
using MyRestaurant.Business.Repositories.Contracts;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _token;
        private readonly IUserAccessorService _userAccessor;
        private readonly UserManager<User> _userManager;
        public AccountRepository(IMapper mapper, UserManager<User> userManager, IJwtTokenService token, IUserAccessorService userAccessor)
        {
            _mapper = mapper;
            _userManager = userManager;
            _token = token;
            _userAccessor = userAccessor;
        }
        public CurrentUserDto GetCurrentUser()
        {
            var currentUser =  _userAccessor.GetCurrentUser();
            return _mapper.Map<CurrentUserDto>(currentUser);
        }

        public async Task<IEnumerable<GetUserDto>> GetUsersAsync()
        {
            var users = await Task.FromResult(_userManager.Users.ToList());

            return _mapper.Map<IEnumerable<GetUserDto>>(users);
        }

        public async Task<RegisterResultDto> RegisterAdminAsync(RegisterAdminDto registerDto)
        {
            var dbUser = await _userManager.FindByNameAsync(registerDto.Email);
            if (dbUser != null)
                throw new RestException(HttpStatusCode.Conflict, $"Email {registerDto.Email} is already registered.");

            var user = _mapper.Map<User>(registerDto);

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, new List<string> { Roles.Admin.ToString() });

            return new RegisterResultDto
            {
                Status = result.Succeeded == true ? "Success" : "Failed",
                Message = result.Succeeded == true ? "User created successfully, grant Admin access." : $"Failed to create new user. Error: ({string.Join(", ", result.Errors.Select(x => x.Description))})"
            };
        }

        public async Task<RegisterResultDto> RegisterNormalAsync(RegisterNormalDto registerDto)
        {
            var dbUser = await _userManager.FindByNameAsync(registerDto.Email);
            if (dbUser != null)
                throw new RestException(HttpStatusCode.Conflict, $"Email {registerDto.Email } is already registered.");

            var user = _mapper.Map<User>(registerDto);

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, registerDto.Roles);

            return new RegisterResultDto
            {
                Status = result.Succeeded == true ? "Success" : "Failed",
                Message = result.Succeeded == true ? $"User created successfully, grant {string.Join(", ", registerDto.Roles)} access." : $"Failed to create new user. Error: ({string.Join(", ", result.Errors.Select(x => x.Description))})"
            };
        }

        public async Task<TokenResultDto> LoginAsync(LoginDto loginDto, string ipAddress)
        {
            var dbUser = await _userManager.FindByNameAsync(loginDto.Email);
            if (dbUser == null)
                throw new RestException(HttpStatusCode.Unauthorized, "Username or password is incorrect.");

            ////if(!dbUser.EmailConfirmed)
            ////    throw new RestException(HttpStatusCode.Forbidden, $"Email is not confirmed for {loginDto.Email }.");

            var result = await _userManager.CheckPasswordAsync(dbUser, loginDto.Password);
            if (!result)
                throw new RestException(HttpStatusCode.Unauthorized, "Username or password is incorrect.");

            var accessToken = await _token.GenerateAccessToken(dbUser);
            var refreshToken = await _token.GenerateRefreshToken(dbUser.Id, ipAddress);

            return new TokenResultDto { AccessToken = accessToken, RefreshToken = refreshToken.Token };
        }
        
        public async Task RevokeToken(RevokeDto revokeDto, string ipAddress)
        {
            var refreshToken = await _token.GetRefreshTokenAsync(e => e.Token == revokeDto.RefreshToken);

            if (refreshToken == null)
                throw new RestException(HttpStatusCode.NotFound, "RefreshToken not found.");

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            
            await _token.UpdateRefreshTokenAsync(refreshToken);
        }

        public async Task<TokenResultDto> RefreshToken(RefreshDto refreshDto, string ipAddress)
        {
            var isValidRefreshToken = _token.ValidateRefreshToken(refreshDto.RefreshToken);

            if (!isValidRefreshToken)
                throw new RestException(HttpStatusCode.Unauthorized, "Invalid Refresh token.");

            var dbRefreshToken = await _token.GetRefreshTokenAsync(e => e.Token == refreshDto.RefreshToken);

            if (dbRefreshToken == null)
                throw new RestException(HttpStatusCode.NotFound, "RefreshToken not found.");

            if(!dbRefreshToken.IsActive)
                throw new RestException(HttpStatusCode.BadRequest, "RefreshToken revoked by admin.");

            var dbUser = await _userManager.FindByIdAsync(dbRefreshToken.UserId.ToString());
            if (dbUser == null)
                throw new RestException(HttpStatusCode.NotFound, $"User associated for this token not found.");

            //Create new AccessToken and RefreshToken
            var accessToken = await _token.GenerateAccessToken(dbUser);
            var refreshToken = await _token.GenerateRefreshToken(dbUser.Id, ipAddress);

            //Revoke the previous access token
            dbRefreshToken.Revoked = DateTime.UtcNow;
            dbRefreshToken.RevokedByIp = ipAddress;
            dbRefreshToken.ReplacedByToken = refreshToken.Token;
            await _token.UpdateRefreshTokenAsync(dbRefreshToken);

            return new TokenResultDto { AccessToken = accessToken, RefreshToken = refreshToken.Token };
        }
    }
}
