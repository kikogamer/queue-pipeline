using RabbitMQ.Client;

namespace WebApp.Server.Configuration
{
    public class RabbitMQConfigurationBuilder
    {
        private IConfiguration _configuration;
        private string _configurationPrefix = "RABBITMQ";
        private readonly IServiceCollection _services;

        public RabbitMQConfigurationBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public void Build()
        {
            _services.AddTransient(sp => sp.GetRequiredService<IConnection>().CreateChannelAsync().GetAwaiter().GetResult());

            _services.AddSingleton(sp =>
            {
                ConnectionFactory connectionFactory = new();
                _configuration.Bind(_configurationPrefix, connectionFactory);
                return connectionFactory;
            });

            _services.AddSingleton(sp =>
            {
                IConnection connection = sp.GetRequiredService<ConnectionFactory>().CreateConnectionAsync().GetAwaiter().GetResult();

                return connection;
            });
        }

        public RabbitMQConfigurationBuilder WithConfiguration(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            _configuration = configuration;

            return this;
        }
    }
}
