using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace App.Adapters.Amqp.Configuration
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
                System.Diagnostics.Debug.WriteLine("Trying to create a connection with RabbitMQ");

                IConnection connection = sp.GetRequiredService<ConnectionFactory>().CreateConnectionAsync().GetAwaiter().GetResult();

                Console.WriteLine(@$"Connected on RabbitMQ '{connection}' with name '{connection.ClientProvidedName}'. 
....Local Port: {connection.LocalPort}
....Remote Port: {connection.RemotePort}
....cluster_name: {connection.ServerProperties["cluster_name"]}
....copyright: {connection.ServerProperties["copyright"]}
....information: {connection.ServerProperties["information"]}
....platform: {connection.ServerProperties["platform"]}
....product: {connection.ServerProperties["product"]}
....version: {connection.ServerProperties["version"]}");

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
