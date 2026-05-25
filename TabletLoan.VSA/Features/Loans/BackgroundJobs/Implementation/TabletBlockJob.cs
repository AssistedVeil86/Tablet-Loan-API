using Microsoft.Extensions.Options;
using TabletLoan.VSA.Infrastructure.Data;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.Options;

namespace TabletLoan.VSA.Features.Loans.BackgroundJobs.Implementation;

public class TabletBlockJob(
    AppDbContext context,
    IAirDroidParentalClient airDroidClient,
    ILogger<TabletBlockJob> logger,
    IOptionsSnapshot<AirDroidOptions> options,
    AirDroidTokenManager tokenManager) : ITabletBlockJob
{
    public async Task ExcuteTabletBlockAsync(int tabletId)
    {
        var tablet = await context.Tablets.FindAsync(tabletId);

        if (tablet is null)
        {
            logger.LogWarning("Hangfire tried to block the tablet with {TabletId} but it doesn't exist", tabletId);
            return;
        }

        var request = AirDroidExtensions.ToRequest(
            tokenManager.GetToken(),
            tablet.AirDroidDeviceId,
            tablet.AirDroidCDeviceId, 1, -2,
            options.Value.AppVersion);

        var response = await airDroidClient.BlockTabletAsync(request);

        if (response.Code == 1 && response.Msg == "success")
        {
            if (!tablet.IsAvailable)
            {
                tablet.UpdateAvailability();
                await context.SaveChangesAsync();
            }
            logger.LogInformation("Tablet {TabletId} bloqueada exitosamente por Hangfire.", tabletId);
        }
        else
        {
            throw new Exception($"Error Blocking The Tablet: {response.Msg}");
        }
    }
}