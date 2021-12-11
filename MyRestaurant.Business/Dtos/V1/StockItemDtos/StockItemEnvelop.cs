namespace MyRestaurant.Business.Dtos.V1
{
    public class StockItemEnvelop
    {
        public IEnumerable<GetStockItemDto> StockItems { get; set; } = default!;
        public int StockItemCount { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalPages { get; set; }
    }
}
