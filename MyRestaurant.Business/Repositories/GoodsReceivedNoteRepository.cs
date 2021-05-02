using AutoMapper;
using MyRestaurant.Business.Dtos.V1;
using MyRestaurant.Business.Errors;
using MyRestaurant.Business.Repositories.Common;
using MyRestaurant.Business.Repositories.Contracts;
using MyRestaurant.Models;
using MyRestaurant.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories
{
    public class GoodsReceivedNoteRepository : IGoodsReceivedNoteRepository
    {
        private readonly IMapper _mapper;
        private readonly IGoodsReceivedNoteService _goodReceivedNote;
        private readonly IPurchaseOrderService _purchaseOrder;
        private readonly IPurchaseOrderItemService _purchaseOrderItem;
        private readonly IGoodsReceivedNoteItemService _goodsReceivedNoteItem;
        private readonly IUserAccessorService _userAccessor;
        public GoodsReceivedNoteRepository(IMapper mapper, IGoodsReceivedNoteService goodReceivedNote,
            IUserAccessorService userAccessor, IPurchaseOrderService purchaseOrder,
            IPurchaseOrderItemService purchaseOrderItem, IGoodsReceivedNoteItemService goodsReceivedNoteItem)
        {
            _mapper = mapper;
            _purchaseOrder = purchaseOrder;
            _goodReceivedNote = goodReceivedNote;
            _purchaseOrderItem = purchaseOrderItem;
            _goodsReceivedNoteItem = goodsReceivedNoteItem;
            _userAccessor = userAccessor;
        }

        private PurchaseOrder CheckPurchaseOrderAllowedToCreateGRN(PurchaseOrder order)
        {
            var statusNoNeedNewGRN = new List<Status> { Status.Approved, Status.Pending };

            if (order.GoodsReceivedNotes.Any(d => statusNoNeedNewGRN.Contains(d.ApprovalStatus)))
                throw new RestException(HttpStatusCode.BadRequest, "GRN already created for this purchase order.");

            return order;
        }

        private async Task<PurchaseOrder> CheckPurchaseOrder(long purchaseOrderId)
        {
            var order = await _purchaseOrder.GetPurchaseOrderAsync(d => d.Id == purchaseOrderId);

            if (order == null)
                throw new RestException(HttpStatusCode.NotFound, "Purchase order not found.");

            if (order.ApprovalStatus != Status.Approved)
                throw new RestException(HttpStatusCode.BadRequest, "GRN can create for approved purchase order.");

            return order;
        }

        public async Task<GetGoodsReceivedNoteDto> CreateGoodsReceivedNoteAsync(CreateGoodsReceivedNoteDto goodsReceivedNoteDto)
        {
            //verify PO allowed to create GRN
            var order = await CheckPurchaseOrder(goodsReceivedNoteDto.PurchaseOrderId);
            CheckPurchaseOrderAllowedToCreateGRN(order);

            var currentUser = Helper.GetCurrentUser(_userAccessor);
            var goodsReceivedNote = _mapper.Map<GoodsReceivedNote>(goodsReceivedNoteDto);
            goodsReceivedNote.CreatedBy = currentUser.UserId;
            goodsReceivedNote.CreatedDate = DateTime.Now;
            goodsReceivedNote.ApprovalStatus = Status.Pending;

            goodsReceivedNote = await _goodReceivedNote.AddGoodsReceivedNoteAsync(goodsReceivedNote);

            var items = await _purchaseOrderItem.GetPurchaseOrderItemsAsync(e => e.PurchaseOrderId == goodsReceivedNoteDto.PurchaseOrderId);
            foreach (var item in items)
            {
                var grnItem = new GoodsReceivedNoteItem
                {
                    GoodsReceivedNoteId = goodsReceivedNote.Id,
                    ItemId = item.ItemId,
                    ItemUnitPrice = item.ItemUnitPrice,
                    Quantity = item.Quantity
                };
                await _goodsReceivedNoteItem.AddGoodsReceivedNoteItemAsync(grnItem);
            }

            return _mapper.Map<GetGoodsReceivedNoteDto>(goodsReceivedNote);
        }

        private async Task<GoodsReceivedNote> GetGoodsReceivedNoteById(long id)
        {
            var goodsReceiveNote = await _goodReceivedNote.GetGoodsReceivedNoteAsync(d => d.Id == id);

            if (goodsReceiveNote == null)
                throw new RestException(HttpStatusCode.NotFound, "Goods received note not found.");

            return goodsReceiveNote;
        }
        public async Task DeleteGoodsReceivedNoteAsync(long id)
        {
            var goodsReceiveNote = await GetGoodsReceivedNoteById(id);

            await _goodReceivedNote.DeleteGoodsReceivedNoteAsync(goodsReceiveNote);
        }

        public async Task<GetGoodsReceivedNoteDto> GetGoodsReceivedNoteAsync(long id)
        {
            var goodsReceivedNote = await GetGoodsReceivedNoteById(id);

            return _mapper.Map<GetGoodsReceivedNoteDto>(goodsReceivedNote);
        }

        public async Task<IEnumerable<GetGoodsReceivedNoteDto>> GetGoodsReceivedNotesAsync()
        {
            var goodsReceivedNotes = await _goodReceivedNote.GetGoodsReceivedNotesAsync();

            return _mapper.Map<IEnumerable<GetGoodsReceivedNoteDto>>(goodsReceivedNotes);
        }

        public async Task<GetGoodsReceivedNoteDto> UpdateGoodsReceivedNoteAsync(long id, EditGoodsReceivedNoteDto goodsReceivedNoteDto)
        {
            var goodsReceivedNote = await GetGoodsReceivedNoteById(id);

            //verify PO allowed to EDIT GRN
            await CheckPurchaseOrder(goodsReceivedNoteDto.PurchaseOrderId);

            goodsReceivedNote = _mapper.Map(goodsReceivedNoteDto, goodsReceivedNote);

            await _goodReceivedNote.UpdateGoodsReceivedNoteAsync(goodsReceivedNote);

            return _mapper.Map<GetGoodsReceivedNoteDto>(goodsReceivedNote);
        }

        public async Task<GetGoodsReceivedNoteDto> ApprovalGoodsReceivedNoteAsync(long id, ApprovalGoodsReceivedNoteDto goodsReceivedNoteDto)
        {
            var goodsReceivedNote = await GetGoodsReceivedNoteById(id);
            goodsReceivedNote = _mapper.Map(goodsReceivedNoteDto, goodsReceivedNote);

            var currentUser = Helper.GetCurrentUser(_userAccessor);
            goodsReceivedNote.ApprovedBy = currentUser.UserId;
            goodsReceivedNote.ApprovedDate = DateTime.Now;

            await _goodReceivedNote.UpdateGoodsReceivedNoteAsync(goodsReceivedNote);

            return _mapper.Map<GetGoodsReceivedNoteDto>(goodsReceivedNote);
        }
    }
}
