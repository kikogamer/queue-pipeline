using App.Core.Business.Contracts;
using App.Core.Business.Models;
using App.Core.Data;

namespace App.Core.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MeuDbContext db) : base(db)
        {
        }
    }
}
