using FluentValidation;
using TabletLoan.VSA.Infrastructure.Filters;

namespace TabletLoan.VSA.Infrastructure.Extensions;

public static class ValidationExtensions
{
    public static IServiceCollection RegisterValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<Program>();
        return services;
    }

    public static RouteHandlerBuilder WithRequestValidation<TRequest>(this RouteHandlerBuilder builder)
    {
        builder.AddEndpointFilter<ValidationFilter<TRequest>>()
            .ProducesValidationProblem();

        return builder;
    }
}
