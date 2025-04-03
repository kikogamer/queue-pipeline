using App.Adapters.Amqp.Services;
using App.Core.Business.Contracts;
using App.Core.Business.Services;
using App.Core.Repository;

namespace WebApp.Server.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAmqpPedidoService, AmqpPedidoService>();
            
            services.AddScoped<IPedidoService, PedidoService>();

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();

            return services;
        }
    }
}
