using System;
using System.IO;
using System.Reflection;
using Infrastructure.Extensions;
using Microsoft.OpenApi.Models;
using Infrastructure.Interfaces;
using Infrastructure.Utils.ExceptionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using RabbitMq;

namespace Infrastructure
{
    public abstract class BaseStartup
    {
        protected BaseStartup(IHostEnvironment hostEnvironment)
        {
            HostEnvironment = hostEnvironment;

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                 .AddJsonFile("runsettings.json", true, true)
                .AddJsonFile($"runsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                    optional: true, true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                    optional: true, true)
                .AddEnvironmentVariables();

            Configuration = config.Build();
        }
        protected IConfiguration Configuration { get; }
        protected IHostEnvironment HostEnvironment { get; }
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Whatever", Version = "v1" });
            });
            services.AddMvc(options =>
            {
                var authorizationPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(authorizationPolicy));
                options.Filters.Add<OperationCancelledExceptionFilter>();
            });
            services.AddAuthorizationWithPolicies();
            //services.AddRabbitMessageBus();
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "doesntMatterWhat");
                c.RoutePrefix = string.Empty;
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
        }
    }
}