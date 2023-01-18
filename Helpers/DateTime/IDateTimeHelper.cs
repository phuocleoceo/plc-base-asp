namespace PlcBase.Helpers;

public interface IDateTimeHelper
{
    DateTime Now();

    DateTime ConvertLocalTime(DateTime dateTime);
}