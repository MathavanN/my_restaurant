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
    public class GoodsReceivedNoteRepository : IGoodsReceivedNoteRepository
    {
        private readonly IMapper _mapper;
        private readonly IGoodsReceivedNoteService _goodReceivedNote;
        private readonly IPurchaseOrderItemService _purchaseOrderItem;
        private readonly IGoodsReceivedNoteItemSevice _goodsReceivedNoteItem;
        private readonly IUserAccessorService _userAccessor;
        public GoodsReceivedNoteRepository(IMapper mapper, IGoodsReceivedNoteService goodReceivedNote, IUserAccessorService userAccessor, 
            IPurchaseOrderItemService purchaseOrderItem, IGoodsReceivedNoteItemSevice goodsReceivedNoteItem)
        {
            _mapper = mapper;
            _goodReceivedNote = goodReceivedNote;
            _purchaseOrderItem = purchaseOrderItem;
            _goodsReceivedNoteItem = goodsReceivedNoteItem;
            _userAccessor = userAccessor;
        }

        public async Task<GetGoodsReceivedNoteDto> CreateGoodsReceivedNoteAsync(CreateGoodsReceivedNoteDto goodsReceivedNoteDto)
        {
            var currentUser = _userAccessor.GetCurrentUser();
            var goodsReceivedNote = _mapper.Map<GoodsReceivedNote>(goodsReceivedNoteDto);
            goodsReceivedNote.CreatedBy = currentUser.UserId;
            goodsReceivedNote.CreatedDate = DateTime.Now;
            goodsReceivedNote.ApprovalStatus = Status.Pending;

            await _goodReceivedNote.AddGoodsReceivedNoteAsync(goodsReceivedNote);

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
            var goodsReceiveNote = await GetGoodsReceivedNoteById(id);

            return _mapper.Map<GetGoodsReceivedNoteDto>(goodsReceiveNote);
        }

        public async Task<IEnumerable<GetGoodsReceivedNoteDto>> GetGoodsReceivedNotesAsync()
        {
            var goodsReceiveNotes = await _goodReceivedNote.GetGoodsReceivedNotesAsync();

            return _mapper.Map<IEnumerable<GetGoodsReceivedNoteDto>>(goodsReceiveNotes);
        }

        public async Task UpdateGoodsReceivedNoteAsync(long id, EditGoodsReceivedNoteDto goodsReceivedNoteDto)
        {
            var goodsReceiveNote = await GetGoodsReceivedNoteById(id);

            goodsReceiveNote = _mapper.Map(goodsReceivedNoteDto, goodsReceiveNote);

            await _goodReceivedNote.UpdateGoodsReceivedNoteAsync(goodsReceiveNote);
        }
    }
}
