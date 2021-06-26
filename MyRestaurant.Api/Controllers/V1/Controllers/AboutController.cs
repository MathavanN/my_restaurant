using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyRestaurant.Api.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = true)]
    public class AboutController : BaseApiController
    {
        public AboutController()
        {
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get() => Ok(new { data = "This is About Details v1" });
    }
}
