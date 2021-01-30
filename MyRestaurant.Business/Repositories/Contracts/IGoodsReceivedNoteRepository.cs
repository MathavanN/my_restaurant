using MyRestaurant.Business.Dtos.V1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IGoodsReceivedNoteRepository
    {
        Task<IEnumerable<GetGoodsReceivedNoteDto>> GetGoodsReceivedNotesAsync();
        Task<GetGoodsReceivedNoteDto> GetGoodsReceivedNoteAsync(long id);
        Task<GetGoodsReceivedNoteDto> CreateGoodsReceivedNoteAsync(CreateGoodsReceivedNoteDto goodsReceivedNoteDto);
        Task UpdateGoodsReceivedNoteAsync(long id, EditGoodsReceivedNoteDto goodsReceivedNoteDto);
        Task DeleteGoodsReceivedNoteAsync(long id);
    }
}
