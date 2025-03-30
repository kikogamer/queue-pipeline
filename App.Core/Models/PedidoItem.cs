namespace App.Core.Models
{
    public class PedidoItem : Entity
    {
        public Guid PedidoId { get; set; }
        public Pedido? Pedido { get; set; }
        public Guid ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get => Quantidade * Produto?.Preco ?? 0; }
    }
}
