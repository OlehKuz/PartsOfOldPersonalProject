using Common.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Payment.Domain;

namespace Payment.Persistance
{
    public class StageLogConfiguration:IEntityTypeConfiguration<StageLog>
    {
        public void Configure(EntityTypeBuilder<StageLog> builder)
        {
            builder.Property(e => e.Input).HasConversion(v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<IMessage>(v));
            builder.Property(e => e.Output).HasConversion(v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<IMessage>(v));
        }
    }
}