using System;
using Microsoft.AspNetCore.SignalR;

namespace TabletLoan.VSA.Infrastructure.Hubs;

public class LoanNotificationHub : Hub
{
    public async Task SendLoanNotification(int loanId, int tabletId, string servoPin, string switchPin)
    {
        await Clients.All.SendAsync("LoanNotification", loanId, tabletId, servoPin, switchPin);
    }
}