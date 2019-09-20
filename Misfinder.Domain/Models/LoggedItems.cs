using System;
using System.Collections.Generic;
using System.Text;

namespace MisFinder.Domain.Models
{
    class LoggedItems
    {
        public int Id { get; set; }
        public string LostItems{ get; set; }
        public int LostItemId { get; set; }
        public LostItem LostItem { get; set; }


    }
}
