using PlcBase.Shared.Helpers;
using PlcBase.Middlewares;

namespace PlcBase.Extensions.Pipelines;

public static class PipelineExtension
{
    public static void ConfigurePipeline(this WebApplication app)
    {
        ILoggerManager logger = app.Services.GetRequiredService<ILoggerManager>();

        app.UseHealthCheck();

        app.UseMigration();

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
