using System;
using System.Collections.Generic;
using System.Text;

namespace MisFinder.Domain.Models
{
  public  interface IAudit
    {
         DateTime CreatedAt { get; set; }
         string DeletedBy { get; set; }
        

    }
}
