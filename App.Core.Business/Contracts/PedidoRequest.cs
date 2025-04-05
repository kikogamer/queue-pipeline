using System.Text.Json.Serialization;

namespace App.Core.Business.Contracts
{
    public class PedidoRequest
    {
        public Guid Id{ get; }

        public PedidoRequest(Guid id)
        {
            Id = id;
        }
    }
}
