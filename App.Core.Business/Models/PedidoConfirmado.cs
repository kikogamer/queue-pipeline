namespace App.Core.Business.Models
{
    public class PedidoConfirmado : PedidoState
    {
        public override void Processar(Pedido pedido)
        {
            throw new InvalidOperationException("O pedido já está confirmado!");
        }
    }
}
