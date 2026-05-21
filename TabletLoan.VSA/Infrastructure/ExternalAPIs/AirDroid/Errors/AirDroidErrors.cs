using System;
using ErrorOr;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.Errors;

public static class AirDroidErrors
{
    public static Error UnlockFailed(string message) =>
        Error.Failure("AirDroid.UnlockFailed", message);

    public static Error NetworkError(string message) =>
        Error.Unexpected("AirDroid.NetworkError", message);
}
