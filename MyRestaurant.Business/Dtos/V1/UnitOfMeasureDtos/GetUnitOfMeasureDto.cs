namespace MyRestaurant.Business.Dtos.V1
{
    public class GetUnitOfMeasureDto : IUnitOfMeasureDto
    {
        public int Id { get; set; }
        public required string Code { get; set; }
        public string? Description { get; set; }
    }
}
