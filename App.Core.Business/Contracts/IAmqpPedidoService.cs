namespace App.Core.Business.Contracts
{
    public interface IAmqpPedidoService
    {
        Task Add(PedidoRequest pedidoRequest);
    }
}
