using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IRepository<TEntity, in TKey> where TEntity:class, IEntity<TKey>
    {
        Task<bool> ExistsAsync(TKey key,CancellationToken cancellationToken);
        
        Task<TEntity[]> GetAllAsync(CancellationToken cancellationToken);
        Task<TEntity[]> GetAsync(Expression<Func<TEntity, bool>> expression,CancellationToken cancellationToken);
        Task<TEntity> GetAsync(TKey key,CancellationToken cancellationToken);
       
        TEntity Insert(TEntity item);
        void InsertMany(IEnumerable<TEntity> items);
        
        TEntity Update(TEntity item);
        
        bool Remove(TEntity item);
        Task<bool> RemoveAsync(TKey key);
        
        IUnitOfWork UnitOfWork { get; }
    }
}