using App.Core.Business.Contracts;

namespace Consumer.Pedido.Services
{
    public class PedidoRequestService
    {
        public Task Execute(PedidoRequest pedidoRequest)
        {
            System.Console.WriteLine($"Pedido número: {pedidoRequest.Id} recebido com sucesso!!!");

            return Task.CompletedTask;
        }
    }
}
