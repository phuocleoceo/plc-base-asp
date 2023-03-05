using PlcBase.Common.Constants;

namespace PlcBase.Base.Error;

public class BaseException : Exception
{
    public int StatusCode { get; set; }

    public BaseException() { }

    public BaseException(int StatusCode = HttpCode.INTERNAL_SERVER_ERROR,
                         string Message = "") : base(Message)
    {
        this.StatusCode = StatusCode;
    }
}