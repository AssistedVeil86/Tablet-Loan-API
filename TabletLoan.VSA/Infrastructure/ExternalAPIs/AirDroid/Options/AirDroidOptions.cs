namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.Options;

public class AirDroidOptions
{
    public const string SectionName = "AirDroid";
    public string Mail { get; set; } = string.Empty;
    public string Pwd { get; set; } = string.Empty;
    public int AppVersion { get; set; }
}
