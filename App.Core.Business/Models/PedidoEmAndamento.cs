namespace App.Core.Business.Models
{
    public class PedidoEmAndamento : PedidoState
    {
        public override void Processar(Pedido pedido)
        {
            Console.WriteLine($"Consultando saldo em estoque para o pedido: {pedido.Numero}.");
            Console.WriteLine($"Saldo em estoque disponível para o pedido: {pedido.Numero}.");
            Console.WriteLine($"O Pedido {pedido.Numero} foi criado com sucesso, enviando para processar o pagamento!");

            pedido.Status = new PedidoConfirmado();
        }
    }
}
