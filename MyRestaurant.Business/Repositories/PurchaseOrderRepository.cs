using AutoMapper;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Errors;
using MyRestaurant.Business.Repositories.Contracts;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly IMapper _mapper;
        private readonly IPurchaseOrderServices _purchaseOrder;
        private readonly IUserAccessorService _userAccessor;
        public PurchaseOrderRepository(IMapper mapper, IPurchaseOrderServices purchaseOrder, IUserAccessorService userAccessor)
        {
            _mapper = mapper;
            _purchaseOrder = purchaseOrder;
            _userAccessor = userAccessor;
        }

        public async Task<GetPurchaseOrderDto> CreatePurchaseOrderAsync(CreatePurchaseOrderDto purchaseOrderDto)
        {
            var currentUser = _userAccessor.GetCurrentUser();
            var purchaseOrder = _mapper.Map<PurchaseOrder>(purchaseOrderDto);
            var currentDate = DateTime.Now;
            purchaseOrder.OrderNumber = $"PO_{currentDate.ToString("yyyyMMdd")}_{currentDate.Ticks.ToString("x")}";
            purchaseOrder.RequestedBy = currentUser.UserId;
            purchaseOrder.RequestedDate = currentDate;
            purchaseOrder.ApprovalStatus = PurchaseOrderStatus.Pending;

            await _purchaseOrder.AddPurchaseOrderAsync(purchaseOrder);

            return _mapper.Map<GetPurchaseOrderDto>(purchaseOrder);
        }

        private async Task<PurchaseOrder> GetPurchaseOrderById(long id)
        {
            var order = await _purchaseOrder.GetPurchaseOrderAsync(d => d.Id == id);

            if (order == null)
                throw new RestException(HttpStatusCode.NotFound, "Purchase order not found.");

            return order;
        }
        
        public async Task<GetPurchaseOrderDto> GetPurchaseOrderAsync(long id)
        {
            var order = await GetPurchaseOrderById(id);

            return _mapper.Map<GetPurchaseOrderDto>(order);
        }
        
        public async Task<IEnumerable<GetPurchaseOrderDto>> GetPurchaseOrdersAsync()
        {
            var orders = await _purchaseOrder.GetPurchaseOrdersAsync();

            return _mapper.Map<IEnumerable<GetPurchaseOrderDto>>(orders);
        }
    }
}
