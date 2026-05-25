using ErrorOr;
using Hangfire;
using Microsoft.Extensions.Options;
using TabletLoan.VSA.Domain.Entities;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.BackgroundJobs;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.Errors;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.Options;

namespace TabletLoan.VSA.Infrastructure.Services;

public sealed class AirDroidTabletService(
    IAirDroidParentalClient airDroidClient,
    AirDroidTokenManager tokenManager,
    IOptionsSnapshot<AirDroidOptions> options,
    IBackgroundJobClient backgroundJobs
)
{
    public async Task<ErrorOr<Success>> UnlockTabletAsync(Tablet tablet)
    {
        var unlockRequest = AirDroidExtensions.ToRequest(
            tokenManager.GetToken(),
            tablet.AirDroidDeviceId,
            tablet.AirDroidCDeviceId, 0, 0,
            options.Value.AppVersion);

        try
        {
            var airDroidResponse = await airDroidClient.BlockTabletAsync(unlockRequest);

            if (airDroidResponse.Code != 1)
            {
                // Retry Login
                backgroundJobs.Enqueue<IRefreshAirDroidTokenJob>(
                    job => job.ExecuteSignInAsync());

                return AirDroidErrors.UnlockFailed("It wasn't possible to unblock the iPad");
            }

            return Result.Success;
        }
        catch (Exception ex)
        {
            return AirDroidErrors.NetworkError(ex.Message);
        }
    }
}
