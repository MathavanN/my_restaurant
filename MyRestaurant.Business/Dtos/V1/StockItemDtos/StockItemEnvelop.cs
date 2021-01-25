using System.Collections.Generic;

namespace MyRestaurant.Business.Dtos.V1
{
    public class StockItemEnvelop
    {
        public IEnumerable<GetStockItemDto> StockItems { get; set; }
        public int StockItemCount { get; set; }
    }
}
