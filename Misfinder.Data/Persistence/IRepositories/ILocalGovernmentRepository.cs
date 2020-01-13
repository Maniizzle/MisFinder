using MisFinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence.IRepositories
{
  public  interface ILocalGovernmentRepository
    {
        Task<IEnumerable<LocalGovernment>> GetAllLGA();
        Task<IEnumerable<LocalGovernment>> GetAllLGAByStateId(int id);

    }
}
