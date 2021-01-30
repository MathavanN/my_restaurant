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
            GoodsReceivedNotes = new HashSet<GoodsReceivedNote>();
            GoodsCreatedNotes = new HashSet<GoodsReceivedNote>();
            UserRoles = new HashSet<UserRole>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrderRequests { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrderApprovals { get; set; }
        public virtual ICollection<GoodsReceivedNote> GoodsReceivedNotes { get; set; }
        public virtual ICollection<GoodsReceivedNote> GoodsCreatedNotes { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
