using Microsoft.AspNetCore.Mvc;

namespace MyRestaurant.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {
        public BaseController()
        {

        }
    }
}
