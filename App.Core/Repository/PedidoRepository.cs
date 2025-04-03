using App.Core.Business.Contracts;
using App.Core.Business.Models;
using App.Core.Data;

namespace App.Core.Repository
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(MeuDbContext db) : base(db)
        {
        }
    }
}
