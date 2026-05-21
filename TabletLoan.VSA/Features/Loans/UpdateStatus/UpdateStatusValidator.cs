using FluentValidation;
using TabletLoan.VSA.Domain.Enums;

namespace TabletLoan.VSA.Features.Loans.UpdateStatus;

public class UpdateStatusValidator : AbstractValidator<UpdateStatusRequest>
{
    public UpdateStatusValidator()
    {
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Loan Status is Required")
            .Must(status => status == LoanStatus.DONE);
    }
}
