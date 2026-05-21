using ErrorOr;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.Errors;

public static class KohaErrors
{
    public static Error KohaNoSessionError(string message) =>
        Error.Unexpected("Koha.NoSession", message);

    public static Error KohaSessionExpired(string message) =>
        Error.Unauthorized("Koha.SessionExpired", message);

    public static Error KohaPatronNotFound(string message) =>
        Error.NotFound("Koha.PatroNotFound", message);

}
