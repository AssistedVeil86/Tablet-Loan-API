namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.DTOs.Response;

public sealed record AirDroidBlockResponse(
    int Code,
    string Msg,
    AirDroidBlockData Data
);
