using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    public class RestaurantInfoController : BaseApiController
    {
        private readonly IRestaurantInfoRepository _repository;
        public RestaurantInfoController(IRestaurantInfoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRestaurantInfos()
        {
            var result = await _repository.GetRestaurantInfosAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRestaurantInfo(int id)
        {
            var result = await _repository.GetRestaurantInfoAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRestaurantInfo(CreateRestaurantInfoDto restaurantInfoDto, ApiVersion version)
        {
            var result = await _repository.CreateRestaurantInfoAsync(restaurantInfoDto);
            return CreatedAtRoute(new { id = result.Id, version = $"{version}" }, result);
        }
    }
}
