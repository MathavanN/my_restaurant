namespace MyRestaurant.Business.Dtos.V1
{
    public class EditStockTypeDto
    {
        public string Type { get; set; }
        public string Description { get; set; }
    }

    public class GetStockTypeDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
