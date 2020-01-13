using System;
using System.Collections.Generic;
using System.Text;

namespace MisFinder.Domain.Models
{
    public class LostItemClaim : Audit
    {
        public int Id { get; set; }
        public ItemCategory? ItemCategory { get; set; }

        public string Description { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int LostItemId { get; set; }
        public virtual LostItem LostItem { get; set; }
        public string WhereItemWasFound { get; set; }
        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;

        //public bool IsValidated { get; set; }
        public DateTime? ValidatedOn { get; set; }

        public DateTime DateFound { get; set; }
        public int? ImageId { get; set; }
        public virtual Image Image { get; set; }
        public bool IsAdminValid { get; set; }
    }
}