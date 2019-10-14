using Microsoft.EntityFrameworkCore;
using MisFinder.Data.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence.IRepositories
{
    public class EntityRepository<TEntity> : IRepository<TEntity> where TEntity: class
    {
        private readonly MisFinderDbContext context;
        private DbSet<TEntity> _dbSet;

        public EntityRepository(MisFinderDbContext context)
        {
            this.context = context;
        }

        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_dbSet == null)
                {
                    _dbSet = context.Set<TEntity>();
                }

                return _dbSet;
            }
        }
        public void Create(TEntity entity)
        {
            Entities.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
