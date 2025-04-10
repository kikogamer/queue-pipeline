using App.Adapters.Amqp.Configuration;
using RabbitMQ.Client;

namespace Consumer.Entrega
{
    public class WorkerEntrega : BackgroundService
    {
        private readonly ILogger<WorkerEntrega> _logger;
        private readonly ConfigureRabbitMQRoutes _configureRoutes;

        public WorkerEntrega(ILogger<WorkerEntrega> logger, IChannel channel)
        {
            _logger = logger;
            _configureRoutes = new ConfigureRabbitMQRoutes(channel);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _configureRoutes.CreateUnroutedSchema("entrega", new Dictionary<string, string> {
                { "create", "create" }
            });
            _configureRoutes.CreateUnroutedSchema("entrega", new Dictionary<string, string> {
                { "close", "close" }
            });

            await Task.CompletedTask;
        }
    }
}
