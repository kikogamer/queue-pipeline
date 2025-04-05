using App.Core.Business.Contracts;

namespace App.Core.Business.Models
{
    public class PedidoConfirmado : IPedidoState
    {
        public IPedidoState Processar(Pedido pedido)
        {
            throw new InvalidOperationException("O pedido já está confirmado!");
        }
    }
}
