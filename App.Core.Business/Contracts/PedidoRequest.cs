namespace App.Core.Business.Contracts
{
    public class PedidoRequest
    {
        public Guid Id{ get; }

        public PedidoRequest(Guid pedidoId)
        {
            Id = pedidoId;
        }
    }
}
