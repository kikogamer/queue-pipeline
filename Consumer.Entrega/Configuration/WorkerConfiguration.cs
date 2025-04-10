using App.Adapters.Amqp.Configuration;
using App.Adapters.Amqp.Services;
using App.Adapters.Consumer;
using App.Core.Business.Contracts;
using App.Core.Data;
using App.Core.Repository;
using Consumer.Entrega.Services;
using Microsoft.EntityFrameworkCore;

namespace Consumer.Entrega.Configuration
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

            builder.Services.AddTransient<EntregaRequestService>();

            builder.Services.AddRabbitMQConfiguration(cfg => cfg.WithConfiguration(builder.Configuration));

            builder.Services.AddHostedService<WorkerEntrega>();

            ushort prefetchCount = builder.Configuration.GetValue<ushort>("RABBITMQ:PREFETCHCOUNT");

            builder.Services.MapQueue<EntregaRequestService, PedidoRequest>("entrega_create_queue", prefetchCount, (svc, data) => svc.Start(data));
            builder.Services.MapQueue<EntregaRequestService, PedidoRequest>("entrega_close_queue", prefetchCount, (svc, data) => svc.Close(data));

            return builder;
        }
    }
}
