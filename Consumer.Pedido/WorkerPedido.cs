
using App.Adapters.Amqp.Configuration;
using RabbitMQ.Client;

namespace Consumer.Pedido
{
    public class WorkerPedido : BackgroundService
    {
        private readonly ILogger<WorkerPedido> _logger;
        private readonly ConfigureRabbitMQRoutes _configureRoutes;

        public WorkerPedido(ILogger<WorkerPedido> logger, IChannel channel)
        {
            _logger = logger;
            _configureRoutes = new ConfigureRabbitMQRoutes(channel);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _configureRoutes.CreateUnroutedSchema("pedido", new Dictionary<string, string> {
                { "create", "create" }
            });

            await Task.CompletedTask;
        }
    }
}
