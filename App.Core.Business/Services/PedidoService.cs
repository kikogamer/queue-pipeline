using App.Core.Business.Contracts;
using App.Core.Business.Models;

namespace App.Core.Business.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IAmqpPedidoService _amqpPedidoService;
        
        public PedidoService(IPedidoRepository pedidoRepository, IAmqpPedidoService amqpPedidoService)
        {
            _pedidoRepository = pedidoRepository;
            _amqpPedidoService = amqpPedidoService;
        }

        public async Task Add(Pedido pedido)
        {
            await _pedidoRepository.Add(pedido);
            await _amqpPedidoService.Confirmar(new PedidoRequest(pedido.Id));
        }

        public async Task<Pedido?> Get(Guid id)
        {
            return await _pedidoRepository.Get(id);
        }
    }
}
