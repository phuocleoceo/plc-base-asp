using PlcBase.Shared.Constants;

namespace PlcBase.Base.Error;

public class BaseException : Exception
{
    public int StatusCode { get; set; } = HttpCode.INTERNAL_SERVER_ERROR;

    public Dictionary<string, string[]> Errors { get; set; } = null;

    public BaseException() { }

    public BaseException(int StatusCode = HttpCode.INTERNAL_SERVER_ERROR,
                         string Message = "",
                         Dictionary<string, string[]> Errors = null) : base(Message)
    {
        this.StatusCode = StatusCode;
        this.Errors = Errors;
    }
}