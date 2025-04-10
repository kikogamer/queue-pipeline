namespace App.Core.Business.Models
{
    public class PedidoEntregaIniciada : PedidoState
    {
        public override void EmitirNotaFiscal(Pedido pedido)
        {
            throw new InvalidOperationException("O pedido já teve sua nota fiscal emitida!");
        }

        public override void EncerrarEntrega(Pedido pedido)
        {
            Console.WriteLine($"Encerrando Entrega para o pedido: {pedido.Numero}.");
            Console.WriteLine($"Entrega encerrada com sucesso para o pedido {pedido.Numero}!");
            Console.WriteLine("Pedido foi finalizado!");

            pedido.Status = new PedidoFinalizado();
        }

        public override void IniciarEntrega(Pedido pedido)
        {
            throw new InvalidOperationException("Entrega do Pedido já foi iniciada!");
        }

        public override void Processar(Pedido pedido)
        {
            throw new InvalidOperationException("O pedido já foi confirmado!");
        }

        public override void ProcessarPagamento(Pedido pedido)
        {
            throw new InvalidOperationException("O pedido já foi pago!");
        }
    }
}
