namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.DTOs;

public sealed record AirDroidBlockData(
    int Enabled,
    int Limit_time,
    long Start_time
);
