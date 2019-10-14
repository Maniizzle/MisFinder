using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence.IRepositories
{
    public interface IRepositoryy<TEntity>
    {
        void Create(TEntity entity);
        void Delete(TEntity entity);
        Task Delete(int id);
        void Update(TEntity entity);
        void Save();
    }
}
