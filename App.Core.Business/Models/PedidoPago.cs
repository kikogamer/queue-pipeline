﻿namespace App.Core.Business.Models
{
    public class PedidoPago : PedidoState
    {
        public override void EmitirNotaFiscal(Pedido pedido)
        {
            Console.WriteLine($"Emitindo Nota Fiscal o pedido: {pedido.Numero}.");
            Console.WriteLine($"Nota Fiscal emitida para o pedido {pedido.Numero}, enviando para a transportadora!");

            pedido.Status = new PedidoNotaFiscalEmitida();
        }

        public override void EncerrarEntrega(Pedido pedido)
        {
            throw new InvalidOperationException("O pedido ainda não teve sua entrega iniciada!");
        }

        public override void IniciarEntrega(Pedido pedido)
        {
            throw new InvalidOperationException("O pedido ainda não teve sua nota fiscal emitida!");
        }

        public override void Processar(Pedido pedido)
        {
            throw new InvalidOperationException("O pedido já está confirmado!");
        }

        public override void ProcessarPagamento(Pedido pedido)
        {
            throw new InvalidOperationException("O pedido já foi pago!");
        }
    }
}
