using App.Core.Interfaces;
using App.Core.Repository;

namespace WebApp.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            return services;
        }
    }
}
