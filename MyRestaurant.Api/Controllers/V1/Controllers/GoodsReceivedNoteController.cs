using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Api.PolicyHandlers;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    public class GoodsReceivedNoteController : BaseApiController
    {
        private readonly IGoodsReceivedNoteRepository _repository;
        public GoodsReceivedNoteController(IGoodsReceivedNoteRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGoodsReceivedNotes()
        {
            var result = await _repository.GetGoodsReceivedNotesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGoodsReceivedNote(long id)
        {
            var result = await _repository.GetGoodsReceivedNoteAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateGoodsReceivedNote(CreateGoodsReceivedNoteDto goodsReceivedNoteDto, ApiVersion version)
        {
            var result = await _repository.CreateGoodsReceivedNoteAsync(goodsReceivedNoteDto);
            return CreatedAtRoute(new { id = result.Id, version = $"{version}" }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        [Authorize(ApplicationClaimPolicy.SuperAdminOnly)]
        public async Task<IActionResult> UpdateGoodsReceivedNote(long id, EditGoodsReceivedNoteDto goodsReceivedNoteDto)
        {
            var result = await _repository.UpdateGoodsReceivedNoteAsync(id, goodsReceivedNoteDto);
            return Ok(result);
        }

        [HttpPut("approval/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ApproveGoodsReceivedNote(long id, ApprovalGoodsReceivedNoteDto goodsReceivedNoteDto)
        {
            var result = await _repository.ApprovalGoodsReceivedNoteAsync(id, goodsReceivedNoteDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGoodsReceivedNote(long id)
        {
            await _repository.DeleteGoodsReceivedNoteAsync(id);
            return NoContent();
        }
    }
}
