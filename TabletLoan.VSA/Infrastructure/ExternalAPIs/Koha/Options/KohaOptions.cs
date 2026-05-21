namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.Options;

public class KohaOptions
{
    public const string SectionName = "Koha";
    public string UserId { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
