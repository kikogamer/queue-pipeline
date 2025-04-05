namespace App.Core.Business.Models
{
    public abstract class PedidoState
    {
        public abstract void Processar(Pedido pedido);
    }
}
