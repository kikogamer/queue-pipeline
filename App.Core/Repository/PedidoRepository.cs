using App.Core.Data;
using App.Core.Interfaces;
using App.Core.Models;

namespace App.Core.Repository
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(MeuDbContext db) : base(db)
        {
        }
    }
}
