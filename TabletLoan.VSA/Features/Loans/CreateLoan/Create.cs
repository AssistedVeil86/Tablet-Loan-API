using ErrorOr;
using Hangfire;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TabletLoan.VSA.Domain.Entities;
using TabletLoan.VSA.Domain.Errors;
using TabletLoan.VSA.Features.Loans.BackgroundJobs;
using TabletLoan.VSA.Features.Loans.Shared;
using TabletLoan.VSA.Infrastructure.Data;
using TabletLoan.VSA.Infrastructure.Extensions;
using TabletLoan.VSA.Infrastructure.Hubs;
using TabletLoan.VSA.Infrastructure.Services;

namespace TabletLoan.VSA.Features.Loans.CreateLoan;

public static class CreateLoanEndpoint
{
    public static RouteGroupBuilder MapCreateLoanEndpoint(this RouteGroupBuilder route)
    {
        route.MapPost("/", Handler)
            .WithName("CreateLoan")
            .WithSummary("Create a Loan for a Student")
            .WithDescription("Creates a Loan for the student and unlocks the physical devices")
            .WithRequestValidation<LoanRequest>()
            .ProducesProblem(StatusCodes.Status404NotFound);

        return route;
    }

    private static async Task<Results<Created<LoanResponse>, ProblemHttpResult>> Handler(
        LoanRequest req, CreateLoanHandler handler, CancellationToken ct)
    {
        var result = await handler.HandleAsync(req, ct);

        return result.Match<Results<Created<LoanResponse>, ProblemHttpResult>>(
            loan => TypedResults.Created($"api/loan/{loan.Id}", loan),
            errors => errors.ToProblemResult()
        );
    }
}

internal sealed class CreateLoanHandler(
    AppDbContext context,
    IHubContext<LoanNotificationHub> hubContext,
    KohaStudentService kohaStudentService,
    AirDroidTabletService airDroidTabletService,
    IBackgroundJobClient backgroundJobs)
{
    public async Task<ErrorOr<LoanResponse>> HandleAsync(LoanRequest req, CancellationToken ct)
    {
        var studentResult = await kohaStudentService.ValidateAndGetStudentAsync(req.Cif);
        if (studentResult.IsError)
            return studentResult.Errors;

        var student = studentResult.Value;

        var availableTablet = await context.Tablets
            .FirstOrDefaultAsync(t => t.IsAvailable, ct);

        if (availableTablet is null)
            return TabletErrors.TabletNotFound("No Available Tablet Found");

        var unlockResult = await airDroidTabletService.UnlockTabletAsync(availableTablet);
        if (unlockResult.IsError)
            return unlockResult.Errors;

        availableTablet.UpdateAvailability();

        var loan = Loan.Create(student.CardNumber, student.FirstName,
            student.Surname, availableTablet);

        backgroundJobs.Schedule<ITabletBlockJob>(
            job => job.ExcuteTabletBlockAsync(availableTablet.Id),
            TimeSpan.FromHours(1));

        context.Loans.Add(loan);
        await context.SaveChangesAsync(ct);

        var response = loan.ToResponse();

        await hubContext.Clients.All.SendAsync("LoanNotification", response.Id,
            availableTablet.Id, availableTablet.ServoPin, availableTablet.SwitchPin, ct);

        return response;
    }
}