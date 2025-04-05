using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace App.Adapters.Consumer
{
    public abstract class QueueServiceWorkerBase : BackgroundService
    {
        protected readonly ILogger? _logger;
        protected readonly IConnection _connection;
        protected readonly IChannel _channel;
        public ushort PrefetchCount { get; }
        public string QueueName { get; }

        protected QueueServiceWorkerBase(ILogger? logger, IConnection connection, IChannel channel, string queueName, ushort prefetchCount)
        {
            _logger = logger;
            _connection = connection;
            _channel = channel;
            QueueName = queueName;
            PrefetchCount = prefetchCount;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: this.PrefetchCount, global: false);

            IAsyncBasicConsumer consumer = BuildConsumer();

            await WaitQueueCreation();

            string consumerTag = await consumer.Channel.BasicConsumeAsync(queue: this.QueueName, autoAck: false, consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                this?._logger?.LogTrace("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }

            await _channel.BasicCancelAsync(consumerTag);
        }

        protected abstract IAsyncBasicConsumer BuildConsumer();

        protected virtual async Task WaitQueueCreation()
        {
            await _channel.QueueDeclarePassiveAsync(QueueName);
        }
    }
}
