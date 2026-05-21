using System.Text.Json.Serialization;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.DTOs.Request;

public sealed record AirDroidSignOutRequest(
    [property: JsonPropertyName("ptoken")] string Ptoken,
    [property: JsonPropertyName("account_id")] long AccountId,
    [property: JsonPropertyName("device_id")] string DeviceId,
    [property: JsonPropertyName("timestamp")] long Timestamp,
    [property: JsonPropertyName("device_type")] int DeviceType,
    [property: JsonPropertyName("app_version")] int AppVersion,
    [property: JsonPropertyName("language")] string Language,
    [property: JsonPropertyName("nonce")] long Nonce
);
