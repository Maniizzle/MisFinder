using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MisFinder.Domain.Models.ViewModel
{
    public class LostItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public string NameOfLostItem { get; set; }
        [StringLength(250, MinimumLength = 5)]
        public string Description { get; set; }
        [Required]
        public Location Location { get; set; }
        [MaxLength(100)]
        public string SpecificLocation { get; set; }
        public string Color { get; set; }
        [Required]
        public DateTime DateMisplaced { get; set; }
        public ICollection<FoundAndLostItem> FoundLostItems { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        //public TimeSpan TimeMisplaced { get; set; }
        public string StateFound { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
