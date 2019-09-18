using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MisFinder.Domain.Models.ViewModel
{
    public class SearchViewModel
    {   [Required]
        public string Name { get; set; }
        
        public string Color { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
