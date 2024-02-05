using Core.Entities.Base;
using System.Linq.Expressions;

namespace Repository.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> GetByIdAsync(int id);
        IQueryable<TEntity?> GetByIdQueryableAsync(int id);
        IQueryable<TEntity> GetAll();
        Task InsertAsync(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
