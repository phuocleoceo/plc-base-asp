using Monolithic.Middlewares;
using Monolithic.Helpers;

namespace Monolithic.Extensions.Pipelines;

public static class PipelineExtension
{
    public static void ConfigurePipeline(this WebApplication app)
    {
        var logger = app.Services.GetRequiredService<ILoggerManager>();

        app.UseCorsPipeline();

        app.UseResponseHandlerPipeline(logger);

        app.UseCustomAuthResponse();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseMiddleware<JwtMiddleware>();

        app.MapControllers();

        app.Run();
    }
}