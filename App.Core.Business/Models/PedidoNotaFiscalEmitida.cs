﻿namespace App.Core.Business.Models
{
    public class PedidoNotaFiscalEmitida : PedidoState
    {
        public override void EmitirNotaFiscal(Pedido pedido)
        {
            throw new InvalidOperationException("O pedido já teve sua nota fiscal emitida!");
        }

        public override void EncerrarEntrega(Pedido pedido)
        {
            throw new InvalidOperationException("O pedido ainda não teve sua entrega iniciada!");
        }

        public override void IniciarEntrega(Pedido pedido)
        {
            Console.WriteLine($"Iniciando Entrega para o pedido: {pedido.Numero}.");
            Console.WriteLine($"Entrega iniciada com sucesso para o pedido {pedido.Numero}!");

            pedido.Status = new PedidoEntregaIniciada();
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
