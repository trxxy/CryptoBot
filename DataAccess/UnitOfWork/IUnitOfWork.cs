using DataAccess.Entities;
using System;

namespace DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Basket> BasketRepository { get; }
        IGenericRepository<Sushi> SushiRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<SessionState> SessionStateRepository { get; }
        void SaveChanges();
    }
}