// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Payment.Persistance;

namespace Payment.Persistance.Migrations
{
    [DbContext(typeof(LoggingDbContext))]
    partial class LoggingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Payment.Domain.StageLog", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("FunctionToCall")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FunctionToRestore")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Input")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Output")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("ProcessState")
                        .HasColumnType("int");

                    b.Property<bool>("ShouldWaitForPreviousStageToComplete")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("TaskDeploymentLogEntryTransactionId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("TaskDeploymentLogEntryTransactionId");

                    b.ToTable("StageLog");
                });

            modelBuilder.Entity("Payment.Domain.TaskDeploymentLogEntry", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("CompleteByUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EventName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("FailureCount")
                        .HasColumnType("int");

                    b.Property<Guid?>("LockedBy")
                        .HasColumnType("char(36)");

                    b.Property<int>("ProcessState")
                        .HasColumnType("int");

                    b.Property<int>("UnderlyingEntityId")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.ToTable("LogEntryOfDeploymentPlanExecution");
                });

            modelBuilder.Entity("Payment.Domain.StageLog", b =>
                {
                    b.HasOne("Payment.Domain.TaskDeploymentLogEntry", null)
                        .WithMany("TaskStages")
                        .HasForeignKey("TaskDeploymentLogEntryTransactionId");
                });
#pragma warning restore 612, 618
        }
    }
}
