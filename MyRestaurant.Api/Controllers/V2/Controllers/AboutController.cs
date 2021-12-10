using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Api.PolicyHandlers;

namespace MyRestaurant.Api.Controllers.V2
{
    [ApiVersion("2.0")]
    public class AboutController : BaseApiController
    {
        public AboutController()
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get() => Ok(new { data = "This is About Details v2" });

        [HttpGet("SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(ApplicationClaimPolicy.SuperAdminOnly)]
        public IActionResult GetSuperAdmin() => Ok(new { data = "This is About Details v2 Super Admin" });

        [HttpGet("Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(ApplicationClaimPolicy.AdminOnly)]
        public IActionResult GetAdmin() => Ok(new { data = "This is About Details v2 Admin" });

        [HttpGet("AdminSpecefic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(ApplicationClaimPolicy.IsSpecificAdminOnly)]
        public IActionResult GetAdminSpecific() => Ok(new { data = "This is About Details v2 Admin Specific" });

        [HttpGet("Normal")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(ApplicationClaimPolicy.NormalOnly)]
        public IActionResult GetNormal() => Ok(new { data = "This is About Details v2 Normal" });

        [HttpGet("Report")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(ApplicationClaimPolicy.ReportOnly)]
        public IActionResult GetReport() => Ok(new { data = "This is About Details v2 Report" });
    }
}
