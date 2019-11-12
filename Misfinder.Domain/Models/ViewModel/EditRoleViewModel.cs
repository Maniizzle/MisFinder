using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MisFinder.Domain.Models.ViewModel
{
    public class EditRoleViewModel
    {
        [Required]
        public string Id { get; set; }

        public string RoleName { get; set; }

        [Required]
        public string UserName { get; set; }

        public List<ApplicationUser> Users { get; set; }
        //public List<>
    }
}