namespace MyRestaurant.Business.Dtos.V1
{
    public class SupplierEnvelop : EnvelopDto
    {
        public IEnumerable<GetSupplierDto> Suppliers { get; set; } = default!;
        public int SupplierCount { get; set; }
    }
}
