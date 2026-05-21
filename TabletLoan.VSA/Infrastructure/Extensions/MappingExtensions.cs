using TabletLoan.VSA.Domain.Entities;
using TabletLoan.VSA.Features.Loans.Shared;

namespace TabletLoan.VSA.Infrastructure.Extensions;

public static class MappingExtensions
{
    public static LoanResponse ToResponse(this Loan loan)
    {
        return new LoanResponse(
            loan.Id,
            loan.StudentCif,
            loan.StudentName,
            loan.StudentLastname,
            loan.status,
            loan.LoanStartedAt,
            loan.LoanEndsAt);
    }
}
