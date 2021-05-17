using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    public static class QuaryableExtentions
    {
        public static async Task<PagingResponse<TEntity>> PaginateAsync<TEntity>(this IQueryable<TEntity> source, IPaging paging, CancellationToken cancellationToken) where TEntity : IEntity
        {
            var count = await source.CountAsync(cancellationToken);
            var paginated = await source.Skip((paging.PageIndex ?? 1 - 1) * paging.PageSize ?? 5).Take(paging.PageSize ?? 5).ToArrayAsync(cancellationToken);
            return new PagingResponse<TEntity>(paginated, count);
        }
    }
}