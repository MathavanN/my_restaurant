using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyRestaurant.Api.Controllers.V1.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    public class AboutController : BaseController
    {
        public AboutController()
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get() => Ok(new { data = "This is About Details v1" });
    }
}
