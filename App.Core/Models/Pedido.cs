﻿using System.ComponentModel.DataAnnotations.Schema;

namespace App.Core.Models
{
    public class Pedido : Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Numero { get; set; }
        public string? Cliente { get; set; }
        public DateTimeOffset Data { get; set; }
        public string? Email { get; set; }
        public string? Endereco { get; set; }
        public string? Complemento { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? Pais { get; set; }
        public string? Cep { get; set; }
        public string? Telefone { get; set; }
        public decimal Frete { get; set; }
        public decimal Imposto { get; set; }
        public List<PedidoItem> Itens { get; set; }

        public Pedido()
        {
            Data = DateTime.Now;
            Itens = new List<PedidoItem>();
        }
    }
}
