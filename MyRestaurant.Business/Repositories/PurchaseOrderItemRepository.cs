using AutoMapper;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Errors;
using MyRestaurant.Business.Repositories.Contracts;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories
{
    public class PurchaseOrderItemRepository : IPurchaseOrderItemRepository {
        private readonly IMapper _mapper;
        private readonly IPurchaseOrderItemService _purchaseOrderItem;
        public PurchaseOrderItemRepository(IMapper mapper, IPurchaseOrderItemService purchaseOrderItem)
        {
            _mapper = mapper;
            _purchaseOrderItem = purchaseOrderItem;
        }
        private async Task CheckOrderItemAsync(long id, long orderId, long itemId)
        {
            var dbStockType = await _purchaseOrderItem.GetPurchaseOrderItemAsync(d => d.ItemId == itemId && d.PurchaseOrderId == orderId && d.Id != id);
            if (dbStockType != null)
                throw new RestException(HttpStatusCode.Conflict, $"Item already available for this purchase request.");
        }
        private async Task<PurchaseOrderItem> GetPurchaseOrderItemById(long id)
        {
            var item = await _purchaseOrderItem.GetPurchaseOrderItemAsync(d => d.Id == id);

            if (item == null)
                throw new RestException(HttpStatusCode.NotFound, "Purchase order item not found.");

            return item;
        }

        public async Task<GetPurchaseOrderItemDto> CreatePurchaseOrderItemAsync(CreatePurchaseOrderItemDto purchaseOrderItemDto)
        {
            await CheckOrderItemAsync(0, purchaseOrderItemDto.PurchaseOrderId, purchaseOrderItemDto.ItemId);

            var item = _mapper.Map<PurchaseOrderItem>(purchaseOrderItemDto);
            await _purchaseOrderItem.AddPurchaseOrderItemAsync(item);

            return _mapper.Map<GetPurchaseOrderItemDto>(item);
        }

        public async Task<GetPurchaseOrderItemDto> GetPurchaseOrderItemAsync(long id)
        {
            var item = await GetPurchaseOrderItemById(id);

            return _mapper.Map<GetPurchaseOrderItemDto>(item);
        }

        public async Task<IEnumerable<GetPurchaseOrderItemDto>> GetPurchaseOrderItemsAsync(long orderId)
        {
            var items = await _purchaseOrderItem.GetPurchaseOrderItemsAsync(d => d.PurchaseOrderId == orderId);

            return _mapper.Map<IEnumerable<GetPurchaseOrderItemDto>>(items);
        }

        public async Task UpdatePurchaseOrderItemAsync(long id, EditPurchaseOrderItemDto purchaseOrderItemDto)
        {
            await CheckOrderItemAsync(id, purchaseOrderItemDto.PurchaseOrderId, purchaseOrderItemDto.ItemId);

            var item = await GetPurchaseOrderItemById(id);

            item = _mapper.Map(purchaseOrderItemDto, item);

            await _purchaseOrderItem.UpdatePurchaseOrderItemAsync(item);
        }

        public async Task DeletePurchaseOrderItemAsync(long id)
        {
            var item = await GetPurchaseOrderItemById(id);

            await _purchaseOrderItem.DeletePurchaseOrderItemAsync(item);
        }
    }
}
