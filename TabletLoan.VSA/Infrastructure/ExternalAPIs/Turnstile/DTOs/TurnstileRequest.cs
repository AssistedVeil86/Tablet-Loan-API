using System.Text.Json.Serialization;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.Turnstile.DTOs;

public class TurnstileRequest
{
    [JsonPropertyName("secret")]
    public string Secret { get; set; } = string.Empty;
    [JsonPropertyName("response")]
    public string Response { get; set; } = string.Empty;
}
