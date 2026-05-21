using System.Text.Json.Serialization;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.DTOs.Request;

public sealed record KohaSearchPatronRequest(
    [property: JsonPropertyName("searchQuery")] string SearchQuery,
    [property: JsonPropertyName("cookie")] string Cookie
);
