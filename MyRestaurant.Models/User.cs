using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MyRestaurant.Models
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {
            RefreshTokens = new HashSet<RefreshToken>();
            PurchaseOrderRequests = new HashSet<PurchaseOrder>();
            PurchaseOrderApprovals = new HashSet<PurchaseOrder>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrderRequests { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrderApprovals { get; set; }
    }
}
