using Refit;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Turnstile.Options;
namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.Turnstile;

public static class TurnstileExtensions
{
    public static IServiceCollection AddTurnstileClient(
        this IServiceCollection services, IConfiguration configuration)
    {
        var turnstileUrl = configuration.GetValue<string>("Turnstile:BaseUrl");

        services.AddOptions<TurnstileOptions>()
            .BindConfiguration(TurnstileOptions.SectionName);

        services.AddRefitClient<ITurnstileClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(turnstileUrl!));

        return services;
    }
}
