using System;
using System.Collections.Generic;
using System.Text;

namespace MisFinder.Domain.Models
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<LocalGovernment> LocalGovernments { get; set; }
    }
}
