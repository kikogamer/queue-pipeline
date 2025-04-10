namespace App.Core.Business.Models
{
    public class PedidoFinalizado : PedidoState
    {
        public override void EmitirNotaFiscal(Pedido pedido)
        {
            throw new InvalidOperationException("O pedido já teve sua nota fiscal emitida!");
        }

        public override void EncerrarEntrega(Pedido pedido)
        {
            throw new InvalidOperationException("O pedido já foi encerrado!");
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
