using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Utils
{
    public static class WebHostCustomExtensions
    {
        public static async Task<IHost> MigrateContextAsync<TDbContext>(this IHost host) where TDbContext:DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var logger = serviceProvider.GetRequiredService<ILogger<TDbContext>>();
                var context = serviceProvider.GetRequiredService<TDbContext>();
                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}",
                        typeof(TDbContext).Name);
                    await RetryHelper.RetryAttempt(async ()=>await context.Database.MigrateAsync(), $"{typeof(TDbContext)} migration ", CancellationToken.None);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TDbContext).Name);
                }

                return host;
            }
        }
    }
}