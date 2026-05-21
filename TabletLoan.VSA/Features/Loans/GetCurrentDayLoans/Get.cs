using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TabletLoan.VSA.Domain.Shared;
using TabletLoan.VSA.Features.Loans.Shared;
using TabletLoan.VSA.Infrastructure.Data;
using TabletLoan.VSA.Infrastructure.Extensions;
using TabletLoan.VSA.Infrastructure.Services;

namespace TabletLoan.VSA.Features.Loans.GetCurrentDayLoans;

public static class GetCurrentDayLoansEndpoint
{
    public static RouteGroupBuilder MapGetCurrentDayLoansEndpoint(this RouteGroupBuilder route)
    {
        route.MapGet("/", Handler);

        return route;
    }

    private static async Task<Ok<PagedResponse<LoanResponse>>> Handler(
        [AsParameters] GetCurrentDayLoansRequest req, GetCurrentDayLoansHandler handler, CancellationToken ct)
    {
        var result = await handler.HandleAsync(req, ct);
        return TypedResults.Ok(result);
    }
}

internal sealed class GetCurrentDayLoansHandler(AppDbContext context, DateTimeService timeService)
{
    public async Task<PagedResponse<LoanResponse>> HandleAsync(GetCurrentDayLoansRequest req,
        CancellationToken ct)
    {
        var (startCurrentDayUtc, endCurrentDayUtc) = timeService.GetCurrentDayBoundaries();

        var baseQuery = context.Loans
            .AsNoTracking()
            .Where(l => l.CreatedAt >= startCurrentDayUtc && l.CreatedAt <= endCurrentDayUtc)
            .OrderByDescending(l => l.CreatedAt);

        var totalCount = await baseQuery.CountAsync(ct);

        var loans = await baseQuery
            .Skip((req.Page - 1) * req.Size)
            .Take(req.Size)
            .Select(l => l.ToResponse())
            .ToListAsync(ct);

        return PagedResponse<LoanResponse>.Create(loans, totalCount, req.Page, req.Size);
    }
}
