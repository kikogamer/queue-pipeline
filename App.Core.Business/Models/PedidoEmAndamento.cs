using App.Core.Business.Contracts;

namespace App.Core.Business.Models
{
    public class PedidoEmAndamento : IPedidoState
    {
        public IPedidoState Processar(Pedido pedido)
        {
            Console.WriteLine($"Consultando saldo em estoque para o pedido: {pedido.Numero}.");
            Console.WriteLine($"Saldo em estoque disponível para o pedido: {pedido.Numero}.");
            Console.WriteLine($"O Pedido {pedido.Numero} foi criado com sucesso, enviando para processar o pagamento!");

            return new PedidoConfirmado();
        }
    }
}
