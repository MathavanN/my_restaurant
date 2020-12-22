﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyRestaurant.Api.Controllers.V2.Controllers
{
    [ApiVersion("2.0")]
    public class AboutController : BaseController
    {
        public AboutController()
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get() => Ok(new { data = "This is About Details v2" });
    }
}
