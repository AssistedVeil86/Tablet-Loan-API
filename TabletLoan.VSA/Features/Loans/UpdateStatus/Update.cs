using ErrorOr;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TabletLoan.VSA.Domain.Enums;
using TabletLoan.VSA.Domain.Errors;
using TabletLoan.VSA.Features.Loans.Shared;
using TabletLoan.VSA.Infrastructure.Data;
using TabletLoan.VSA.Infrastructure.Extensions;
using TabletLoan.VSA.Infrastructure.Hubs;

namespace TabletLoan.VSA.Features.Loans.UpdateStatus;

public static class UpdateStatusEndpoint
{
    public static RouteGroupBuilder MapUpdateStatusEndpoint(this RouteGroupBuilder route)
    {
        route.MapPut("{loanId}", Handler)
            .WithName("UpdateLoanStatus")
            .WithSummary("Update the Loan Status to DONE")
            .WithDescription("Update the Loan Status after confirming the physical devices has been returned")
            .WithRequestValidation<UpdateStatusRequest>()
            .ProducesProblem(StatusCodes.Status404NotFound);

        return route;
    }

    private static async Task<Results<Ok<LoanResponse>, ProblemHttpResult>> Handler(
        UpdateStatusRequest req, int loanId, UpdateStatusHandler handler, CancellationToken ct)
    {
        var result = await handler.HandleAsync(req, loanId, ct);

        return result.Match<Results<Ok<LoanResponse>, ProblemHttpResult>>(
            loan => TypedResults.Ok(loan),
            errors => errors.ToProblemResult()
        );
    }
}

internal sealed class UpdateStatusHandler(
    AppDbContext context,
    IHubContext<LoanStatusHub> hubContext)
{
    public async Task<ErrorOr<LoanResponse>> HandleAsync(
        UpdateStatusRequest req, int id, CancellationToken ct)
    {
        var loan = await context.Loans.FirstOrDefaultAsync(l => l.Id == id, ct);

        if (loan is null)
            return LoanErrors.LoanNotFound($"Loan with Id {id} not found");

        if (loan.status == LoanStatus.DONE)
            return LoanErrors.TabletStatusError($"Loan with {id} is already DONE");

        loan.status = req.Status;
        loan.UpdateModified();

        await hubContext.Clients.All
            .SendAsync("LoanStatusNotification", loan.status, ct);

        await context.SaveChangesAsync(ct);

        return loan.ToResponse();
    }
}