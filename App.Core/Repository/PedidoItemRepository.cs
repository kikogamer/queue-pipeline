using App.Core.Business.Contracts;
using App.Core.Business.Models;
using App.Core.Data;

namespace App.Core.Repository
{
    public class PedidoItemRepository : Repository<PedidoItem>, IPedidoItemRepository
    {
        public PedidoItemRepository(MeuDbContext db) : base(db)
        {
        }
    }
}
