using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MisFinder.Domain.Models.ViewModel
{
    public class MeetingDateViewModel
    {
        [Required]
        public DateTime? FirstDate { get; set; }

        [Required]
        public DateTime? SecondDate { get; set; }

        [Required]
        public int StateId { get; set; }

        [Required]
        public int LGAId { get; set; }
    }
}