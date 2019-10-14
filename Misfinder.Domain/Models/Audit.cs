using System;
using System.Collections.Generic;
using System.Text;

namespace MisFinder.Domain.Models
{
    class Audit : IAudit
    {
        
        public DateTime CreatedAt { get; set; }
        public string DeletedBy { get; set; } 
    }
}
