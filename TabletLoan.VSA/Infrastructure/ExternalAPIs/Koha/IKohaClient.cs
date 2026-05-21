using Refit;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.DTOs.Request;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.DTOs.Response;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha;

public interface IKohaClient
{
    [Post("/auth/login")]
    Task<KohaLoginResponse> LoginAsync([Body] KohaLoginRequest req);
    [Post("/patrons")]
    Task<KohaSearchPatronResponse> SearchPatronAsync([Body] KohaSearchPatronRequest req);
}
