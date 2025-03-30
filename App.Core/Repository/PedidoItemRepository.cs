using App.Core.Data;
using App.Core.Interfaces;
using App.Core.Models;

namespace App.Core.Repository
{
    public class PedidoItemRepository : Repository<PedidoItem>, IPedidoItemRepository
    {
        public PedidoItemRepository(MeuDbContext db) : base(db)
        {
        }
    }
}
