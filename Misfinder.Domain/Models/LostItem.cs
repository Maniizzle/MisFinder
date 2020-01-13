using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MisFinder.Domain.Models
{
    public class LostItem : Audit
    {
        public LostItem()
        {
            LostItemClaims = new List<LostItemClaim>();
        }

        public int Id { get; set; }
        public ItemCategory? ItemCategory { get; set; }

        public string NameOfLostItem { get; set; }

        [StringLength(250, MinimumLength = 5)]
        public string Description { get; set; }

        public string ExactArea { get; set; }
        public int LocalGovernmentId { get; set; }
        public virtual LocalGovernment LocalGovernment { get; set; }

        public ICollection<LostItemClaim> LostItemClaims { get; set; }
        public string WhereItemWasLost { get; set; }
        public string Color { get; set; }
        public DateTime DateMisplaced { get; set; }
        public int? ImageId { get; set; }
        public virtual Image Image { get; set; }
        public bool IsMeetingSucceess { get; set; }

        //public ICollection<FoundAndLostItem> FoundLostItems { get; set; }
        public string ApplicationUserId { get; set; }

        public ApplicationUser LostItemUser { get; set; }
        public bool IsFound { get; set; }
    }
}