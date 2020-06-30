using System;

namespace DataAccess.Contexts
{
    public interface IDataContext : IDisposable
    {
        public int SaveChanges();
    }
}