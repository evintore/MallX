using Core.Entities.Base;
using Data;
using Microsoft.EntityFrameworkCore;
using Repository.Abstract;
using System.Linq.Expressions;

namespace Repository.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.CountAsync(predicate);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<TEntity?> GetByIdQueryableAsync(int id)
        {
            return _dbSet.AsQueryable();
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
