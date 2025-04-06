using App.Core.Business.Contracts;

namespace Consumer.NotaFiscal.Services
{
    public class NotaFiscalRequestService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IAmqpPedidoService _amqpPedidoService;

        public NotaFiscalRequestService(IPedidoRepository pedidoRepository, IAmqpPedidoService amqpPedidoService)
        {
            _pedidoRepository = pedidoRepository;
            _amqpPedidoService = amqpPedidoService;
        }

        public async Task Execute(PedidoRequest pedidoRequest)
        {
            var pedido = await _pedidoRepository.Get(pedidoRequest.Id);

            Console.WriteLine($"Pedido número [{pedido?.Numero}] emitindo nota fiscal!!!");

            //pedido?.Processar();

            await _pedidoRepository.Update(pedido);

            //await _amqpPedidoService.EmitirNotaFiscal(pedidoRequest);
        }
    }
}
