using System;
using System.IO;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Payment.Persistance
{
    public class OrderDbContextFactory //: BaseDbContextFactory<OrderPaymentDbContext>
    {
        public OrderPaymentDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).Build();
            var optionsBuilder =  new DbContextOptionsBuilder<DbContext>()
                .UseMySql(configuration.GetConnectionString("MysqlConnection"));
            return (OrderPaymentDbContext) Activator.CreateInstance(typeof(OrderPaymentDbContext), optionsBuilder.Options);
        }

        public OrderPaymentDbContext CreateDbContext()
        {
            return CreateDbContext(new string[] { });
        }
    }
}