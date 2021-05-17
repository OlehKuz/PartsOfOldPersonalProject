using System.IdentityModel.Tokens.Jwt;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchedulingServices.Persistance;
using AutoMapper;
using Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;

namespace SchedulingServices
{
    public class Startup:BaseStartup
    {
        
        public Startup(IHostEnvironment hostEnvironment) : base(hostEnvironment)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.AddDbContext<SchedulingDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("MysqlConnection")));
            services.AddAutoMapper(typeof(Startup));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) 
                .AddJwtBearer(options =>
                {
                    options.Authority = ApiUrls.IdentityServerUrl;

                    // name of the API resource
                    options.Audience = Scopes.SchedulingServices; 
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters.NameClaimType = "name";
                    options.TokenValidationParameters.RoleClaimType = "role";
                    // Microsoft to jwtclaim mapping  https://leastprivilege.com/2016/08/21/why-does-my-authorize-attribute-not-work/
                });
            
            //or can do AddIdentityServerAuthentication which adds jwt bearer and some services on top of it 
            IdentityModelEventSource.ShowPII = true;
            //   services.AddTransient<IClaimsTransformation, UserClaimsTransformer>(); didnt really need to use it yet 
        }
        
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app,env);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/hello", async context => { await context.Response.WriteAsync("Hello World!"); });
                endpoints.MapControllers();
            });
        }
    }
}