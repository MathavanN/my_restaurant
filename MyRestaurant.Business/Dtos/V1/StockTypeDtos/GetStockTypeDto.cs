namespace MyRestaurant.Business.Dtos.V1
{
    public class GetStockTypeDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
