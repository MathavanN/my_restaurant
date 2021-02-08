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
    public class GoodsReceivedNoteFreeItemRepository : IGoodsReceivedNoteFreeItemRepository
    {
        private readonly IMapper _mapper;
        private readonly IGoodsReceivedNoteFreeItemService _goodsReceivedNoteFreeItem;
        public GoodsReceivedNoteFreeItemRepository(IMapper mapper, IGoodsReceivedNoteFreeItemService goodsReceivedNoteFreeItem)
        {
            _mapper = mapper;
            _goodsReceivedNoteFreeItem = goodsReceivedNoteFreeItem;
        }

        private async Task CheckGoodsReceivedNoteFreeItemAsync(long id, long goodsReceivedNoteId, long itemId)
        {
            var dbItem = await _goodsReceivedNoteFreeItem.GetGoodsReceivedNoteFreeItemAsync(d => d.ItemId == itemId && d.GoodsReceivedNoteId == goodsReceivedNoteId && d.Id != id);
            if (dbItem != null)
                throw new RestException(HttpStatusCode.Conflict, $"Item already available for this goods received note.");
        }
        private async Task<GoodsReceivedNoteFreeItem> GetGoodsReceivedNoteFreeItemById(long id)
        {
            var item = await _goodsReceivedNoteFreeItem.GetGoodsReceivedNoteFreeItemAsync (d => d.Id == id);

            if (item == null)
                throw new RestException(HttpStatusCode.NotFound, "Goods received note free item not found.");

            return item;
        }

        public async Task<GetGoodsReceivedNoteFreeItemDto> CreateGoodsReceivedNoteFreeItemAsync(CreateGoodsReceivedNoteFreeItemDto goodsReceivedNoteFreeItemDto)
        {
            await CheckGoodsReceivedNoteFreeItemAsync(0, goodsReceivedNoteFreeItemDto.GoodsReceivedNoteId, goodsReceivedNoteFreeItemDto.ItemId);

            var item = _mapper.Map<GoodsReceivedNoteFreeItem>(goodsReceivedNoteFreeItemDto);
            await _goodsReceivedNoteFreeItem.AddGoodsReceivedNoteFreeItemAsync(item);

            return _mapper.Map<GetGoodsReceivedNoteFreeItemDto>(item);
        }

        public async Task DeleteGoodsReceivedNoteFreeItemAsync(long id)
        {
            var item = await GetGoodsReceivedNoteFreeItemById(id);

            await _goodsReceivedNoteFreeItem.DeleteGoodsReceivedNoteFreeItemAsync(item);
        }

        public async Task<GetGoodsReceivedNoteFreeItemDto> GetGoodsReceivedNoteFreeItemAsync(long id)
        {
            var item = await GetGoodsReceivedNoteFreeItemById(id);

            return _mapper.Map<GetGoodsReceivedNoteFreeItemDto>(item);
        }

        public async Task<IEnumerable<GetGoodsReceivedNoteFreeItemDto>> GetGoodsReceivedNoteFreeItemsAsync(long goodsReceivedNoteId)
        {
            var items = await _goodsReceivedNoteFreeItem.GetGoodsReceivedNoteFreeItemsAsync(d => d.GoodsReceivedNoteId == goodsReceivedNoteId);

            return _mapper.Map<IEnumerable<GetGoodsReceivedNoteFreeItemDto>>(items);
        }

        public async Task UpdateGoodsReceivedNoteFreeItemAsync(long id, EditGoodsReceivedNoteFreeItemDto goodsReceivedNoteFreeItemDto)
        {
            await CheckGoodsReceivedNoteFreeItemAsync(id, goodsReceivedNoteFreeItemDto.GoodsReceivedNoteId, goodsReceivedNoteFreeItemDto.ItemId);

            var item = await GetGoodsReceivedNoteFreeItemById(id);

            item = _mapper.Map(goodsReceivedNoteFreeItemDto, item);

            await _goodsReceivedNoteFreeItem.UpdateGoodsReceivedNoteFreeItemAsync(item);
        }
    }
}
