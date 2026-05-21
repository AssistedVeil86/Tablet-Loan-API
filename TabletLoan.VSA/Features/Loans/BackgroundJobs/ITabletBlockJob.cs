using System;

namespace TabletLoan.VSA.Features.Loans.BackgroundJobs;

public interface ITabletBlockJob
{
    Task ExcuteTabletBlockAsync(int tabletId);
}
