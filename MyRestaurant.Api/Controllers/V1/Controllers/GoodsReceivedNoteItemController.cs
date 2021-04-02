using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    public class GoodsReceivedNoteItemController : BaseApiController<GoodsReceivedNoteItemController>
    {
        private readonly IGoodsReceivedNoteItemRepository _repository;
        public GoodsReceivedNoteItemController(IGoodsReceivedNoteItemRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGoodsReceivedNoteItems(long goodsReceivedNoteId)
        {
            var result = await _repository.GetGoodsReceivedNoteItemsAsync(goodsReceivedNoteId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGoodsReceivedNoteItem(long id)
        {
            var result = await _repository.GetGoodsReceivedNoteItemAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateGoodsReceivedNoteItem(CreateGoodsReceivedNoteItemDto goodsReceivedNoteItemDto, ApiVersion version)
        {
            var result = await _repository.CreateGoodsReceivedNoteItemAsync(goodsReceivedNoteItemDto);
            return CreatedAtRoute(new { id = result.Id, version = $"{ version}" }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateGoodsReceivedNoteItem(long id, EditGoodsReceivedNoteItemDto goodsReceivedNoteItemDto)
        {
            var result = await _repository.UpdateGoodsReceivedNoteItemAsync(id, goodsReceivedNoteItemDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGoodsReceivedNoteItem(long id)
        {
            await _repository.DeleteGoodsReceivedNoteItemAsync(id);
            return NoContent();
        }
    }
}
