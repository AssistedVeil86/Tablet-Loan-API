namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha;

public class KohaSessionManager
{
    private string CurrentCookie = string.Empty;
    private readonly Lock Lock = new();

    public string GetCookie()
    {
        lock (Lock)
        {
            return CurrentCookie;
        }
    }

    public void SetCookie(string newCookie)
    {
        lock (Lock)
        {
            CurrentCookie = newCookie;
        }
    }

}
