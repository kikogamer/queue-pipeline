namespace App.Core.Business.Models
{
    public abstract class PedidoState
    {
        public abstract void EmitirNotaFiscal(Pedido pedido);
        public abstract void Processar(Pedido pedido);
        public abstract void ProcessarPagamento(Pedido pedido);
    }
}
