using DataAccess.Contexts;
using DataAccess.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DataAccess.Configuration
{
    public static class DataAccessConfiguration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            var current = services.BuildServiceProvider()
                .GetRequiredService<IOptions<DatabaseSettings>>()
                .Value
                .CurrentDatabaseType;
            return current switch
            {
                DatabaseType.SqlServer & DatabaseType.SqLite => services.AddDbContext<IDataContext, SqlContext>(ServiceLifetime.Scoped),
                DatabaseType.JsonFile => services.AddScoped<IDataContext, JsonContext>(),
                _ => throw new InvalidAppSettingsKeyException()
            };
        }
    }
}