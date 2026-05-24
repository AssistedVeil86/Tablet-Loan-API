using Microsoft.AspNetCore.SignalR;
using TabletLoan.VSA.Domain.Enums;

namespace TabletLoan.VSA.Infrastructure.Hubs;

public class LoanStatusHub : Hub
{
    public async Task SendLoanStatus(LoanStatus status)
    {
        await Clients.All.SendAsync("LoanStatusNotification", status);
    }
}
