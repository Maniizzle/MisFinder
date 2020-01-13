using System;
using System.Collections.Generic;
using System.Text;

namespace MisFinder.Domain.Models
{
    public class FoundItemClaim : Audit
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ItemCategory? ItemCategory { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public string Description { get; set; }
        public int FoundItemId { get; set; }
        public virtual FoundItem FoundItem { get; set; }
        public int? ImageId { get; set; }
        public virtual Image Image { get; set; }
        public string WhereItemWasLost { get; set; }
        public DateTime DateLost { get; set; }
        public string Color { get; set; }
        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;
        public DateTime? ValidatedOn { get; set; }
        public bool IsAdminValid { get; set; }
    }
}