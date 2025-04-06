using App.Adapters.Amqp.Configuration;
using RabbitMQ.Client;

namespace Consumer.NotaFiscal
{
    public class WorkerNotaFiscal : BackgroundService
    {
        private readonly ILogger<WorkerNotaFiscal> _logger;
        private readonly ConfigureRabbitMQRoutes _configureRoutes;

        public WorkerNotaFiscal(ILogger<WorkerNotaFiscal> logger, IChannel channel)
        {
            _logger = logger;
            _configureRoutes = new ConfigureRabbitMQRoutes(channel);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _configureRoutes.CreateUnroutedSchema("nota_fiscal", new Dictionary<string, string> {
                { "create", "create" }
            });

            await Task.CompletedTask;
        }
    }
}
