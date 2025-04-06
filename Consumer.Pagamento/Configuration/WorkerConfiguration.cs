using App.Adapters.Amqp.Configuration;
using App.Adapters.Amqp.Services;
using App.Adapters.Consumer;
using App.Core.Business.Contracts;
using App.Core.Data;
using App.Core.Repository;
using Consumer.Pagamento.Services;
using Microsoft.EntityFrameworkCore;

namespace Consumer.Pagamento.Configuration
{
    public static class WorkerConfiguration
    {
        public static HostApplicationBuilder ConfigureWorker(this HostApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetValue<string>("DB:CONNECTION_STRING");
            builder.Services.AddDbContext<MeuDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

            builder.Services.AddScoped<IAmqpPedidoService, AmqpPedidoService>();

            builder.Services.AddTransient<PagamentoRequestService>();

            builder.Services.AddRabbitMQConfiguration(cfg => cfg.WithConfiguration(builder.Configuration));

            builder.Services.AddHostedService<WorkerPagamento>();

            ushort prefetchCount = builder.Configuration.GetValue<ushort>("RABBITMQ:PREFETCHCOUNT");

            builder.Services.MapQueue<PagamentoRequestService, PedidoRequest>("pagamento_create_queue", prefetchCount, (svc, data) => svc.Execute(data));

            return builder;
        }
    }
}
