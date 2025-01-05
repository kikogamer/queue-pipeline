using Microsoft.EntityFrameworkCore;

namespace WebApp.Context
{
    public class MeuDbContext : DbContext
    {

        public MeuDbContext(DbContextOptions<MeuDbContext> options) : base(options) { }
    }
}
