using System.Text.Json.Serialization;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.Turnstile.DTOs;

public class TurnstileResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    [JsonPropertyName("error-codes")]
    public List<String> ErrorCodes { get; set; } = null!;
}
