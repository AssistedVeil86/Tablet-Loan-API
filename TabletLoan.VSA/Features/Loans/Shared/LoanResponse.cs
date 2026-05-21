using TabletLoan.VSA.Domain.Enums;

namespace TabletLoan.VSA.Features.Loans.Shared;

public sealed record LoanResponse(
    int Id,
    string StudentCif,
    string StudentName,
    string StudentLastname,
    LoanStatus Status,
    DateTimeOffset LoanStartedAt,
    DateTimeOffset LoanEndsAt);
