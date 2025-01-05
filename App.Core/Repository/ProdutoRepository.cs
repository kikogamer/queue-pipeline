using App.Core.Data;
using App.Core.Interfaces;
using App.Core.Models;

namespace App.Core.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MeuDbContext db) : base(db)
        {
        }
    }
}
