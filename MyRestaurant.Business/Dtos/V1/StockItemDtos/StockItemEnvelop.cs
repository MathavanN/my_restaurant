namespace MyRestaurant.Business.Dtos.V1
{
    public class StockItemEnvelop : EnvelopDto
    {
        public IEnumerable<GetStockItemDto> StockItems { get; set; } = default!;
        public int StockItemCount { get; set; }
    }
}
