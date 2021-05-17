using System;
using System.Data.Common;
using System.Threading.Tasks;
using AutoMapper;
using Common.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Payment.Domain;
using Payment.Persistance;
using Payment.Services.DeploymentPlans;

namespace Payment.Services
{

    // THIS SERVICE IS ALL OBSOLETE, BEFORE I KNEW ANY ARCHITECTURE DESIGN PATTERNS. PROBABLY WOULD CREATE
    // CHAIN OF RESPONSIBILITY INSTEAD OF THIS OrderPaymentIntegrationEventService 


    // this service is only for incoming events from UPSTREAM SERVICES (LIKE Booking platform project). AIM is to save event from user, and then other service has
    // a workflow and error handling logic, scheduler and supervisor that makes sure event is handled accordingly between multiple remote services
    public class OrderPaymentIntegrationEventService//TODO throw error in transaction to see if it saves order without log, or removes order. Then remove unnecessary code related to IMessageHandler
    {
        private IntegrationEventLogger _integrationEventLogger;
        private DatabaseTransaction _databaseTransaction;
        private readonly IServiceScopeFactory _scopeFactory;
        private OrderPaymentDbContext _context;

        private readonly Func<DbConnection, IntegrationEventLogger> _integrationEventLoggerFactory;

        public OrderPaymentIntegrationEventService(DatabaseTransaction databaseTransaction,OrderPaymentDbContext context,
           Func<DbConnection, IntegrationEventLogger> integrationEventLoggerFactory,IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _context = context;
            _integrationEventLoggerFactory = integrationEventLoggerFactory;
            _integrationEventLogger = _integrationEventLoggerFactory(_context.Database.GetDbConnection());       
            _databaseTransaction = databaseTransaction;
            _scopeFactory = scopeFactory;
        }
        

        public async Task ExecutePaymentOrderRelatedTransaction<TDbContext>(Func<Task> command, IMessage message, Order order)
            where TDbContext : DbContext
        {
            //var deploymentPlan = _workflowService.GetDeploymentPlanForTask(message);
            var deploymentPlan = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IDeploymentPlan>();
            if (deploymentPlan != null)
            {
                await _databaseTransaction.ExecuteTransactionAsync<TDbContext>(async()=>//<TDbContext>(async () =>
                {
                    await command();
                    //deploymentPlan.UnderlyingEntityId = 0; //orderId from command
                   
                     _integrationEventLogger.LogDeploymentPlan(deploymentPlan, _context.Database.CurrentTransaction);
                });
            }
        }
    }
}