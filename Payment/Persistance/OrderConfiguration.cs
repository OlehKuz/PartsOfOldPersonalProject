using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.Domain;

namespace Payment.Persistance
{
    public class OrderConfiguration:IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(o => o.Payment)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.PaymentsId)
                .OnDelete(DeleteBehavior.Restrict);//maybe need to setnull
            builder.Property(b => b.EndHour).IsRequired();
            builder.Property(b => b.StartHour).IsRequired();
            builder.Property(b => b.UnpayedOrderExpirationTime).IsRequired();
            builder.HasIndex(o => o.ClientsId);
            builder.HasIndex(o => o.ServicesId);
        }
    }
}