using System.Text.Json.Serialization;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.DTOs.Response;

public record KohaSearchPatronResponse(
    [property: JsonPropertyName("success")] bool Success,
    [property: JsonPropertyName("patrons")] List<KohaPatron> Patrons,
    [property: JsonPropertyName("error")] string? Error);
