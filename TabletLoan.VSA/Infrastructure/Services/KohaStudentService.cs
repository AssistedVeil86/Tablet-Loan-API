using ErrorOr;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.DTOs.Request;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.DTOs.Response;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.Errors;

namespace TabletLoan.VSA.Infrastructure.Services;

public sealed class KohaStudentService(
    IKohaClient kohaClient,
    KohaSessionManager sessionManager)
{
    public async Task<ErrorOr<KohaPatron>> ValidateAndGetStudentAsync(string cif)
    {
        var activeCookie = sessionManager.GetCookie();

        if (string.IsNullOrEmpty(activeCookie))
            return KohaErrors.KohaNoSessionError("No Active Session Found in Koha");

        var kohaRequest = new KohaSearchPatronRequest(cif, activeCookie);
        var kohaResponse = await kohaClient.SearchPatronAsync(kohaRequest);

        if (!kohaResponse.Success)
        {
            if (kohaResponse.Error == "Sesión expirada")
                return KohaErrors.KohaSessionExpired("Koha's Session has expired");
        }

        if (kohaResponse.Patrons.Count == 0)
            return KohaErrors.KohaPatronNotFound($"Student with Id {cif} was not found.");

        return kohaResponse.Patrons.First();
    }
}
