namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.DTOs.Request;

public sealed record InstantBlockRequest(
    string Ptoken,
    long Account_id,
    string Device_id,
    long Timestamp,
    int Device_type,
    int App_version,
    string Language,
    long Nonce,
    string C_device_id,
    int Enabled,     // 0 = Desbloquear, 1 = Bloquear
    int Limit_time   // 0 = Sin límite, -2 = Infinito (según tu imagen)
);
