using App.Core.Business.Contracts;

namespace App.Adapters.Amqp.Services
{
    public class AmqpPedidoService : AmqpServiceBase<PedidoRequest>, IAmqpPedidoService
    {
        public AmqpPedidoService(SimpleAmqp simpleAmqp) : base(simpleAmqp)
        {
        }

        protected override string ExchangeName => "";

        public async Task Add(PedidoRequest pedidoRequest)
        {
            await SendAsync(request: pedidoRequest, routingKey: "pedido_create_queue");
        }
    }
}
