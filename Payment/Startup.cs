using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Common.Enums;
using Common.Messages;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Payment.IntegrationEvents.Events;
using Payment.Persistance;
using Payment.Services;
using Payment.Services.DeploymentPlans;
using RabbitMq;
using AutoMapper;
using Payment.IntegrationEvents.IntegrationEventsHandling;

namespace Payment
{
    public class Startup//:BaseStartup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected IConfiguration Configuration { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddRabbitMessageBus();
            services.AddDbContext<OrderPaymentDbContext>(opt =>
                opt.UseMySql(Configuration.GetConnectionString("MysqlConnection")));
            services.AddDbContext<LoggingDbContext>(opt =>
                opt.UseMySql(Configuration.GetConnectionString("MysqlConnection")));
            services.Configure<RabbitMQOptions>(options => Configuration.GetSection("RabbitMQ").Bind(options));
           // base.ConfigureServices(services);
           services.AddSingleton<WorkflowService>();
           //services.AddScoped<IntegrationEventLogger>();
            services.AddTransient<DatabaseTransaction>();
            services.AddTransient<OrderPaymentIntegrationEventService>();
            services.AddScoped<OrderingService>();
            services.AddScoped<IDeploymentPlan,OrderPlacedDeploymentPlan>();
            services.AddScoped<IMessageHandler<BookingPlacedMessage>, BookingPlacedMessageHandler>();
            services.AddTransient<Func<DbConnection, IntegrationEventLogger>>(
                sp => (DbConnection c) => new IntegrationEventLogger(c));
            //services.AddSingleton<IDesignTimeDbContextFactory<OrderDbContext>, OrderDbContextFactory>(); check if it works , becuse most that that it doesnt
            //TODO either in startup or in scheduler(hosted service) subscribe for all possible messages that this project can receive, then in each deployment plan add observer that the rabbit mq for message with specific transaction id
            // TODO only messages that come from booking service (like placeorder, cancelorder, startpayment) should have handlers, and those handlers should just insert it in db and create log. then the respective service for that event handles all the logic to interact with remote services
            //TaskDeploymentPlan has only stages that require remote interaction
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IServiceProvider serviceProvider)
        {
        
            var messageBus = app.ApplicationServices.GetRequiredService<IMessageBus>();
            var name = typeof(BookingPlacedMessage).ToString();
            messageBus.Subscribe<BookingPlacedMessage>(serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IMessageHandler<BookingPlacedMessage>>(), $"exc.{name}", RabbitExchangeType.DirectExchange, name,false);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
                endpoints.MapControllers();
            });
        }
        
    }
}