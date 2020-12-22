using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Business.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRestaurant.Api.Controllers.V1.Controllers
{
    [ApiVersion("1.0")]
    public class ServiceTypeController : BaseController
    {

        private readonly IServiceTypeRepository _serviceTypeRepository;
        public ServiceTypeController(IServiceTypeRepository serviceTypeRepository)
        {
            _serviceTypeRepository = serviceTypeRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetServiceTypes()
        {
            var result = await _serviceTypeRepository.GetServicesTypeAsync();
            return Ok(result);
        }
    }
}
