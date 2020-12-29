using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System.Threading.Tasks;

namespace MyRestaurant.Api.Controllers.V1.Controllers
{
    [ApiVersion("1.0")]
    public class AccountController : BaseApiController<AccountController>
    {
        private readonly IAccountRepository _repository;
        public AccountController(IAccountRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("RegisterAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _repository.RegisterAdminAsync(registerDto);
            return Ok(result);
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _repository.LoginAsync(loginDto, IpAddress());
            return Ok(result);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh(LoginDto loginDto)
        {
            //var result = await _repository.LoginAsync(loginDto);
            return Ok();
        }

        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

    }
}
