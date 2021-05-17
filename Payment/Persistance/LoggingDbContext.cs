using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payment.Domain;

namespace Payment.Persistance
{
    public class LoggingDbContext:DbContext, IUnitOfWork
    {
        public LoggingDbContext(DbContextOptions<LoggingDbContext> options):base(options){}
        
        public DbSet<TaskDeploymentLogEntry> LogEntryOfDeploymentPlanExecution { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new StageLogConfiguration());
            builder.ApplyConfiguration(new TaskDeploymentLogEntryConfiguration());
        }
    }
}