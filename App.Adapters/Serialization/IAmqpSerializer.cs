using RabbitMQ.Client.Events;

namespace App.Adapters.Serialization
{
    public interface IAmqpSerializer
    {
        TResponse? Deserialize<TResponse>(BasicDeliverEventArgs eventArgs);
        byte[] Serialize<T>(T objectToSerialize);
    }
}
