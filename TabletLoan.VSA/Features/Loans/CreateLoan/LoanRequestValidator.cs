using System.Text.RegularExpressions;
using FluentValidation;

namespace TabletLoan.VSA.Features.Loans.CreateLoan;

public class LoanRequestValidator : AbstractValidator<LoanRequest>
{
    private static readonly Regex CifRegex =
        new(@"^\d{4}(01|02)\d{4}$", RegexOptions.Compiled);

    public LoanRequestValidator()
    {
        RuleFor(x => x.Cif)
            .NotEmpty()
                .WithMessage("El CIF es obligatorio.")
            .Length(10)
                .WithMessage("El CIF debe tener exactamente 10 dígitos.")
            .Must(BeNumeric)
                .WithMessage("El CIF debe contener únicamente dígitos numéricos.")
            .Matches(CifRegex)
                .WithMessage("El CIF no tiene un formato válido (ej: 2024011332 o 2024021332).");
    }
    private static bool BeNumeric(string cif) =>
            cif.All(char.IsDigit);
};
