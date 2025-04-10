namespace App.Core.Business.Models
{
    public class PedidoEmAndamento : PedidoState
    {
        public override void EmitirNotaFiscal(Pedido pedido)
        {
            throw new InvalidOperationException("O pedido ainda não foi confirmado!");
        }

        public override void EncerrarEntrega(Pedido pedido)
        {
            throw new InvalidOperationException("O pedido ainda não teve sua entrega iniciada!");
        }

        public override void IniciarEntrega(Pedido pedido)
        {
            throw new InvalidOperationException("O pedido ainda não teve sua nota fiscal emitida!");
        }

        public override void Processar(Pedido pedido)
        {
            Console.WriteLine($"Consultando saldo em estoque para o pedido: {pedido.Numero}.");
            Console.WriteLine($"Saldo em estoque disponível para o pedido: {pedido.Numero}.");
            Console.WriteLine($"O Pedido {pedido.Numero} foi criado com sucesso, enviando para processar o pagamento!");

            pedido.Status = new PedidoConfirmado();
        }

        public override void ProcessarPagamento(Pedido pedido)
        {
            throw new InvalidOperationException("O pedido ainda não foi confirmado!");
        }
    }
}
