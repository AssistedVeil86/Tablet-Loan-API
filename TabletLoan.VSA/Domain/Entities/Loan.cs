using TabletLoan.VSA.Domain.Enums;

namespace TabletLoan.VSA.Domain.Entities;

public class Loan : BaseEntity
{
    public Loan()
    {
        LoanStartedAt = DateTimeOffset.UtcNow;
    }

    private Loan(string cif, string studentName, string studentLastname,
        Tablet tablet, DateTimeOffset loanStartDate)
    {
        StudentCif = cif;
        StudentName = studentName;
        StudentLastname = studentLastname;
        Tablet = tablet;
        TabletId = tablet.Id;
        LoanStartedAt = loanStartDate;
        LoanEndsAt = loanStartDate.AddHours(1);
        status = LoanStatus.ONGOING;
    }

    public string StudentCif { get; private set; } = string.Empty;
    public string StudentName { get; private set; } = string.Empty;
    public string StudentLastname { get; private set; } = string.Empty;
    public DateTimeOffset LoanStartedAt { get; private set; }
    public DateTimeOffset LoanEndsAt { get; private set; }
    public LoanStatus status;
    public int TabletId { get; private set; }
    public Tablet Tablet { get; set; } = null!;
    public static Loan Create(string cif, string studentName, string studentLastname, Tablet tablet)
    {
        var loanStartDate = DateTimeOffset.UtcNow;
        return new Loan(cif, studentName, studentLastname,tablet, loanStartDate);
    }
}
