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
   public class StateRepository : IStateRepository

    {
        private readonly MisFinderDbContext context;

        public StateRepository(MisFinderDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<State>> GetAllStates()
        {
            IEnumerable<State> states = new List<State>();
            states= await context.States.ToListAsync();
            return states;
        }

        public Task<State> GetStateById(int id)
        {
            return context.States.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
