using App.Adapters;
using App.Adapters.Serialization;
using RabbitMQ.Client;

namespace WebApp.Server.Configuration
{
    public static class RabbitMQConfiguration
    {
        public static IServiceCollection AddRabbitMQConfiguration(this IServiceCollection services, Action<RabbitMQConfigurationBuilder> action)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));
            if (action is null) throw new ArgumentNullException(nameof(action));

            RabbitMQConfigurationBuilder builder = new RabbitMQConfigurationBuilder(services);

            action(builder);

            builder.Build();

            services.AddTransient(sp => sp.GetRequiredService<IConnection>().CreateChannelAsync().GetAwaiter().GetResult());

            services.AddSingleton<IAmqpSerializer, AmqpSerializer>();

            services.AddScoped(sp => new SimpleAmqp(
                sp.GetRequiredService<IChannel>(),
                sp.GetRequiredService<IAmqpSerializer>()));

            return services;
        }
    }
}
