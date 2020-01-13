using Microsoft.EntityFrameworkCore;
using MisFinder.Data.Data.Context;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence.Repositories
{
   public class LocalGovernmentRepository : ILocalGovernmentRepository
    {
        private readonly MisFinderDbContext context;

        public LocalGovernmentRepository(MisFinderDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<LocalGovernment>> GetAllLGA()
        {
            return await context.LocalGovernments.ToListAsync();
        }

        public async Task<IEnumerable<LocalGovernment>> GetAllLGAByStateId(int id)
        {
            IEnumerable<LocalGovernment> localGovernments = new List<LocalGovernment>();
               localGovernments= await context.LocalGovernments.Where(c => c.StateId == id).ToListAsync();
            return localGovernments;
        }
    }
}
