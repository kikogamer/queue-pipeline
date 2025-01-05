using App.Core.Models;
using System.Linq.Expressions;

namespace App.Core.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Add(TEntity entity);
        Task Delete(Guid id);
        Task<TEntity?> Get(Guid id);
        Task<List<TEntity>> GetAllAsync();
        Task<int> SaveChanges();
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        Task Update(TEntity entity);
    }
}
