using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DataAccess
{
    public class BaseDbContextFactory<DContext>:IDesignTimeDbContextFactory<DContext> where DContext:DbContext
    {
        public DContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).Build();
            var optionsBuilder =  new DbContextOptionsBuilder<DbContext>()
                .UseMySql(configuration.GetConnectionString("MysqlConnection"));
            return (DContext) Activator.CreateInstance(typeof(DContext), optionsBuilder.Options);
        }

        public DContext CreateDbContext()
        {
            return CreateDbContext(new string[] { });
        }
    }
}