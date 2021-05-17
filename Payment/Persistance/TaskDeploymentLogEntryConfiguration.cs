using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payment.Domain;

namespace Payment.Persistance
{
    public class TaskDeploymentLogEntryConfiguration:IEntityTypeConfiguration<TaskDeploymentLogEntry>
    {
        public void Configure(EntityTypeBuilder<TaskDeploymentLogEntry> builder)
        {
            builder.HasKey(e => e.TransactionId);
        }
    }
}