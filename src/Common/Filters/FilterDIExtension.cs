using Microsoft.AspNetCore.Mvc;

using PlcBase.Base.Filter;

namespace PlcBase.Common.Filters;

public static class FilterDIExtension
{
    public static void ConfigureFilterDI(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddScoped<ValidateModelFilter>();
    }
}
