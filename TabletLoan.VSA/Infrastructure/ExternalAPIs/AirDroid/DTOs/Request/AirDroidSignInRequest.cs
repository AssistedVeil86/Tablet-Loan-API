using System.Text.Json.Serialization;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.DTOs.Request;

public sealed record AirDroidSignInRequest(
    [property: JsonPropertyName("timestamp")] long Timestamp,
    [property: JsonPropertyName("device type")] int DeviceType,
    [property: JsonPropertyName("app_version")] int AppVersion,
    [property: JsonPropertyName("language")] string Language,
    [property: JsonPropertyName("nonce")] long Nonce,
    [property: JsonPropertyName("mode_type")] int ModeType,
    [property: JsonPropertyName("captcha_type")] int CaptchaType,
    [property: JsonPropertyName("fp")] string Fp,
    [property: JsonPropertyName("mail")] string Mail,
    [property: JsonPropertyName("pwd")] string Pwd,
    [property: JsonPropertyName("data_region")] string DataRegion
);
