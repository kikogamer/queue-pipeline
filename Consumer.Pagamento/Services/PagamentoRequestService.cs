using App.Core.Business.Contracts;

namespace Consumer.Pagamento.Services
{
    public class PagamentoRequestService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IAmqpPedidoService _amqpPedidoService;

        public PagamentoRequestService(IPedidoRepository pedidoRepository, IAmqpPedidoService amqpPedidoService)
        {
            _pedidoRepository = pedidoRepository;
            _amqpPedidoService = amqpPedidoService;
        }

        public async Task Execute(PedidoRequest pedidoRequest)
        {
            var pedido = await _pedidoRepository.Get(pedidoRequest.Id);

            Console.WriteLine($"Pedido número [{pedido?.Numero}] recebido para processar pagamento!!!");

            pedido?.ProcessarPagamento();

            await _pedidoRepository.Update(pedido);

            await _amqpPedidoService.EmitirNotaFiscal(pedidoRequest);
        }
    }
}
