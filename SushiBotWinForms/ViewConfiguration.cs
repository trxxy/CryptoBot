using Logic.Services;
using Logic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace UserInterface
{
    public static class ViewConfiguration
    {
        public static IServiceCollection AddViewServices(this IServiceCollection services)
        => services.AddScoped<IAuthService, AuthService>()
                   .AddScoped<ICatalogService, CatalogService>()
                   .AddScoped<IOrderService, OrderService>()
                   .AddSingleton<BackgroundWorkerService>();
    }
}