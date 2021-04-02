using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    public class PurchaseOrderController : BaseApiController<PurchaseOrderController>
    {
        private readonly IPurchaseOrderRepository _repository;
        public PurchaseOrderController(IPurchaseOrderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPurchaseOrders()
        {
            var result = await _repository.GetPurchaseOrdersAsync();
            return Ok(result);
        }
        [HttpGet("grnAllowed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPurchaseOrdersForGRN()
        {
            var result = await _repository.GetPurchaseOrdersAllowToCreateGRN();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPurchaseOrder(long id)
        {
            var result = await _repository.GetPurchaseOrderAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePurchaseOrder(CreatePurchaseOrderDto purchaseOrderDto, ApiVersion version)
        {
            var result = await _repository.CreatePurchaseOrderAsync(purchaseOrderDto);
            return CreatedAtRoute(new { id = result.Id, version = $"{version}" }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePurchaseOrder(long id, EditPurchaseOrderDto purchaseOrderDto)
        {
            var result = await _repository.UpdatePurchaseOrderAsync(id, purchaseOrderDto);
            return Ok(result);
        }

        [HttpPut("approval/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ApprovePurchaseOrder(long id, ApprovalPurchaseOrderDto purchaseOrderDto)
        {
            var result = await _repository.ApprovalPurchaseOrderAsync(id, purchaseOrderDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePurchaseOrder(long id)
        {
            await _repository.DeletePurchaseOrderAsync(id);
            return NoContent();
        }
    }
}
