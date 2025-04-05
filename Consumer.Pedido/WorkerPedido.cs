
using RabbitMQ.Client;

namespace Consumer.Pedido
{
    public class WorkerPedido : BackgroundService
    {
        private readonly ILogger<WorkerPedido> _logger;
        private readonly IChannel _channel;

        public WorkerPedido(ILogger<WorkerPedido> logger, IChannel channel)
        {
            _logger = logger;
            _channel = channel;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CreateUnroutedSchema("pedido", new Dictionary<string, string> {
                { "create", "create" }
            });

            await Task.CompletedTask;
        }

        private async void CreateUnroutedSchema(string appName, Dictionary<string, string> routes)
        {
            await _channel.ExchangeDeclareAsync($"{appName}_unrouted_exchange", "fanout", true, false, null);
            await _channel.QueueDeclareAsync($"{appName}_unrouted_queue", true, false, false, null);
            await _channel.QueueBindAsync($"{appName}_unrouted_queue", $"{appName}_unrouted_exchange", string.Empty, null);

            await _channel.ExchangeDeclareAsync($"{appName}_deadletter_exchange", "fanout", true, false, null);
            await _channel.QueueDeclareAsync($"{appName}_deadletter_queue", true, false, false, null);
            await _channel.QueueBindAsync($"{appName}_deadletter_queue", $"{appName}_deadletter_exchange", string.Empty, null);

            await _channel.ExchangeDeclareAsync($"{appName}_service", "topic", true, false, new Dictionary<string, object>() {
                { "alternate-exchange", $"{appName}_unrouted_exchange" }
            });

            foreach (KeyValuePair<string, string> item in routes)
            {
                string routingKey = item.Key;
                string functionalName = item.Value;

                await _channel.QueueDeclareAsync($"{appName}_{functionalName}_queue", true, false, false, new Dictionary<string, object>() {
                    { "x-dead-letter-exchange", $"{appName}_retry_exchange" }
                });
                await _channel.QueueBindAsync($"{appName}_{functionalName}_queue", $"{appName}_service", routingKey, null);
            }
        }
    }
}
