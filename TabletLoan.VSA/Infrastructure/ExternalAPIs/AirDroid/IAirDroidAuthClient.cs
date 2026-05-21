using System;
using Refit;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.DTOs.Request;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.DTOs.Response;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid;

public interface IAirDroidAuthClient
{
    [Post("/signin")]
    Task<AirDroidSignInResponse> SignInAsync([Body] AirDroidSignInRequest req);
}
