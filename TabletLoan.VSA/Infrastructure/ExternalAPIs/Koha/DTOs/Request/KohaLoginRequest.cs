using System.Text.Json.Serialization;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.DTOs.Request;

public sealed record KohaLoginRequest(
    [property: JsonPropertyName("userid")] string UserId,
    [property: JsonPropertyName("password")] string Password,
    [property: JsonPropertyName("branch")] string Branch
);
