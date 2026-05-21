using TabletLoan.VSA.Domain.Enums;

namespace TabletLoan.VSA.Features.Loans.UpdateStatus;

public record class UpdateStatusRequest(LoanStatus Status);
