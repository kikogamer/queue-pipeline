using App.Adapters.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace App.Adapters.Consumer
{
    public static class WorkerExtensions
    {
        public static void MapQueue<TService, TRequest>(this IServiceCollection services, string queueName, ushort prefetchCount, Func<TService?, TRequest?, Task> functionToExecute)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));
            if (string.IsNullOrEmpty(queueName)) throw new ArgumentException($"'{nameof(queueName)}' cannot be null or empty.", nameof(queueName));
            if (prefetchCount < 1) throw new ArgumentOutOfRangeException(nameof(prefetchCount));
            if (functionToExecute is null) throw new ArgumentNullException(nameof(functionToExecute));

            services.AddSingleton<IHostedService>(sp => 
                new AsyncQueueServiceWorker<TRequest, Task>(
                    sp.GetService<ILogger<AsyncQueueServiceWorker<TRequest, Task>>>(),
                    sp.GetRequiredService<IConnection>(),
                    sp.GetRequiredService<IChannel>(),
                    sp.GetRequiredService<IAmqpSerializer>(),
                    (request) => functionToExecute(sp.GetService<TService>(), request),
                    queueName,
                    prefetchCount
                )
            );
        }
    }
}
