using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MisFinder.Domain.Models
{
    public class FoundItem : Audit
    {
        public FoundItem()
        {
            FoundItemClaims = new List<FoundItemClaim>();
        }

        public int Id { get; set; }
        public ItemCategory? ItemCategory { get; set; }
        public string NameOfFoundItem { get; set; }

        [StringLength(250, MinimumLength = 5)]
        public string Description { get; set; }

        public int LocalGovernmentId { get; set; }
        public virtual LocalGovernment LocalGovernment { get; set; }
        public string ExactArea { get; set; }

        [MaxLength(100)]
        public string WhereItemWasFound { get; set; }

        public string Colour { get; set; }
        public DateTime DateFound { get; set; }

        //  public LCDA LocationArea { get; set; }
        public ICollection<FoundItemClaim> FoundItemClaims { get; set; }

        //   public TimeSpan TimeFound { get; set; }
        public int? ImageId { get; set; }

        public virtual Image Image { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser FoundItemUser { get; set; }
        public bool IsClaimed { get; set; }
        public bool IsMeetingSucceess { get; set; }
    }
}