using System.Text;

namespace App.Adapters.Serialization
{
    public class AmqpSerializer : IAmqpSerializer
    {
        public byte[] Serialize<T>(T objectToSerialize)
        {
            return Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(objectToSerialize));
        }
    }
}
