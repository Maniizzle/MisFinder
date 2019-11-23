using MisFinder.Data.Data.Context;
using MisFinder.Data.Persistence.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence.Repositories
{
    internal class BackGroundCheckRepository // :IBackgroundCheck
    {
        public BackGroundCheckRepository(MisFinderDbContext context)
        {
            this.context = context;
        }

        private readonly MisFinderDbContext context;

        //public Task Check()
        //{
        //  var foundItems = context.FoundItems;
        //    return ;
        //}
    }
}