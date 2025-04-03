using App.Core.Business.Models;

namespace App.Core.Business.Contracts
{
    public interface IPedidoService
    {
        Task Add(Pedido pedido);
        Task<Pedido?> Get(Guid id);
    }
}
