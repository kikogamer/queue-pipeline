using App.Core.Business.Contracts;

namespace Consumer.Entrega.Services
{
    public class EntregaRequestService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IAmqpPedidoService _amqpPedidoService;

        public EntregaRequestService(IPedidoRepository pedidoRepository, IAmqpPedidoService amqpPedidoService)
        {
            _pedidoRepository = pedidoRepository;
            _amqpPedidoService = amqpPedidoService;
        }

        public async Task Close(PedidoRequest pedidoRequest)
        {
            var pedido = await _pedidoRepository.Get(pedidoRequest.Id);

            Console.WriteLine($"Pedido número [{pedido?.Numero}] recebido para encerrar entrega!!!");

            pedido?.EncerrarEntrega();

            await _pedidoRepository.Update(pedido);
        }

        public async Task Start(PedidoRequest pedidoRequest)
        {
            var pedido = await _pedidoRepository.Get(pedidoRequest.Id);

            Console.WriteLine($"Pedido número [{pedido?.Numero}] recebido para iniciar entrega!!!");

            pedido?.IniciarEntrega();

            await _pedidoRepository.Update(pedido);

            await _amqpPedidoService.FinalizarEntrega(pedidoRequest);
        }
    }
}
