namespace TabletLoan.VSA.Infrastructure.Services;

public class DateTimeService
{
    private readonly TimeZoneInfo LocalZone =
        TimeZoneInfo.FindSystemTimeZoneById("America/El_Salvador");

    public (DateTimeOffset startOfDay, DateTimeOffset endOfDay) GetCurrentDayBoundaries()
    {
        var utcNow = DateTimeOffset.UtcNow;
        var localNow = TimeZoneInfo.ConvertTime(utcNow, LocalZone);

        // Calculate Start and End of Today
        var startOfDayLocal = localNow.Date;
        var endOfDayLocal = startOfDayLocal.AddDays(1);

        // Get Offset from Start and End
        var startOfDayOffset = LocalZone.GetUtcOffset(startOfDayLocal);
        var endOfDayOffset = LocalZone.GetUtcOffset(endOfDayLocal);

        // Get Utc Start and End of Day from Offset
        var startOfDayUtc = new DateTimeOffset(startOfDayLocal, startOfDayOffset)
            .ToUniversalTime();
        var endOfDayUtc = new DateTimeOffset(endOfDayLocal, endOfDayOffset)
            .ToUniversalTime();

        return (startOfDayUtc, endOfDayUtc);
    }
}
