using PlcBase.Shared.Constants;

namespace PlcBase.Base.Error;

public class BaseException : Exception
{
    public int StatusCode { get; set; }

    public Dictionary<string, string[]> Errors { get; set; }

    public BaseException(
        int statusCode = HttpCode.INTERNAL_SERVER_ERROR,
        string message = "",
        Dictionary<string, string[]> errors = null
    )
        : base(message)
    {
        StatusCode = statusCode;
        Errors = errors;
    }
}
