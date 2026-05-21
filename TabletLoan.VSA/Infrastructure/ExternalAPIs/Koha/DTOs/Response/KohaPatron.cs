using System.Text.Json.Serialization;

namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.Koha.DTOs.Response;

public sealed record KohaPatron(
    [property: JsonPropertyName("cardnumber")] string CardNumber,
    [property: JsonPropertyName("firstname")] string FirstName,
    [property: JsonPropertyName("surname")] string Surname);
