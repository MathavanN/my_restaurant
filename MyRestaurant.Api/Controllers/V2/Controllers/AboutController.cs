using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyRestaurant.Api.Controllers.V2
{
    [ApiVersion("2.0")]
    public class AboutController : BaseApiController<AboutController>
    {
        public AboutController()
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get() => Ok(new { data = "This is About Details v2" });
    }
}
