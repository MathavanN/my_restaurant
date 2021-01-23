using System.Collections.Generic;

namespace MyRestaurant.Business.Dtos.V1
{
    public class SupplierEnvelop
    {
        public IEnumerable<GetSupplierDto> Suppliers { get; set; }
        public int SupplierCount { get; set; }
    }
}
