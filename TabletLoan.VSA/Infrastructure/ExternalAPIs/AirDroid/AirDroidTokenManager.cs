namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid;

public class AirDroidTokenManager
{
    private string CurrentToken { get; set; } = string.Empty;
    private readonly Lock Lock = new();

    public string GetToken()
    {
        lock (Lock)
        {
            return CurrentToken;
        }
    }

    public void SetToken(string newToken)
    {
        lock (Lock)
        {
            CurrentToken = newToken;
        }
    }
}
