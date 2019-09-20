using MisFinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence.IRepositories
{
   public interface IStateRepository
    {
        Task<IEnumerable<State>> GetAllStates();
        Task<State> GetStateById(int id);      

    }
}
