using Monolithic.Middlewares;
using Monolithic.Helpers;

namespace Monolithic.Extensions;

public static class PipelineExtension
{
    public static void ConfigurePipeline(this WebApplication app)
    {
        var logger = app.Services.GetRequiredService<ILoggerManager>();

        app.UseCors(options => options
                                .AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader());

        // if (app.Environment.IsDevelopment())
        // {
        //     app.UseSwagger();
        //     app.UseSwaggerUI();
        // }

        app.UseSwagger();
        app.UseSwaggerUI();

        app.ConfigureSuccessHandler(logger);
        app.ConfigureErrorHandler(logger);

        app.UseCustomAuthResponse();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseMiddleware<JwtMiddleware>();

        app.MapControllers();

        app.Run();
    }
}