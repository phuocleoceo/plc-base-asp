using Microsoft.Extensions.Options;

namespace PlcBase.Helpers;

public class DateTimeHelper : IDateTimeHelper
{
    private readonly DateTimeSettings _dateTimeSettings;

    public DateTimeHelper(IOptions<DateTimeSettings> dateTimeSettings)
    {
        _dateTimeSettings = dateTimeSettings.Value;
    }

    public DateTime Now()
    {
        return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, _dateTimeSettings.TimeZone);
    }

    public DateTime ConvertLocalTime(DateTime dateTime)
    {
        return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, _dateTimeSettings.TimeZone);
    }
}