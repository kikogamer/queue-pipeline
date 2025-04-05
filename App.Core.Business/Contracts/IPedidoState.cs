using App.Core.Business.Models;

namespace App.Core.Business.Contracts
{
    public interface IPedidoState
    {
        IPedidoState Processar(Pedido pedido);
    }
}
