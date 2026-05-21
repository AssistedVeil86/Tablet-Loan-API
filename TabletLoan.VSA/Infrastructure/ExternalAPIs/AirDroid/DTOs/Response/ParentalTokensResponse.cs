using System.Text.Json.Serialization;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.DTOs.Response;

public record class ParentalTokensResponse(
    [property: JsonPropertyName("ptoken")] string Ptoken
);
