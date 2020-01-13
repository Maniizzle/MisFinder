using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MisFinder.Domain.Models
{
    [Table(nameof(ApplicationUser))]
    public class ApplicationUser: IdentityUser
    {
        public ApplicationUser()
        {
            CreatedOn = DateTime.UtcNow;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedOn { get;  }
        public bool IsBlackListed { get; set; }
        
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{LastName} {FirstName}";
            }
        }
        // public int Id { get; set; }
    }
}
