using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace DataAccess.Contexts
{
    public class JsonContext : IDataContext
    {
        private bool disposed;
        private event Action OnSave;
        string RootDirectoryPath { get; }
        public virtual JsonSet<User> Users { get; set; }
        public virtual JsonSet<Sushi> Sushies { get; set; }
        public virtual JsonSet<Basket> Baskets { get; set; }
        public virtual JsonSet<SessionState> SessionStates { get; set; }

        public JsonContext(IOptions<DatabaseSettings> options)
        {
            var connectionStrings = options.Value.ConnectionStrings;
            var databaseType = options.Value.CurrentDatabaseType.ToString();

            RootDirectoryPath = Directory.GetCurrentDirectory() + connectionStrings[databaseType];

            Users = new JsonSet<User>(ref OnSave, RootDirectoryPath);
            Sushies = new JsonSet<Sushi>(ref OnSave, RootDirectoryPath);
            Baskets = new JsonSet<Basket>(ref OnSave, RootDirectoryPath);
            SessionStates = new JsonSet<SessionState>(ref OnSave, RootDirectoryPath);
        }


        public JsonSet<TEntity> Set<TEntity>()
           where TEntity : Entity
        {
            var type = this.GetType();
            var properties = type.GetProperties();
            var currentProperty = properties.FirstOrDefault(p => p.PropertyType.GenericTypeArguments[0].Equals(typeof(TEntity)));
            var propertyValue = currentProperty.GetValue(this);
            return (JsonSet<TEntity>)propertyValue;
        }

        public int SaveChanges()
        {
            OnSave();
            return 1;
        }

        //public virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            this.Dispose();
        //        }
        //        this.disposed = true;
        //    }
        //}

        public void Dispose()
        {
            
        }

    }
}