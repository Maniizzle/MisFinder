using MisFinder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisFinder.Data.Persistence.IRepositories
{
    public interface IRepository
    {
        void Create(ApplicationUser user);
        void Delete(ApplicationUser user);
        void Delete(int id);

        //void Create(TEntity entity);
        //void Delete(int id);
        //void Delete(TEntity entity);
        //void Edit(TEntity entity);

    }
}
//public interface IRepository<TEntity> where TEntity : IEntity
//{
//    void Create(TEntity entity);
//    void Delete(TEntity entity);
//    void Delete(Guid id);
//    void Edit(TEntity entity);

//    //read side (could be in separate Readonly Generic Repository)
//    TEntity GetById(Guid id);
//    IEnumerable<TEntity> Filter();
//    IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate);

//    //separate method SaveChanges can be helpful when using this pattern with UnitOfWork
//    void SaveChanges();