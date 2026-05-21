using System.Text.Json.Serialization;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.DTOs.Response;

public sealed record KohaLoginResponse(
    [property: JsonPropertyName("success")] bool Success,
    [property: JsonPropertyName("cookie")] string Cookie,
    [property: JsonPropertyName("userid")] string UserId,
    [property: JsonPropertyName("error")] string? Error
);
