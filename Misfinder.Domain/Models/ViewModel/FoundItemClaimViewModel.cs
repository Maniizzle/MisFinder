using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MisFinder.Domain.Models.ViewModel
{
   public class FoundItemClaimViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300, MinimumLength =6)]
        public string Description { get; set; }
        public string ApplicationUserId { get; set; }
        public ItemCategory ItemCategory { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public int FoundItemId { get; set; }
        public FoundItem FoundItem { get; set; }
        [Required]
        public DateTime DateLost { get; set; }
        [Required]
        public string WhereItemWasLost { get; set; }
        public string Color { get; set; }
        public IFormFile Image { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
