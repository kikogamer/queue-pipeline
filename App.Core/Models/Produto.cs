namespace App.Core.Models
{
    public class Produto : Entity
    {
        public string? Descricao { get; set; }
        public string? Imagem { get; set; }
        public decimal Preco { get; set; }
    }
}
