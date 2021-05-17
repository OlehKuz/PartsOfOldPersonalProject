using Common;
using Common.Enums;
using Common.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthorizationWithPolicies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.AdministratorPolicy, policyBuilder =>
                { 
                    policyBuilder.RequireAuthenticatedUser(); 
                    policyBuilder.RequireRole(EnumExtentions.GetEnumValueName(Role.AdministratorRole));
                });
                     
                config.AddPolicy(Policies.BeautyMasterPolicy, policyBuilder =>
                { 
                    policyBuilder.RequireAuthenticatedUser(); 
                    policyBuilder.RequireRole(EnumExtentions.GetEnumValueName(Role.BeautyMasterRole));
                });
                config.AddPolicy(Policies.MarketologPolicy, policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser(); 
                    policyBuilder.RequireRole(EnumExtentions.GetEnumValueName(Role.MarketologRole));
                });
                config.AddPolicy(Policies.CustomerPolicy, policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser();
                    policyBuilder.RequireRole(EnumExtentions.GetEnumValueName(Role.CustomerRole));
                });//ServiceUpdateAuthorizationHandler
            });
            return serviceCollection;
        }
    }
}