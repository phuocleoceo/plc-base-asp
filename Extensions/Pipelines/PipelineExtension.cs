using PlcBase.Middlewares;
using PlcBase.Helpers;

namespace PlcBase.Extensions.Pipelines;

public static class PipelineExtension
{
    public static void ConfigurePipeline(this WebApplication app)
    {
        ILoggerManager logger = app.Services.GetRequiredService<ILoggerManager>();

        app.UseCorsPipeline();

        app.UseSwaggerPipeline();

        app.UseResponseHandlerPipeline(logger);

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseMiddleware<JwtMiddleware>();

        app.MapControllers();
    }
}