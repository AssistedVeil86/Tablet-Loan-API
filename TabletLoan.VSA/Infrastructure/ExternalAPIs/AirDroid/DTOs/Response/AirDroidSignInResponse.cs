namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.DTOs.Response;

public record class AirDroidSignInResponse(
    int Code,
    string Msg,
    AirDroidSignInData Data
);
