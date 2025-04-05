using App.Adapters.Amqp.Configuration;
using App.Adapters.Consumer;
using App.Core.Business.Contracts;
using Consumer.Pedido.Services;

namespace Consumer.Pedido.Configuration
{
    public static class WorkerConfiguration
    {
        public static HostApplicationBuilder ConfigureWorker(this HostApplicationBuilder builder)
        {
            builder.Services.AddTransient<PedidoRequestService>();

            builder.Services.AddRabbitMQConfiguration(cfg => cfg.WithConfiguration(builder.Configuration));

            builder.Services.AddHostedService<WorkerPedido>();

            ushort prefetchCount = builder.Configuration.GetValue<ushort>("RABBITMQ:PREFETCHCOUNT");

            builder.Services.MapQueue<PedidoRequestService, PedidoRequest>("pedido_create_queue", prefetchCount, (svc, data) => svc.Execute(data));
                        
            return builder;
        }
    }
}
