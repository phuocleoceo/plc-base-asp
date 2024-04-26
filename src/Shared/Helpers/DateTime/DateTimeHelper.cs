using Microsoft.Extensions.Options;
using PlcBase.Shared.Utilities;

namespace PlcBase.Shared.Helpers;

public class DateTimeHelper : IDateTimeHelper
{
    private readonly DateTimeSettings _dateTimeSettings;

    public DateTimeHelper(IOptions<DateTimeSettings> dateTimeSettings)
    {
        _dateTimeSettings = dateTimeSettings.Value;
    }

    public DateTime Now()
    {
        return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(
            TimeUtility.Now(),
            _dateTimeSettings.TimeZone
        );
    }

    public DateTime ConvertLocalTime(DateTime dateTime)
    {
        return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, _dateTimeSettings.TimeZone);
    }
}
