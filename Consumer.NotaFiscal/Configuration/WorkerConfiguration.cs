using App.Adapters.Amqp.Configuration;
using App.Adapters.Amqp.Services;
using App.Adapters.Consumer;
using App.Core.Business.Contracts;
using App.Core.Data;
using App.Core.Repository;
using Consumer.NotaFiscal.Services;
using Microsoft.EntityFrameworkCore;

namespace Consumer.NotaFiscal.Configuration
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

            builder.Services.AddTransient<NotaFiscalRequestService>();

            builder.Services.AddRabbitMQConfiguration(cfg => cfg.WithConfiguration(builder.Configuration));

            builder.Services.AddHostedService<WorkerNotaFiscal>();

            ushort prefetchCount = builder.Configuration.GetValue<ushort>("RABBITMQ:PREFETCHCOUNT");

            builder.Services.MapQueue<NotaFiscalRequestService, PedidoRequest>("nota_fiscal_create_queue", prefetchCount, (svc, data) => svc.Execute(data));

            return builder;
        }
    }
}
