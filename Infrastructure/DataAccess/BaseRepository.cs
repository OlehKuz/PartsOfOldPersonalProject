using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    public abstract class BaseRepository<TEntity, TKey>:IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        public abstract IUnitOfWork UnitOfWork { get; protected set; }

        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _entities;

        protected BaseRepository(DbContext context)
        {
            _dbContext = context;
            _entities = _dbContext.Set<TEntity>();
        }

        public async Task<bool> ExistsAsync(TKey key,CancellationToken cancellationToken)
        {
            var result = await _entities.FindAsync(key,cancellationToken);
            return result != null;
        }

        public virtual Task<TEntity[]> GetAllAsync(CancellationToken cancellationToken)
        {
            return _entities.AsNoTracking().ToArrayAsync(cancellationToken);
        }

        public Task<TEntity[]> GetAsync(Expression<Func<TEntity, bool>> expression,CancellationToken cancellationToken)
        {
            return _entities.Where(expression).AsNoTracking().ToArrayAsync(cancellationToken);
        }

        public virtual Task<TEntity> GetAsync(TKey key, CancellationToken cancellationToken)
        {
            return _entities.AsNoTracking().FirstOrDefaultAsync(item => item.Id.Equals(key), cancellationToken);
        }

        public TEntity Insert(TEntity item)
        {
           return _entities.Add(item).Entity;
        }

        public void InsertMany(IEnumerable<TEntity> items)
        {
            _entities.AddRange(items);
        }

        public TEntity Update(TEntity item)
        {
            return _entities.Update(item).Entity;
        }

        public bool Remove(TEntity item)
        {
            var result = _entities.Remove(item);
            return result.State == EntityState.Deleted;
        }

        public virtual async Task<bool> RemoveAsync(TKey key)
        {
            var item = await _entities.FindAsync(key);
            if (item == null)
                return false;

            var result = _entities.Remove(item);
            return result.State == EntityState.Deleted;
        }

    }
}