using Microsoft.Extensions.Options;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Turnstile;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Turnstile.DTOs;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Turnstile.Errors;
using TabletLoan.VSA.Infrastructure.ExternalAPIs.Turnstile.Options;

namespace TabletLoan.VSA.Infrastructure.Filters;

public class TurnstileValidationFilter(
    IOptions<TurnstileOptions> options,
    ITurnstileClient turnstileClient,
    ILogger<TurnstileValidationFilter> logger) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        //Verify Headers
        if (!context.HttpContext.Request.Headers.TryGetValue("X-Turnstile-Token", out var tokenValues))
        {
            logger.LogWarning("Access Denied: Missing X-Turnstile-Token Header");
            return TurnstileErrors.MissingToken();
        }

        //Create Turnstile Request
        var request = new TurnstileRequest()
        {
            Secret = options.Value.SecretKey,
            Response = tokenValues.ToString()
        };

        //Use Refit to send the Request to SiteVerify
        try
        {
            var response = await turnstileClient.VerifyAsync(request);

            if (!response.Success)
            {
                var errors = response.ErrorCodes != null
                    ? string.Join(", ", response.ErrorCodes) : "Desconocido";

                logger.LogWarning("Turnstile Validation Errors: {Errors}", errors);
                return TurnstileErrors.ValidationFailed(errors);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Comunication Error With Turnstile API");
            return TurnstileErrors.CommunicationError();
        }

        return await next(context);
    }
}
