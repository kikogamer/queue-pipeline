namespace App.Core.Business.Models
{
    public class PedidoNotaFiscalEmitida : PedidoState
    {
        public override void EmitirNotaFiscal(Pedido pedido)
        {
            throw new InvalidOperationException("O pedido já teve sua nota fiscal emitida!");
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
