using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace App.Adapters.Serialization
{
    public class AmqpSerializer : IAmqpSerializer
    {
        public TResponse? Deserialize<TResponse>(BasicDeliverEventArgs eventArgs)
        {
            if (eventArgs is null) throw new ArgumentNullException(nameof(eventArgs));
            if (eventArgs.BasicProperties is null) throw new ArgumentNullException(nameof(eventArgs.BasicProperties));

            return this.DeserializeInternal<TResponse>(eventArgs.BasicProperties, eventArgs.Body);
        }

        protected TResponse? DeserializeInternal<TResponse>(IReadOnlyBasicProperties basicProperties, ReadOnlyMemory<byte> body)
        {
            string message = Encoding.UTF8.GetString(body.ToArray());
            return JsonSerializer.Deserialize<TResponse>(message);
        }

        public byte[] Serialize<T>(T objectToSerialize)
        {
            return Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(objectToSerialize));
        }
    }
}
