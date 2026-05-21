using FluentValidation;

namespace TabletLoan.VSA.Features.Loans.GetCurrentDayLoans;

public class GetLoansValidator : AbstractValidator<GetCurrentDayLoansRequest>
{
    public GetLoansValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0)
            .WithMessage("La página debe ser mayor a 0.");

        RuleFor(x => x.Size)
            .InclusiveBetween(1, 50)
            .WithMessage("El tamaño de página debe estar entre 1 y 50.");
    }
}
