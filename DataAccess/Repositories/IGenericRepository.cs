using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess
{
    public interface IGenericRepository<TEntity>
        where TEntity : Entity
    {
        TEntity Get(int id);
        TEntity Get(Func<TEntity, bool> prediacte);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Func<TEntity, bool> prediacte);
        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);
        void Create(TEntity item);
        void CreateRange(IEnumerable<TEntity> entities);
        void Update(TEntity item);
        void Delete(int id);
        void Delete(Func<TEntity, bool> prediacte);
        void DeleteAll();
        int Count();
    }
}