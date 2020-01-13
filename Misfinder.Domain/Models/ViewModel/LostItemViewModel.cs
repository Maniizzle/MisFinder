using Microsoft.AspNetCore.Http;
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
        public ItemCategory ItemCategory { get; set; }

        [StringLength(250, MinimumLength = 5)]
        public string Description { get; set; }
        [Required]
        public int StateId { get; set; }
        
        public State State { get; set; }
        [Required]
        public int LocalGovernmentId { get; set; }
        public LocalGovernment LocalGovernment { get; set; }
        [Required]
        [MaxLength(50)]
        public string ExactArea { get; set; }
        [MaxLength(100)]
        [Required]
        public string WhereItemWasLost { get; set; }
        public string Color { get; set; }
        [Required]
        public DateTime DateMisplaced { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public IFormFile Photo { get; set; }
        //public TimeSpan TimeMisplaced { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
