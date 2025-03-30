using System.ComponentModel.DataAnnotations;

namespace WebApp.Server.ViewModels
{
    public class PedidoViewModel
    {
        [Required]
        public string? Cliente { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Endereco { get; set; }
        public string? Complemento { get; set; }
        [Required]
        public string? Cidade { get; set; }
        [Required]
        public string? Estado { get; set; }
        [Required]
        public string? Pais { get; set; }
        [Required]
        public string? Cep { get; set; }
        public string? Telefone { get; set; }
        public decimal Frete { get; set; }
        public decimal Imposto { get; set; }
        public List<PedidoItemViewModel> Itens { get; set; }
        public PedidoViewModel()
        {
            Itens = new List<PedidoItemViewModel>();
        }
    }
}
