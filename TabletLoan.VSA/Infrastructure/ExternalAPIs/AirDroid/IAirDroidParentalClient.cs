using Refit;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.DTOs.Request;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.DTOs.Response;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid;

public interface IAirDroidParentalClient
{
    [Post("/web/userule/saveInstantBlock")]
    Task<AirDroidBlockResponse> BlockTabletAsync([Body] InstantBlockRequest req);
    [Post("/web/user/signout")]
    Task<AirDroidSignOutResponse> SignOutAsync([Body] AirDroidSignOutRequest req);
}
