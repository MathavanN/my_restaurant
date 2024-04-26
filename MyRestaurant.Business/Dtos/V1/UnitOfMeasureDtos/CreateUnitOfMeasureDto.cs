namespace MyRestaurant.Business.Dtos.V1
{
    public class CreateUnitOfMeasureDto : IUnitOfMeasureDto
    {
        public required string Code { get; set; }
        public string? Description { get; set; }
    }
}
