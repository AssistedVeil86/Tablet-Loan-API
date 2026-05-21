using System.Text.Json.Serialization;

namespace TabletLoan.VSA.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum LoanStatus
{
    ONGOING,
    DONE
}
