using Microsoft.Extensions.Options;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.DTOs.Request;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.Options;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.BackgroundJobs.Implementation;

public class SignOutAirDroidJob(
    IAirDroidParentalClient airDroidClient,
    AirDroidTokenManager tokenManager,
    IOptionsSnapshot<AirDroidOptions> options,
    ILogger<SignOutAirDroidJob> logger

) : ISignOutAirDroidJob
{
    public async Task ExecuteSignOutAsync()
    {
        logger.LogInformation("Starting AirDroid Signout...");

        var currentToken = tokenManager.GetToken();

        if (string.IsNullOrEmpty(currentToken))
        {
            logger.LogInformation("No hay token activo en memoria. Se omite el SignOut.");
            return;
        }

        var request = new AirDroidSignOutRequest(
            Ptoken: currentToken,
            AccountId: 75749899,
            DeviceId: "ebb38a324c7d508e277ff16b5b2e0080",
            Timestamp: DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            DeviceType: 75,
            AppVersion: options.Value.AppVersion,
            Language: "es-es",
            Nonce: DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 10
        );

        var response = await airDroidClient.SignOutAsync(request);

        if (response.Code == 1 || response.Code == 40002)
        {
            tokenManager.SetToken(string.Empty);
            logger.LogInformation("Sesión de AirDroid cerrada y token eliminado de la memoria.");
        }
        else
        {
            logger.LogError("Error inesperado al cerrar sesión. Code: {Code}, Msg: {Msg}", response.Code, response.Msg);
        }
    }
}
