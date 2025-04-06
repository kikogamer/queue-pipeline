using App.Core.Business.Contracts;

namespace Consumer.Pedido.Services
{
    public class PedidoRequestService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IAmqpPedidoService _amqpPedidoService;

        public PedidoRequestService(IPedidoRepository pedidoRepository, IAmqpPedidoService amqpPedidoService)
        {
            _pedidoRepository = pedidoRepository;
            _amqpPedidoService = amqpPedidoService;
        }

        public async Task Execute(PedidoRequest pedidoRequest)
        {
            var pedido = await _pedidoRepository.Get(pedidoRequest.Id);

            Console.WriteLine($"Pedido número [{pedido?.Numero}] recebido com sucesso!!!");

            pedido?.Processar();

            await _pedidoRepository.Update(pedido);

            await _amqpPedidoService.EmitirNotaFiscal(pedidoRequest);
        }
    }
}
