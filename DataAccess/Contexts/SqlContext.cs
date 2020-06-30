using DataAccess.Entities;
using DataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace DataAccess.Contexts
{
    public class SqlContext: DbContext, IDataContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Sushi> Sushies { get; set; }
        public virtual DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<SessionState> SessionStates { get; set; }

        DatabaseType databaseType;

        private string connectionString;
        //public SqlContext()
        //{
        //    connectionString = @"Data Source=DESKTOP-O6IGAQJ\SQLEXPRESS;Initial Catalog=SushiBot;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //}
        public SqlContext(IOptions<DatabaseSettings> options)
        {
            databaseType = options.Value.CurrentDatabaseType;

            var connectionStringKey = databaseType.ToString();
            connectionString = options.Value.ConnectionStrings[connectionStringKey];
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = databaseType switch
            {
                DatabaseType.SqlServer => optionsBuilder.UseSqlServer(connectionString),
                DatabaseType.SqLite => optionsBuilder.UseSqlite(connectionString),
                _ => throw new InvalidAppSettingsKeyException()
            };
        }
    }
}