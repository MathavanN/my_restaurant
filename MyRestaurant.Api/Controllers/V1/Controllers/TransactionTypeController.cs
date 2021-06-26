using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    public class TransactionTypeController : BaseApiController
    {
        private readonly ITransactionTypeRepository _repository;
        public TransactionTypeController(ITransactionTypeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactionTypes()
        {
            var result = await _repository.GetTransactionTypesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTransactionType(int id)
        {
            var result = await _repository.GetTransactionTypeAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTransactionType(CreateTransactionTypeDto transactionTypeDto, ApiVersion version)
        {
            var result = await _repository.CreateTransactionTypeAsync(transactionTypeDto);
            return CreatedAtRoute(new { id = result.Id, version = $"{version}" }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTransactionType(int id, EditTransactionTypeDto transactionTypeDto)
        {
            var result = await _repository.UpdateTransactionTypeAsync(id, transactionTypeDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTransactionType(int id)
        {
            await _repository.DeleteTransactionTypeAsync(id);
            return NoContent();
        }
    }
}
