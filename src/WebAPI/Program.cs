using AutoMapper;
using Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Nexus.Application;
using Nexus.Infrastructure;
using Nexus.Infrastructure.Persistence;
using Nexus.WebAPI.Common.Endpoint;
using Nexus.WebAPI.Common.Exceptions;
using Nexus.WebAPI.Configuration;
using System.Reflection;

namespace Nexus.WebAPI;

public class Program
{
    public const int ShutdownTimeout = 5000;

    public static readonly Assembly WebAssembly = Assembly.GetExecutingAssembly();
    public static readonly Assembly InfrastructureAssembly = Assembly.GetAssembly(typeof(InfrastructureAssemblyMarker)) ?? throw new InvalidOperationException("Infrastructure Assembly not found!");
    public static readonly Assembly ApplicationAssembly = Assembly.GetAssembly(typeof(ApplicationAssemblyMarker)) ?? throw new InvalidOperationException("Application Assembly not found!");

    public static Startup Startup { get; private set; } = null!;
    public static WebApplication Application { get; private set; } = null!;
    public static IServiceProvider Provider { get; private set; } = null!;
    public static ILogger Logger { get; private set; } = null!;

    public static async Task Main(string[] args)
    {
        Application = CreateApplication(args);
        Provider = Application.Services;
        Logger = Application.Services.GetRequiredService<ILogger<Startup>>();

#if DEBUG
        EnsureValidMapperConfiguration();
#endif
        RegisterEndpoints();

        //await MigrateDatabaseAsync();

        await Provider.InitializeApplicationAsync(WebAssembly);
        await Provider.InitializeApplicationAsync(InfrastructureAssembly);
        await Provider.InitializeApplicationAsync(ApplicationAssembly);

        Provider.RunApplication(WebAssembly);
        Provider.RunApplication(InfrastructureAssembly);
        Provider.RunApplication(ApplicationAssembly);

        string url = GetListenUrl();
        Application.Run(url);
    }

    static WebApplication CreateApplication(string[] args)
    {
        var webApp = WebApplication.CreateBuilder(args);

        webApp.Logging.AddConsole();
        webApp.Configuration.AddJsonFile("appsettings.json", false);

        Startup = new Startup(webApp.Configuration);
        Startup.ConfigureServices(webApp.Services);

        var host = webApp.Build();

        Startup.ConfigurePipeline(host, webApp.Environment);
        Startup.ConfigureRoutes(host);

        return host;
    }

    static string GetListenUrl()
    {
        var bindingOptions = Provider.GetRequiredService<BindingOptions>();
        return $"http://{bindingOptions.BindAddress}:{bindingOptions.ApplicationPort}";
    }

    static void RegisterEndpoints()
    {
        var endpointTypes = WebAssembly.GetHttpEndpointTypes();

        foreach (var endpointType in endpointTypes)
        {
            var endpoint = (IRouteOwner)Provider.GetRequiredService(endpointType);
            endpoint.RegisterRoute(Application);
        }
    }

    public static async Task MigrateDatabaseAsync()
    {
        Logger!.LogInformation("Migrating database...");
        using var scope = Provider!.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await dbContext.Database.MigrateAsync();
        await dbContext.DisposeAsync();
        Logger!.LogInformation("Database Migration complete!");
    }

    public static void EnsureValidMapperConfiguration()
    {
        var mapper = Provider!.GetRequiredService<IMapper>();
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    public static async Task ShutdownAsync()
    {
        var lifetime = Provider!.GetRequiredService<IHostLifetime>();
        using var cts = new CancellationTokenSource(ShutdownTimeout);
        await lifetime.StopAsync(cts.Token);
        Environment.Exit(0);
    }
}