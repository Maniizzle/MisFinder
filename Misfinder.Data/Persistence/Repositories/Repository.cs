using MisFinder.Domain.Models;
using MisFinder.Data.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisFinder.Persistence.Repositories
{
    public class Repository
    {
        private readonly MisFinderDbContext context;

        public Repository(MisFinderDbContext context)
        {
            this.context = context;
        }

        public void Create(FoundItem item)
        {
           
        }
    }
}
