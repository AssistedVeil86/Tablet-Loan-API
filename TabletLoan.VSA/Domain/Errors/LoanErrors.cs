using System;
using ErrorOr;

namespace TabletLoan.VSA.Domain.Errors;

public static class LoanErrors
{
    public static Error LoanNotFound(string message) 
        => Error.NotFound("Loan.NotFound", message);
}
