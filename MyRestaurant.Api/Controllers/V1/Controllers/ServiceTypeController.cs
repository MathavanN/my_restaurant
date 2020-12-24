using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Business.Dtos.V1.ServiceTypeDtos;
using MyRestaurant.Business.Repositories.Contracts;
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
            var result = await _serviceTypeRepository.GetServicesTypesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetServiceType(int id)
        {
            var result = await _serviceTypeRepository.GetServiceTypeAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateServiceType(CreateServiceTypeDto serviceTypeDto, ApiVersion version)
        {
            var result = await _serviceTypeRepository.CreateServiceTypeAsync(serviceTypeDto);
            return CreatedAtRoute(new { id = result.Id, version = $"{version}" }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateServiceType(int id,  EditServiceTypeDto serviceTypeDto) 
        {
            await _serviceTypeRepository.UpdateServiceTypeAsync(id, serviceTypeDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteServiceType(int id)
        {
            await _serviceTypeRepository.DeleteServiceTypeAsync(id);

            return NoContent();
        }
    }
}
