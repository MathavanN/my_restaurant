using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    public class GoodsReceivedNoteFreeItemController : BaseApiController
    {
        private readonly IGoodsReceivedNoteFreeItemRepository _repository;
        public GoodsReceivedNoteFreeItemController(IGoodsReceivedNoteFreeItemRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGoodsReceivedNoteFreeItems(long goodsReceivedNoteId)
        {
            var result = await _repository.GetGoodsReceivedNoteFreeItemsAsync(goodsReceivedNoteId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGoodsReceivedNoteFreeItem(long id)
        {
            var result = await _repository.GetGoodsReceivedNoteFreeItemAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateGoodsReceivedNoteFreeItem(CreateGoodsReceivedNoteFreeItemDto goodsReceivedNoteFreeItemDto, ApiVersion version)
        {
            var result = await _repository.CreateGoodsReceivedNoteFreeItemAsync(goodsReceivedNoteFreeItemDto);
            return CreatedAtRoute(new { id = result.Id, version = $"{ version}" }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateGoodsReceivedNoteFreeItem(long id, EditGoodsReceivedNoteFreeItemDto goodsReceivedNoteFreeItemDto)
        {
            var result = await _repository.UpdateGoodsReceivedNoteFreeItemAsync(id, goodsReceivedNoteFreeItemDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGoodsReceivedNoteFreeItem(long id)
        {
            await _repository.DeleteGoodsReceivedNoteFreeItemAsync(id);
            return NoContent();
        }
    }
}
