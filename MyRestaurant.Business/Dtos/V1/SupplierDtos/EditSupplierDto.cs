namespace MyRestaurant.Business.Dtos.V1
{
    public class EditSupplierDto
    {
        public string Name { get; set; } = default!;
        public string Address1 { get; set; } = default!;
        public string Address2 { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string Telephone1 { get; set; } = default!;
        public string Telephone2 { get; set; } = default!;
        public string Fax { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string ContactPerson { get; set; } = default!;
    }
}
