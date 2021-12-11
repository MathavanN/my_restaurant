using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Api.PolicyHandlers;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;

namespace MyRestaurant.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    public class AccountController : BaseApiController
    {
        private readonly IAccountRepository _repository;
        public AccountController(IAccountRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("RegisterAdminUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(ApplicationClaimPolicy.SuperAdminOnly)]
        public async Task<IActionResult> RegisterAdmin(RegisterAdminDto registerDto)
        {
            var result = await _repository.RegisterAdminAsync(registerDto);
            return Ok(result);
        }

        [HttpPost("RegisterNormalUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(ApplicationClaimPolicy.AdminOnly)]
        public async Task<IActionResult> RegisterNormalUser(RegisterNormalDto registerDto)
        {
            var result = await _repository.RegisterNormalAsync(registerDto);
            return Ok(result);
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _repository.LoginAsync(loginDto, IpAddress());
            return Ok(result);
        }

        [HttpPost("Refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh(RefreshDto refreshDto)
        {
            var result = await _repository.RefreshToken(refreshDto, IpAddress());
            return Ok(result);
        }

        [HttpPost("Revoke")]
        [Authorize(ApplicationClaimPolicy.AdminOnly)]
        public async Task<IActionResult> Revoke(RevokeDto revokeDto)
        {
            await _repository.RevokeToken(revokeDto, IpAddress());

            return NoContent();
        }

        [HttpGet("CurrentUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCurrentUser()
        {
            var currentUser = _repository.GetCurrentUser();
            return Ok(currentUser);
        }

        [HttpGet("Users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _repository.GetUsersAsync();
            return Ok(result);
        }

        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
        }
    }
}
