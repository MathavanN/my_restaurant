namespace MyRestaurant.Business.Dtos.V1
{
    public class UnitOfMeasureDto : IUnitOfMeasureDto
    {
        public string Code { get; set; } = default!;
        public string? Description { get; set; }
    }
}
