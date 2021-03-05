using MyRestaurant.Core;
using MyRestaurant.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyRestaurant.Services.Tests
{
    public class SupplierInitializer
    {
        public static void Initialize(MyRestaurantContext context)
        {
            if (!context.Suppliers.Any())
            {
                var suppliers = new List<Supplier>
                {
                    new Supplier
                    {
                        Id = 1,
                        Name = "ABC Pvt Ltd",
                        Address1 = "American Mission School Road",
                        Address2 = "Madduvil South",
                        City = "Chavakachcheri",
                        Country = "Sri Lanka",
                        Telephone1 = "0765554345",
                        Telephone2 = "0766554567",
                        Fax = "",
                        Email = "goldendining2010@gmail.com",
                        ContactPerson = "James"
                    },
                    new Supplier
                    {
                        Id = 2,
                        Name = "VBT Pvt Ltd",
                        Address1 = "VBT Road",
                        Address2 = "VBTt",
                        City = "Jaffna",
                        Country = "Sri Lanka",
                        Telephone1 = "0777113644",
                        Telephone2 = "",
                        Fax = "",
                        Email = "test@test.com",
                        ContactPerson = "James"
                    }
                };
                context.Suppliers.AddRange(suppliers);
                context.SaveChanges();
            }
        }
    }
}
