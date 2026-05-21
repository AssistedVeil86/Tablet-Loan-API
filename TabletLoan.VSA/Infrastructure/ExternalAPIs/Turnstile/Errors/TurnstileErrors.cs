namespace TabletLoan.VSA.Infrastructure.ExternalAPIs.Turnstile.Errors;

public static class TurnstileErrors
{
    public static IResult MissingToken() => 
        TypedResults.Problem(
            statusCode: StatusCodes.Status401Unauthorized,
            title: "Acceso Denegado",
            detail: "El header X-Turnstile-Token es obligatorio pero no fue proporcionado.",
            type: "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1"
        );

    public static IResult ValidationFailed(string errors) => 
        TypedResults.Problem(
            statusCode: StatusCodes.Status403Forbidden,
            title: "Validación de Seguridad Fallida",
            detail: $"El token proporcionado fue rechazado por el servidor de validación. Detalles: {errors}",
            type: "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4"
        );

    public static IResult CommunicationError() => 
        TypedResults.Problem(
            statusCode: StatusCodes.Status500InternalServerError,
            title: "Error de Infraestructura",
            detail: "Ocurrió un problema al comunicarse con el proveedor de seguridad.",
            type: "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
        );
}
