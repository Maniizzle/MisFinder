using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MisFinder.Domain.Models
{
    public class FoundItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [StringLength(250,MinimumLength =5)]
        public string Description { get; set; }
        public Location? Location { get; set; }
        [MaxLength(100)]
        public string SpecificLocation { get; set; }
        public string Colour { get; set; }
        public DateTime DateFound { get; set; }
      //  public LCDA LocationArea { get; set; }
        public ICollection<FoundAndLostItem> FoundLostItems { get; set; }
     //   public TimeSpan TimeFound { get; set; }
       // public string StateFound { get; set; }
        public byte[] Image { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser FoundItemUser { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsClaimed { get; set; }
        
    }
}
