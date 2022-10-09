using EPiServer.Authorization;
using EPiServer.Shell.Modules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleOptimizelyAddon.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOptimizelyAddon
{
    public static class ServiceCollectionExtensions
    {
        
            private static readonly Action<AuthorizationPolicyBuilder> DefaultPolicy = p => p.RequireRole(Roles.WebAdmins);

            public static IServiceCollection AddOptimizelySampleAddOn(this IServiceCollection services)
            {
                return AddOptimizelySampleAddOn(services, _ => { }, DefaultPolicy);
            }

            public static IServiceCollection AddOptimizelySampleAddOn(
                this IServiceCollection services,
                Action<SampleOptimizelyAddonOptions> setupAction)
            {
                return AddOptimizelySampleAddOn(services, setupAction, DefaultPolicy);
            }

            public static IServiceCollection AddOptimizelySampleAddOn(
                this IServiceCollection services,
                Action<SampleOptimizelyAddonOptions> setupAction,
                Action<AuthorizationPolicyBuilder> configurePolicy)
            {
                AddModule(services);

                //TODO: Register services needed!


                services.AddOptions<SampleOptimizelyAddonOptions>().Configure<IConfiguration>((options, configuration) =>
                {
                    setupAction(options);
                    configuration.GetSection(Constants.ConfigSection).Bind(options);
                });

                services.AddAuthorization(options =>
                {
                    options.AddPolicy(Constants.PolicyName, configurePolicy);
                });

                return services;
            }

            private static void AddModule(IServiceCollection services)
            {
                services.Configure<ProtectedModuleOptions>(
                    pm =>
                    {
                        if (!pm.Items.Any(i => i.Name.Equals(Constants.ModuleName, StringComparison.OrdinalIgnoreCase)))
                        {
                            pm.Items.Add(new ModuleDetails { Name = Constants.ModuleName });
                        }
                    });
            }
        }
}
