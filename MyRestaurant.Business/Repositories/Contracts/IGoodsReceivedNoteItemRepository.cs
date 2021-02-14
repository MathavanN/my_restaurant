using MyRestaurant.Business.Dtos.V1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IGoodsReceivedNoteItemRepository
    {
        Task<IEnumerable<GetGoodsReceivedNoteItemDto>> GetGoodsReceivedNoteItemsAsync(long goodsReceivedNoteId);
        Task<GetGoodsReceivedNoteItemDto> CreateGoodsReceivedNoteItemAsync(CreateGoodsReceivedNoteItemDto goodsReceivedNoteItemDto);
        Task<GetGoodsReceivedNoteItemDto> GetGoodsReceivedNoteItemAsync(long id);
        Task<GetGoodsReceivedNoteItemDto> UpdateGoodsReceivedNoteItemAsync(long id, EditGoodsReceivedNoteItemDto goodsReceivedNoteItemDto);
        Task DeleteGoodsReceivedNoteItemAsync(long id);
    }
}
