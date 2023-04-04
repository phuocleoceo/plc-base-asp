using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

using PlcBase.Shared.Constants;
using PlcBase.Base.Error;

namespace PlcBase.Base.Filter;

public class ValidateModelFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

            var errorResponse = new ErrorResponse
            {
                Errors = errors.SelectMany(x => x.Errors.Select(e => new ErrorModel
                {
                    FieldName = x.Key,
                    Message = e.ErrorMessage
                })).ToList()
            };

            context.Result = new UnprocessableEntityObjectResult(errorResponse);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}

public class ErrorModel
{
    public string FieldName { get; set; }
    public string Message { get; set; }
}

public class ErrorResponse
{
    public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
}
