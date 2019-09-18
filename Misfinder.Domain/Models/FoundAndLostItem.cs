
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisFinder.Domain.Models
{
    public class FoundAndLostItem
    {
        public int FoundItemId { get; set; }
        public int LostItemId { get; set; }
        public FoundItem FoundItem { get; set; }
        public LostItem LostItem { get; set; }

    }
}
