namespace MyRestaurant.Business.Dtos.V1
{
    public class SupplierEnvelop
    {
        public IEnumerable<GetSupplierDto> Suppliers { get; set; } = default!;
        public int SupplierCount { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalPages { get; set; }
    }
}
