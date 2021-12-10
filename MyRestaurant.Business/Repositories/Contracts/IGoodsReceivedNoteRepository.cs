using MyRestaurant.Business.Dtos.V1;

namespace MyRestaurant.Business.Repositories.Contracts
{
    public interface IGoodsReceivedNoteRepository
    {
        Task<IEnumerable<GetGoodsReceivedNoteDto>> GetGoodsReceivedNotesAsync();
        Task<GetGoodsReceivedNoteDto> GetGoodsReceivedNoteAsync(long id);
        Task<GetGoodsReceivedNoteDto> CreateGoodsReceivedNoteAsync(CreateGoodsReceivedNoteDto goodsReceivedNoteDto);
        Task<GetGoodsReceivedNoteDto> UpdateGoodsReceivedNoteAsync(long id, EditGoodsReceivedNoteDto goodsReceivedNoteDto);
        Task DeleteGoodsReceivedNoteAsync(long id);
        Task<GetGoodsReceivedNoteDto> ApprovalGoodsReceivedNoteAsync(long id, ApprovalGoodsReceivedNoteDto goodsReceivedNoteDto);
    }
}
