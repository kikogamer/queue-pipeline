using App.Adapters.Amqp.Configuration;
using RabbitMQ.Client;

namespace Consumer.Pagamento
{
    public class WorkerPagamento : BackgroundService
    {
        private readonly ILogger<WorkerPagamento> _logger;
        private readonly ConfigureRabbitMQRoutes _configureRoutes;

        public WorkerPagamento(ILogger<WorkerPagamento> logger, IChannel channel)
        {
            _logger = logger;
            _configureRoutes = new ConfigureRabbitMQRoutes(channel);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _configureRoutes.CreateUnroutedSchema("pagamento", new Dictionary<string, string> {
                { "create", "create" }
            });

            await Task.CompletedTask;
        }
    }
}
