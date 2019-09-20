using System;
using System.Collections.Generic;
using System.Text;

namespace MisFinder.Domain.Models
{
   public class ItemClaim
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
       
    }
}
