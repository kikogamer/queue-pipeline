namespace App.Adapters.Amqp.Services
{
    public abstract class AmqpServiceBase<TRequest>
    {
        private readonly SimpleAmqp _simpleAmqpRpc;

        public AmqpServiceBase(SimpleAmqp simpleAmqp)
        {
            _simpleAmqpRpc = simpleAmqp;
        }

        protected abstract string ExchangeName { get; }

        protected async Task SendAsync(TRequest request, string routingKey = "")
        {
            await _simpleAmqpRpc.Send(exchangeName: ExchangeName,
                                      routingKey: routingKey,
                                      requestModel: request);
        }
    }
}
