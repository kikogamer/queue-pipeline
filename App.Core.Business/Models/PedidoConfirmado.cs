namespace App.Core.Business.Models
{
    public class PedidoConfirmado : PedidoState
    {
        public override void EmitirNotaFiscal(Pedido pedido)
        {
            throw new InvalidOperationException("O pedido ainda não foi pago!");
        }

        public override void Processar(Pedido pedido)
        {
            throw new InvalidOperationException("O pedido já está confirmado!");
        }

        public override void ProcessarPagamento(Pedido pedido)
        {
            Console.WriteLine($"Processando Pagamento para o pedido: {pedido.Numero}.");
            Console.WriteLine($"Pagamento efetuado com sucesso para o pedido {pedido.Numero}!");

            pedido.Status = new PedidoPago();
        }
    }
}
