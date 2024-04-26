namespace PlcBase.Shared.Utilities;

public class TimeUtility
{
    public static readonly DateTime MIN_LOCAL_DATE_TIME = new(0, 1, 1, 0, 0, 0);
    public static readonly DateTime MAX_LOCAL_DATE_TIME = new(9999, 12, 31, 23, 59, 59);
    public const long MAX_EPOCH_MILLISECOND = 10413792000000L;

    public static string GetDateTimeFormatted(DateTime dateTime, string pattern)
    {
        return dateTime.ToString(pattern);
    }

    public static DateTime Now()
    {
        return DateTime.UtcNow;
    }

    public static DateTime ConvertToDateTime(long epochTime)
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(epochTime).UtcDateTime;
    }

    public static long ConvertToEpochMilli(DateTime dateTime)
    {
        return ((DateTimeOffset)dateTime).ToUnixTimeMilliseconds();
    }

    public static long GetEpochOfNow()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }

    public static bool IsValidTimezone(string timezone)
    {
        return !string.IsNullOrEmpty(timezone)
            && TimeZoneInfo.GetSystemTimeZones().Any(tz => tz.Id.Equals(timezone));
    }

    public static DateTime NowAtTimezone(string timezone)
    {
        return TimeZoneInfo.ConvertTime(
            DateTime.UtcNow,
            TimeZoneInfo.FindSystemTimeZoneById(timezone)
        );
    }

    public static DateTime DateAtTimezone(DateTime dateTime, string timezone)
    {
        return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(timezone));
    }

    public static long DaysDifference(DateTime startTime, DateTime endTime)
    {
        return (long)(endTime.Date - startTime.Date).TotalDays;
    }

    public static long DaysDifferenceAtTimezone(
        DateTime startTime,
        DateTime endTime,
        string timezone
    )
    {
        return DaysDifference(
            DateAtTimezone(startTime, timezone),
            DateAtTimezone(endTime, timezone)
        );
    }

    public static long MillisDifference(DateTime startTime, DateTime endTime)
    {
        return (long)(endTime - startTime).TotalMilliseconds;
    }
}
