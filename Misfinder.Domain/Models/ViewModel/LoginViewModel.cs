using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MisFinder.Domain.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        
        [Remote("EmailExist","Account",ErrorMessage ="Invalid UserName or sword")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
