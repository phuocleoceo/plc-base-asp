namespace PlcBase.Extensions.Builders;

public static class BuilderExtension
{
    public static void ConfigureBuilder(
        this WebApplicationBuilder builder,
        IConfiguration configuration
    )
    {
        builder.Logging.ConfigureSerilog(configuration);
    }
}
