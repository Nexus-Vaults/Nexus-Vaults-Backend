using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.RPC.Web3;
using Nexus.Application.Services;
using Nexus.Infrastructure.Configuration;
using Nexus.Infrastructure.Persistence;
using Nexus.Infrastructure.Persistence.Concurrency;
using Org.BouncyCastle.Crypto.Digests;
using System.Security.Cryptography;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Util;

namespace Nexus.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {

        services.AddDbContext<IAppDbContext, AppDbContext>((provider, options) =>
        {
            var dbOptions = provider.GetRequiredService<DatabaseOptions>();
            options.UseNpgsql(dbOptions.AppConnectionString,
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
        });

        services.AddHangfire((provider, options)
         => options
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(provider.GetRequiredService<DatabaseOptions>().HangfireConnectionString, new PostgreSqlStorageOptions()
            {
                PrepareSchemaIfNecessary = true,
                EnableTransactionScopeEnlistment = true,
            }));

        services.AddHangfireServer();

        services.AddSingleton<Random>();
        services.AddSingleton<PropertyMerger>();
        services.AddSingleton(RandomNumberGenerator.Create());

        return services;
    }
}