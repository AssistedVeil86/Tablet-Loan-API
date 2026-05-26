using System.Text.RegularExpressions;
using FluentValidation;

namespace TabletLoan.VSA.Features.Loans.CreateLoan;

public class LoanRequestValidator : AbstractValidator<LoanRequest>
{
    private static readonly Regex CifRegex =
        new(@"^\d{4}(01|02)\d{4}$", RegexOptions.Compiled);

    private static readonly Regex DuiRegex =
        new(@"^\d{9}$", RegexOptions.Compiled);

    public LoanRequestValidator()
    {
        RuleFor(x => x.Cif)
            .NotEmpty()
                .WithMessage("El CIF es obligatorio.")
            .Must(BeNumeric)
                .WithMessage("El CIF debe contener únicamente dígitos numéricos.")
            .Must(HaveValidFormat)
                .WithMessage("El CIF no tiene un formato válido. " +
                            "Debe ser un CIF (ej: 2024011332) o un DUI sin guión (ej: 012345678).");
    }
    private static bool BeNumeric(string cif) =>
        !string.IsNullOrEmpty(cif) && cif.All(char.IsDigit);

    private static bool HaveValidFormat(string cif) =>
        CifRegex.IsMatch(cif) || DuiRegex.IsMatch(cif);
};
