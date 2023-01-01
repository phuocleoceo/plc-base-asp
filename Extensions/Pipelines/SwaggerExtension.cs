namespace PlcBase.Extensions.Pipelines;

public static class SwaggerExtension
{
    public static void UseSwaggerPipeline(this WebApplication app)
    {
        // if (app.Environment.IsDevelopment())
        // {
        //     app.UseSwagger();
        //     app.UseSwaggerUI();
        // }

        app.UseSwagger();
        app.UseSwaggerUI();
    }
}