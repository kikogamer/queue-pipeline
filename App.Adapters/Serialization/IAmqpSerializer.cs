using RabbitMQ.Client.Events;
using RabbitMQ.Client;

namespace App.Adapters.Serialization
{
    public interface IAmqpSerializer
    {
        byte[] Serialize<T>(T objectToSerialize);
    }
}
