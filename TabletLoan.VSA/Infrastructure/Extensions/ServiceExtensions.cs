using Hangfire;
using Hangfire.PostgreSql;

namespace TabletLoan.VSA.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureHangFire(this IServiceCollection services,
        IConfiguration configuration) 
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(options => options.UseNpgsqlConnection(connectionString)));

        services.AddHangfireServer();

        return services;
    }

    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy("AllowReact", builder =>
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

        return services;
    }
}