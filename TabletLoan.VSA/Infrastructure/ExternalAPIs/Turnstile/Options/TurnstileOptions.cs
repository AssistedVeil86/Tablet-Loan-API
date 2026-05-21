namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.Turnstile.Options;

public class TurnstileOptions
{
    public const string SectionName = "Turnstile";
    public string SecretKey { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
}
