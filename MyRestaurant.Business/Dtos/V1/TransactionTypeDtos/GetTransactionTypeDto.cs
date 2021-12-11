namespace MyRestaurant.Business.Dtos.V1
{
    public class GetTransactionTypeDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = default!;
    }
}
