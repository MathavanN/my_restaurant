﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    public class PurchaseOrderItemController : BaseApiController<PurchaseOrderItemController>
    {
        private readonly IPurchaseOrderItemRepository _repository;
        public PurchaseOrderItemController(IPurchaseOrderItemRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPurchaseOrderItems(long orderId)
        {
            var result = await _repository.GetPurchaseOrderItemsAsync(orderId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPurchaseOrderItem(long id)
        {
            var result = await _repository.GetPurchaseOrderItemAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePurchaseOrderItem(CreatePurchaseOrderItemDto purchaseOrderDto, ApiVersion version)
        {
            var result = await _repository.CreatePurchaseOrderItemAsync(purchaseOrderDto);
            return CreatedAtRoute(new { id = result.Id, version = $"{ version}" }, result);
        }
    }
}