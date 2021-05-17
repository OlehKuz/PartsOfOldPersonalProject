using System;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payment.Domain;

namespace Payment.Persistance
{
    public class OrderPaymentDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Domain.Payment> Payments { get; set; }

        public OrderPaymentDbContext(DbContextOptions<OrderPaymentDbContext> options):base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new OrderConfiguration());
        }
        
        public override void Dispose()
        {
            Console.WriteLine("We are at dispose!");
            base.Dispose();
         
        }
        
        public override ValueTask DisposeAsync()
        {
            Console.WriteLine("We are at dispose!");
            return base.DisposeAsync();
        }
    }
}