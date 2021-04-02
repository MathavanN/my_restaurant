using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    public class UnitOfMeasureController : BaseApiController<UnitOfMeasureController>
    {
        private readonly IUnitOfMeasureRepository _repository;
        public UnitOfMeasureController(IUnitOfMeasureRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUnitOfMeasures()
        {
            var result = await _repository.GetUnitOfMeasuresAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUnitOfMeasure(int id)
        {
            var result = await _repository.GetUnitOfMeasureAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUnitOfMeasure(CreateUnitOfMeasureDto unitOfMeasureDto, ApiVersion version)
        {
            var result = await _repository.CreateUnitOfMeasureAsync(unitOfMeasureDto);
            return CreatedAtRoute(new { id = result.Id, version = $"{version}" }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUnitOfMeasure(int id, EditUnitOfMeasureDto unitOfMeasureDto)
        {
            var result = await _repository.UpdateUnitOfMeasureAsync(id, unitOfMeasureDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUnitOfMeasure(int id)
        {
            await _repository.DeleteUnitOfMeasureAsync(id);
            return NoContent();
        }
    }
}
