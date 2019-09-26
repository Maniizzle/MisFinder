using System;
using System.Collections.Generic;
using System.Text;

namespace MisFinder.Domain.Models
{
   public class LostItemClaim
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public bool Sorted { get; set; }
    }
}
