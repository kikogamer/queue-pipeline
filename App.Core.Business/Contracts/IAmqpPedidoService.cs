namespace App.Core.Business.Contracts
{
    public interface IAmqpPedidoService
    {
        Task Confirmar(PedidoRequest pedidoRequest);
        Task EmitirNotaFiscal(PedidoRequest pedidoRequest);
        Task FinalizarEntrega(PedidoRequest pedidoRequest);
        Task IniciarEntrega(PedidoRequest pedidoRequest);
        Task ProcessarPagamento(PedidoRequest pedidoRequest);
    }
}
