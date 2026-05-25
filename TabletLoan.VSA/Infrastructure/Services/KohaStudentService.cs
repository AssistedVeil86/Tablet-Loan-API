using System.Net;
using ErrorOr;
using Hangfire;
using Refit;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.BackgroundJobs;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.DTOs.Request;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.DTOs.Response;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.Errors;

namespace TabletLoan.VSA.Infrastructure.Services;

public sealed class KohaStudentService(
    IKohaClient kohaClient,
    KohaSessionManager sessionManager,
    IBackgroundJobClient backgroundJobs)
{
    public async Task<ErrorOr<KohaPatron>> ValidateAndGetStudentAsync(string cif)
    {
        var activeCookie = sessionManager.GetCookie();

        if (string.IsNullOrEmpty(activeCookie))
        {
            //Retry Koha Login
            backgroundJobs.Enqueue<IRefreshKohaSessionJob>(
                job => job.ExecuteLoginAsync());

            return KohaErrors.KohaNoSessionError("No Active Session Found in Koha");
        }

        var kohaRequest = new KohaSearchPatronRequest(cif, activeCookie);

        try
        {
            // Intentamos consumir la API
            var kohaResponse = await kohaClient.SearchPatronAsync(kohaRequest);

            // Si Koha devuelve 200 OK, pero dice Success = false en el JSON
            if (!kohaResponse.Success)
            {
                return Error.Failure("Koha.ApiError", kohaResponse.Error ?? "Unknown Koha error");
            }

            if (kohaResponse.Patrons.Count == 0)
                return KohaErrors.KohaPatronNotFound($"Student with Id {cif} was not found.");

            return kohaResponse.Patrons.First();
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            return KohaErrors.KohaSessionExpired("Koha's Session has expired (HTTP 401).");
        }
        catch (ApiException ex)
        {
            return Error.Failure("Koha.NetworkError", $"Koha API failed with status: {ex.StatusCode}");
        }
        catch (Exception ex)
        {
            return Error.Unexpected("Koha.Unexpected", ex.Message);
        }
    }
}
