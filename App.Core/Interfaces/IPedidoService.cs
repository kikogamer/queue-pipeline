using App.Core.Models;

namespace App.Core.Interfaces
{
    public interface IPedidoService
    {
        Task Add(Pedido pedido);
        Task<Pedido?> Get(Guid id);
    }
}
