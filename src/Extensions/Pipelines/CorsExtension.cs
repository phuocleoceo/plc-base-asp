namespace PlcBase.Extensions.Pipelines;

public static class CorsExtension
{
    public static void UseCorsPipeline(this WebApplication app)
    {
        app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    }
}
