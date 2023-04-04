using Microsoft.AspNetCore.Mvc.Filters;

using PlcBase.Shared.Constants;
using PlcBase.Base.Error;

namespace PlcBase.Base.Filter;

public class ValidateModelFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            Dictionary<string, string[]> errors = context.ModelState
                            .Where(x => x.Value.Errors.Count > 0)
                            .ToDictionary(
                                k => k.Key,
                                k => k.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                            );

            throw new BaseException(
                HttpCode.UNPROCESSABLE_ENTITY,
                ErrorMessage.VALIDATION_ERROR,
                errors
            );
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}