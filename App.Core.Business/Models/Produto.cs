namespace App.Core.Business.Models
{
    public class Produto : Entity
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public string? Imagem { get; set; }
        public decimal Preco { get; set; }
    }
}
