using System.Text.Json.Serialization;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.DTOs.Response;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.DTOs;

public record class AirDroidSignInData(
    [property: JsonPropertyName("parental_tokens")] ParentalTokensResponse ParentalTokens
);
