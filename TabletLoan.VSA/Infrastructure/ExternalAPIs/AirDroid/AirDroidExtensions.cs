using Refit;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.BackgroundJobs;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.BackgroundJobs.Implementation;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.DTOs.Request;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid;

public static class AirDroidExtensions
{
    public static InstantBlockRequest ToRequest(
        string pToken, string deviceId, string cDeviceId, int enabled, int limitTime, int appVersion)
    {
        return new InstantBlockRequest(
            Ptoken: pToken,
            Account_id: 75749899,
            Device_id: deviceId,
            Timestamp: DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Device_type: 75,
            App_version: appVersion,
            Language: "es-es",
            Nonce: DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 10,
            C_device_id: cDeviceId,
            Enabled: enabled,
            Limit_time: limitTime
        );
    }

    public static IServiceCollection AddAirDroidClients(
        this IServiceCollection services, IConfiguration configuration)
    {
        var airdroidBlockUrl = configuration.GetValue<string>("AirDroid:BlockBaseUrl");
        var airdroidAuthUrl = configuration.GetValue<string>("AirDroid:AuthBaseUrl");

        services.AddRefitClient<IAirDroidParentalClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(airdroidBlockUrl!));

        services.AddRefitClient<IAirDroidAuthClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(airdroidAuthUrl!));

        return services;
    }

    public static IServiceCollection RegisterAirDroidAuthJobs(this IServiceCollection services)
    {
        services.AddScoped<IRefreshAirDroidTokenJob, RefreshAirDroidTokenJob>();
        services.AddScoped<ISignOutAirDroidJob, SignOutAirDroidJob>();

        return services;
    }
}
