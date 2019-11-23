using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence.IRepositories
{
    public interface IBackgroundCheck
    {
        Task Check();
    }
}