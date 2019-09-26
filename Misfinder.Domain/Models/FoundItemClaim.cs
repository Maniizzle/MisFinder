using System;
using System.Collections.Generic;
using System.Text;

namespace MisFinder.Domain.Models
{
   public class FoundItemClaim
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Description { get; set; }
        public int LostItemId { get; set; }
        public LostItem LostItem { get; set; }
    }
}
