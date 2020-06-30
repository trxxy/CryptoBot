using DataAccess.Contexts;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class JsonGenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : Entity
    {
        private readonly JsonContext _db;
        private readonly JsonSet<TEntity> _set;

        public JsonGenericRepository(JsonContext db)
        {
            _db = db;
            _set = db.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _set.Entities;
        }
        
        public virtual IEnumerable<TEntity> GetAll(Func<TEntity, bool> prediacte)
        {
            return _set.Entities?.Where(prediacte);
        }

        public virtual TEntity Get(int id)
        {
            return _set.Entities?.Where(q=>q.Id == id).FirstOrDefault();
        }

        public virtual TEntity Get(Func<TEntity, bool> prediacte)
        {
            return _set.Entities?.Where(prediacte).FirstOrDefault();
        }

        public virtual IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return _set.Entities?.Where(predicate);
        }

        public virtual void Create(TEntity entity)
        {
            entity.Id = entity.Id != default ? entity.Id : _set.Entities.Count() + 1;
            _set.Entities.Add(entity);
            _set.IsSetChanged = true;
        }

        public virtual void CreateRange(IEnumerable<TEntity> Entities)
        {
            var counter = _set.Entities?.LastOrDefault()?.Id ?? 0;
            foreach (var entity in Entities)
            {
                counter++;
                entity.Id = counter;
            }
            _set.Entities.AddRange(Entities);
            _set.IsSetChanged = true;
        }

        public virtual void Update(TEntity entity)
        {
            var currentIndex = _set.Entities.ToList().FindIndex(q => q.Id == entity.Id);
            _set.Entities.ToList()[currentIndex] = entity;
            _set.IsSetChanged = true;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> Entities)
        {
            foreach (var entity in Entities)
            {
                var currentIndex = _set.Entities.ToList().FindIndex(q => q.Id == entity.Id);
                _set.Entities.ToList()[currentIndex] = entity;
            }
            _set.IsSetChanged = true;
        }

        public virtual void Delete(int id)
        {
            var entity = _set.Entities?.ToList().Find(q => q.Id == id);
            _set.Entities?.ToList().Remove(entity);
            _set.IsSetChanged = true;
        }

        public virtual void Delete(Func<TEntity, bool> predicate)
        {
            var entity = _set.Entities?.Where(predicate).FirstOrDefault();
            _set.Entities?.ToList().Remove(entity);
            _set.IsSetChanged = true;
        }

        public virtual void DeleteAll()
        {
            _set.Entities = new List<TEntity>();
            _set.IsSetChanged = true;
        }

        public virtual int Count()
        {
            return _set.Entities.Count();
        }
    }
}