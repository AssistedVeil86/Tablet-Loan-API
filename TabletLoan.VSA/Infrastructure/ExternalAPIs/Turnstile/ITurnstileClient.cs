using TabletLoan.VSA.Infrastructure.ExternalAPIs.Turnstile.DTOs;
using Refit;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.Turnstile;

public interface ITurnstileClient
{
    [Post("/siteverify")]
    Task<TurnstileResponse> VerifyAsync([Body] TurnstileRequest req);
}