using Common.Extensions;
using Hangfire;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Models;
using Nexus.Application;
using Nexus.Infrastructure;
using Nexus.WebAPI.Common.Serialization;

namespace Nexus.WebAPI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(Program.WebAssembly);
        services.AddAutoMapper(Program.ApplicationAssembly);
        services.AddAutoMapper(Program.InfrastructureAssembly);

        services.AddApplication(Configuration, options =>
        {
            options.UseServiceLevels = false;
            options.ValidateServiceLevelsOnInitialize = true;
            options.IgnoreIServiceWithoutLifetime = false;
        },
        Program.WebAssembly,
        Program.ApplicationAssembly,
        Program.InfrastructureAssembly);

        services.AddApplication();
        services.AddInfrastructure();

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddHttpContextAccessor();

        services.AddOutputCache();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(swaggerOptions =>
        {
            swaggerOptions.SwaggerDoc("Farsight-0.0.0", new OpenApiInfo()
            {
                Title = "Farsight API Documentation",
                Version = "0.0.0"
            });

            swaggerOptions.UseInlineDefinitionsForEnums();
            swaggerOptions.DescribeAllParametersInCamelCase();
        });

        services.Configure<JsonOptions>(options => options.SerializerOptions.Converters.Add(new BigIntegerHexConverter()));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void ConfigurePipeline(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseOutputCache();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHangfireDashboard();
    }

    public void ConfigureRoutes(IEndpointRouteBuilder routes)
    {
        routes.MapFallbackToFile("index.html");
    }
}