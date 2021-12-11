namespace MyRestaurant.Business.Dtos.V1
{
    public class SupplierDto
    {
        public string Name { get; set; } = default!;
        public string Address1 { get; set; } = default!;
        public string Address2 { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string? Telephone1 { get; set; }
        public string? Telephone2 { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? ContactPerson { get; set; }
    }
}
