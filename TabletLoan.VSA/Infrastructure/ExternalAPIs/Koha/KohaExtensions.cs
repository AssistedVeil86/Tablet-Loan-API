using Refit;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.BackgroundJobs;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.BackgroundJobs.Implementation;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha;

public static class KohaExtensions
{
    public static IServiceCollection AddKohaClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        var kohaUrl = configuration.GetValue<string>("Koha:BaseUrl");

        services.AddRefitClient<IKohaClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(kohaUrl!));

        return services;
    }

    public static IServiceCollection RegisterKohaJobs(this IServiceCollection services)
    {
        services.AddScoped<IRefreshKohaSessionJob, RefreshKohaSessionJob>();
        return services;
    }
}
