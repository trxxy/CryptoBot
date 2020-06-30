using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Exceptions;
using DataAccess.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IGenericRepository<Basket> BasketRepository 
            => _basketRepository ??= GetCurrentRepository<Basket>();
        public IGenericRepository<Sushi> SushiRepository 
            => _sushiRepository ??= GetCurrentRepository<Sushi>();
        public IGenericRepository<User> UserRepository
            => _userRepository ??= GetCurrentRepository<User>();
        public IGenericRepository<SessionState> SessionStateRepository
            => _sessionStateRepository ??= GetCurrentRepository<SessionState>();

        private IDataContext _db;
        private IGenericRepository<Basket> _basketRepository;
        private IGenericRepository<Sushi> _sushiRepository;
        private IGenericRepository<User> _userRepository;
        private IGenericRepository<SessionState> _sessionStateRepository;

        private DatabaseType databaseType;
        private bool disposed = false;

        public UnitOfWork(IDataContext db, IOptions<DatabaseSettings> options)
        {
            _db = db;
            databaseType = options.Value.CurrentDatabaseType;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            //logger.LogDebug("Datacontext was disposed");
        }

        private IGenericRepository<TEntity> GetCurrentRepository<TEntity>()
            where TEntity : Entity
            => databaseType switch
            {
                DatabaseType.SqlServer & DatabaseType.SqLite 
                    => new SqlGenericRepository<TEntity>((SqlContext)_db),
                DatabaseType.JsonFile 
                    => new JsonGenericRepository<TEntity>((JsonContext)_db),
                _ => throw new InvalidAppSettingsKeyException()
            };
    }
}