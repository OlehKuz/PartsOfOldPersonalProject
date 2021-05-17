using System;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Payment.Persistance
{
    public class DatabaseTransaction
    {
        private readonly IServiceProvider _serviceProvider;
        public DatabaseTransaction(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ExecuteTransactionAsync<TDbContext>(Func<Task> action) where TDbContext:DbContext
        {
            var context = _serviceProvider.GetService<TDbContext>(); 
            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync( async () =>
            {
                using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    await action();
                    await transaction.CommitAsync();
                }
            });
        }
    }
}