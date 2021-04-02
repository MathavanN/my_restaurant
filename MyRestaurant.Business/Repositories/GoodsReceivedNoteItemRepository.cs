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
    public class GoodsReceivedNoteItemRepository : IGoodsReceivedNoteItemRepository
    {
        private readonly IMapper _mapper;
        private readonly IGoodsReceivedNoteItemService _goodsReceivedNoteItem;
        public GoodsReceivedNoteItemRepository(IMapper mapper, IGoodsReceivedNoteItemService goodsReceivedNoteItem)
        {
            _mapper = mapper;
            _goodsReceivedNoteItem = goodsReceivedNoteItem;
        }

        private async Task CheckGoodsReceivedNoteItemAsync(long id, long goodsReceivedNoteId, long itemId)
        {
            var dbItem = await _goodsReceivedNoteItem.GetGoodsReceivedNoteItemAsync(d => d.ItemId == itemId && d.GoodsReceivedNoteId == goodsReceivedNoteId && d.Id != id);
            if (dbItem != null)
                throw new RestException(HttpStatusCode.Conflict, $"Item already available for this goods received note.");
        }
        private async Task<GoodsReceivedNoteItem> GetGoodsReceivedNoteItemById(long id)
        {
            var item = await _goodsReceivedNoteItem.GetGoodsReceivedNoteItemAsync(d => d.Id == id);

            if (item == null)
                throw new RestException(HttpStatusCode.NotFound, "Goods received note item not found.");

            return item;
        }

        public async Task<GetGoodsReceivedNoteItemDto> CreateGoodsReceivedNoteItemAsync(CreateGoodsReceivedNoteItemDto goodsReceivedNoteItemDto)
        {
            await CheckGoodsReceivedNoteItemAsync(0, goodsReceivedNoteItemDto.GoodsReceivedNoteId, goodsReceivedNoteItemDto.ItemId);

            var item = _mapper.Map<GoodsReceivedNoteItem>(goodsReceivedNoteItemDto);
            item = await _goodsReceivedNoteItem.AddGoodsReceivedNoteItemAsync(item);

            return _mapper.Map<GetGoodsReceivedNoteItemDto>(item);
        }

        public async Task DeleteGoodsReceivedNoteItemAsync(long id)
        {
            var item = await GetGoodsReceivedNoteItemById(id);

            await _goodsReceivedNoteItem.DeleteGoodsReceivedNoteItemAsync(item);
        }

        public async Task<GetGoodsReceivedNoteItemDto> GetGoodsReceivedNoteItemAsync(long id)
        {
            var item = await GetGoodsReceivedNoteItemById(id);

            return _mapper.Map<GetGoodsReceivedNoteItemDto>(item);
        }

        public async Task<IEnumerable<GetGoodsReceivedNoteItemDto>> GetGoodsReceivedNoteItemsAsync(long goodsReceivedNoteId)
        {
            var items = await _goodsReceivedNoteItem.GetGoodsReceivedNoteItemsAsync(d => d.GoodsReceivedNoteId == goodsReceivedNoteId);

            return _mapper.Map<IEnumerable<GetGoodsReceivedNoteItemDto>>(items);
        }

        public async Task<GetGoodsReceivedNoteItemDto> UpdateGoodsReceivedNoteItemAsync(long id, EditGoodsReceivedNoteItemDto goodsReceivedNoteItemDto)
        {
            var item = await GetGoodsReceivedNoteItemById(id);

            await CheckGoodsReceivedNoteItemAsync(id, goodsReceivedNoteItemDto.GoodsReceivedNoteId, goodsReceivedNoteItemDto.ItemId);

            item = _mapper.Map(goodsReceivedNoteItemDto, item);

            await _goodsReceivedNoteItem.UpdateGoodsReceivedNoteItemAsync(item);

            return _mapper.Map<GetGoodsReceivedNoteItemDto>(item);
        }
    }
}
