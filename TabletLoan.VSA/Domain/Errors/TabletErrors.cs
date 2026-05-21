using System;
using ErrorOr;

namespace TabletLoan.VSA.Domain.Errors;

public static class TabletErrors
{
    public static Error TabletNotFound(string message) 
        => Error.NotFound("Tablet.NotFound", message);
}
