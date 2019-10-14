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
        [MaxLength(300)]
        public string Description { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int FoundItemId { get; set; }
        public FoundItem FoundItem { get; set; }
        [Required]
        public DateTime DateLost { get; set; }
        public IFormFile Image { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
