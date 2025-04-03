using App.Adapters.Serialization;
using RabbitMQ.Client;

namespace App.Adapters
{
    public class SimpleAmqp
    {
        private readonly IChannel _channel;
        private readonly IAmqpSerializer _serializer;

        public SimpleAmqp(IChannel channel, IAmqpSerializer serializer)
        {
            _channel = channel;
            _serializer = serializer;
        }

        public async Task Send<TRequest>(string exchangeName, string routingKey, TRequest requestModel)
        {
            await _channel.BasicPublishAsync(exchange: exchangeName,
                                             routingKey: routingKey,
                                             body: _serializer.Serialize(requestModel));
        }

    }
}
