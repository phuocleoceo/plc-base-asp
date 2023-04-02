namespace PlcBase.Shared.Helpers;

public interface IDateTimeHelper
{
    DateTime Now();

    DateTime ConvertLocalTime(DateTime dateTime);
}