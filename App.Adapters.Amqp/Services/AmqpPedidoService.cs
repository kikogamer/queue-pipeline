using App.Core.Business.Contracts;

namespace App.Adapters.Amqp.Services
{
    public class AmqpPedidoService : AmqpServiceBase<PedidoRequest>, IAmqpPedidoService
    {
        public AmqpPedidoService(SimpleAmqp simpleAmqp) : base(simpleAmqp)
        {
        }

        protected override string ExchangeName => "";

        public async Task Confirmar(PedidoRequest pedidoRequest)
        {
            await SendAsync(request: pedidoRequest, routingKey: "pedido_create_queue");
        }

        public async Task EmitirNotaFiscal(PedidoRequest pedidoRequest)
        {
            await SendAsync(request: pedidoRequest, routingKey: "nota_fiscal_create_queue");
        }

        public async Task FinalizarEntrega(PedidoRequest pedidoRequest)
        {
            await SendAsync(request: pedidoRequest, routingKey: "entrega_close_queue");
        }

        public async Task IniciarEntrega(PedidoRequest pedidoRequest)
        {
            await SendAsync(request: pedidoRequest, routingKey: "entrega_create_queue");
        }

        public async Task ProcessarPagamento(PedidoRequest pedidoRequest)
        {
            await SendAsync(request: pedidoRequest, routingKey: "pagamento_create_queue");
        }
    }
}
