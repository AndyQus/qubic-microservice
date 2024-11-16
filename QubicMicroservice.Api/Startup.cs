using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;
using StackExchange.Redis;

using QubicMicroservice.Application.Interfaces;
using QubicMicroservice.Application.Services;
using QubicMicroservice.Domain.Interfaces;
using QubicMicroservice.Infrastructure.Data;
using QubicMicroservice.Infrastructure.Messaging;
using QubicMicroservice.Infrastructure.Repositories;

namespace QubicMicroservice.Api.WebAPI;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<MongoDBSettings>(Configuration.GetSection("MongoDBSettings"));
        services.AddSingleton<MongoDBContext>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ITransactionService, TransactionService>();

        var redisConfig = new ConfigurationOptions
        {
            EndPoints = { Environment.GetEnvironmentVariable("REDIS_ENDPOINT") },
            User = Environment.GetEnvironmentVariable("REDIS_USER"),
            Password = Environment.GetEnvironmentVariable("REDIS_PASSWORD"),
            Ssl = bool.TryParse(Environment.GetEnvironmentVariable("REDIS_SSL"), out var ssl) && ssl,
            AbortOnConnectFail = false
        };
        // Redis Pub/Sub
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConfig));
        services.AddScoped<IPubSubClient, PubSubClient>();
        services.AddHostedService<PubSubBackgroundService>();

        services.AddHttpClient();

        services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "QubicMicroservice API",
                Version = "v1",
                Description = "API documentation for the QubicMicroservice."
            });
        });

        services.AddHealthChecks();
    }

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "QubicMicroservice API V1");
                c.RoutePrefix = "swagger";
            });
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            // Health check endpoints
            endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
            {
                Predicate = _ => false // Simple liveness probe
            });

            endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains("ready")
            });
        });
    }
}
