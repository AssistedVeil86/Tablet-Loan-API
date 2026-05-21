namespace TabletLoan.VSA.Features.Loans.GetCurrentDayLoans;

public sealed record GetCurrentDayLoansRequest(
    int Page = 1,
    int Size = 10);
