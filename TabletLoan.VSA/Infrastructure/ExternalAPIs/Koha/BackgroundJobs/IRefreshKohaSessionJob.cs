namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.BackgroundJobs;

public interface IRefreshKohaSessionJob
{
    Task ExecuteLoginAsync();
}
