using TabletLoan.VSA.Features.Loans.BackgroundJobs;
using TabletLoan.VSA.Features.Loans.BackgroundJobs.Implementation;
using TabletLoan.VSA.Features.Loans.CreateLoan;
using TabletLoan.VSA.Features.Loans.UpdateStatus;
using TabletLoan.VSA.Infrastructure.Filters;

namespace TabletLoan.VSA.Features.Loans;

public static class LoanEndpoints
{
    public static IServiceCollection RegisterLoanJobs(this IServiceCollection services)
    {
        services.AddScoped<ITabletBlockJob, TabletBlockJob>();
        return services;
    }
    public static IServiceCollection RegisterLoanHandlers(this IServiceCollection services)
    {
        services.AddScoped<CreateLoanHandler>();
        services.AddScoped<UpdateStatusHandler>();

        return services;
    }

    public static WebApplication MapLoanEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/loans")
            .WithTags("Loans");

        group.MapCreateLoanEndpoint();
        group.MapUpdateStatusEndpoint();

        return app;
    }
}