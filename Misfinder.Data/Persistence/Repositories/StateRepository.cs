using Microsoft.EntityFrameworkCore;
using MisFinder.Data.Data.Context;
using MisFinder.Data.Persistence.IRepositories;
using MisFinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence.Repositories
{
    class StateRepository : IStateRepository

    {
        private readonly MisFinderDbContext context;

        public StateRepository(MisFinderDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<State>> GetAllStates()
        {
            return await context.States.ToListAsync();
        }

        public Task<State> GetStateById(int id)
        {
            return context.States.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
