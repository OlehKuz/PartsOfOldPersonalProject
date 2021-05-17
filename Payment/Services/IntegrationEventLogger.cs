using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Payment.Domain;
using Payment.Persistance;
using Payment.Services.DeploymentPlans;

namespace Payment.Services
{
    public class IntegrationEventLogger
    {
        private readonly LoggingDbContext _loggingDbContext;
       // private readonly IMapper _mapper;

        public IntegrationEventLogger(DbConnection dbConnection)//LoggingDbContext loggingDbContext, IMapper    mapper,IServiceScopeFactory scopeFactory)
        {
           // _loggingDbContext = loggingDbContext;
          //  _mapper = mapper;
            _loggingDbContext = new LoggingDbContext(new DbContextOptionsBuilder<LoggingDbContext>().UseMySql(dbConnection).Options);//scopeFactory.CreateScope().ServiceProvider.GetService<LoggingDbContext>();
        }

        public void LogDeploymentPlan(IDeploymentPlan deploymentPlan, IDbContextTransaction underlyingEntityTransaction)
        {
            Console.WriteLine("LogDeploymentPlan start to add");
            TaskDeploymentLogEntry taskDeploymentLogEntry = _mapper.Map<TaskDeploymentPlan, TaskDeploymentLogEntry>();
            taskDeploymentLogEntry.UnderlyingEntityId = deploymentPlan.UnderlyingEntityId;           
            taskDeploymentLogEntry.TransactionId = underlyingEntityTransaction.TransactionId;
            var mappedStages=_mapper.Map<IEnumerable<Stage>, IEnumerable<StageLog>>(deploymentPlan.TaskDeploymentStages);
            taskDeploymentLogEntry.TaskStages = mappedStages;
            _loggingDbContext.Database.UseTransaction(underlyingEntityTransaction.GetDbTransaction());
            _loggingDbContext.LogEntryOfDeploymentPlanExecution.Add(taskDeploymentLogEntry);
           
            
             _loggingDbContext.SaveChanges();
             
             Console.WriteLine("LogDeploymentPlan added");
            // throw new IndexOutOfRangeException();
        }

    }
}