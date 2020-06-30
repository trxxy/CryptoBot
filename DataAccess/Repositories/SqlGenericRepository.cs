using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class SqlGenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : Entity
    {
        private SqlContext _db;
        private DbSet<TEntity> _set;

        public SqlGenericRepository(SqlContext db)
        {
            _db = db;
            _set = db.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            var result = _set.ToList();
            return result;
        }

        public virtual IEnumerable<TEntity> GetAll(Func<TEntity, bool> prediacte)
        {
            return _set.Where(prediacte);
        }

        public virtual TEntity Get(int id)
        {
            return _set.Find(id);
        }

        public virtual TEntity Get(Func<TEntity, bool> prediacte)
        {
            return _set.Where(prediacte).FirstOrDefault();
        }

        public virtual IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return _set.Where(predicate).ToList();
        }

        public virtual TEntity FindOne(Func<TEntity, bool> predicate)
        {
            return _set.Where(predicate).ToList().FirstOrDefault();
        }

        public virtual void Create(TEntity entity)
        {
            _set.Add(entity);
        }

        public virtual void CreateRange(IEnumerable<TEntity> entities)
        {
            _set.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _db.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            _db.UpdateRange(entities);
        }

        public virtual void Delete(int id)
        {
            var client = _set.Find(id);
            if (client != null) _set.Remove(client);
        }

        public virtual void Delete(Func<TEntity, bool> predicate)
        {
            _set.Remove(_db.Set<TEntity>().Where(predicate).FirstOrDefault());
        }

        public virtual void DeleteAll()
        {
            _set.RemoveRange(_set);
        }

        public virtual int Count()
        {
            return _db.Set<TEntity>().Count();
        }

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _db.Set<TEntity>().AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}