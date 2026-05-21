using System;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.AirDroid.BackgroundJobs;

public interface ISignOutAirDroidJob
{
    Task ExecuteSignOutAsync();
}
