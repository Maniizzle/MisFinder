﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MisFinder.Domain.Models;

namespace MisFinder.Domain.Models.ViewModel
{
    public class FoundItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [StringLength(250, MinimumLength = 5)]
        public string Description { get; set; }
        [Required]
        public int StateId { get; set; }
        public State State { get; set; }
        [Required]
        public int LocalGovernmentId { get; set; }
        public LocalGovernment LocalGovernment { get; set; }
        [MaxLength(100)]
        public string WhereItemWasFound { get; set; }
        public ItemCategory ItemCategory { get; set; }
        public string Colour { get; set; }
        [Required]
        public DateTime DateFound { get; set; }
        //   public TimeSpan TimeFound { get; set; }
        [MaxLength(30)]
        public string ExactArea { get; set; }
        public IFormFile Photo { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser FoundItemUser { get; set; }
      
        public string PhoneNumber { get; set; }
        //public DateTime DateCreated { get; set; }
    }
}
