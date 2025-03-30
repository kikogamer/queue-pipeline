using App.Core.Interfaces;
using App.Core.Models;

namespace App.Core.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }
        public async Task Add(Pedido pedido)
        {
            await _pedidoRepository.Add(pedido);
        }

        public async Task<Pedido?> Get(Guid id)
        {
            return await _pedidoRepository.Get(id);
        }
    }
}
