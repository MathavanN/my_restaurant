namespace MyRestaurant.Business.Dtos.V1
{
    public class EditUnitOfMeasureDto : IUnitOfMeasureDto
    {
        public required string Code { get; set; }
        public string? Description { get; set; }
    }
}
