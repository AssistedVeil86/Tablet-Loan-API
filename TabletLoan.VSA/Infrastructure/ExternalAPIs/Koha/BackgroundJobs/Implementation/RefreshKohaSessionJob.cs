using Microsoft.Extensions.Options;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.DTOs.Request;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.Options;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.BackgroundJobs.Implementation;

public class RefreshKohaSessionJob(
    IKohaClient kohaClient,
    ILogger<RefreshKohaSessionJob> logger,
    KohaSessionManager sessionManager,
    IOptionsSnapshot<KohaOptions> options) : IRefreshKohaSessionJob
{
    public async Task ExecuteLoginAsync()
    {
        var request = new KohaLoginRequest(
            options.Value.UserId,
            options.Value.Password,
            "intranet");

        try
        {
            var response = await kohaClient.LoginAsync(request);

            if (response.Success && !string.IsNullOrEmpty(response.Cookie))
            {
                sessionManager.SetCookie(response.Cookie);
                logger.LogInformation("Sesión de Koha iniciada exitosamente.");
            }
            else
            {
                logger.LogError("Fallo al iniciar sesión en Koha. Error: {Error}", response.Error);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error de red al intentar conectar con la API de Koha.");
            throw;
        }
    }
}
