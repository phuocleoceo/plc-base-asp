using Quartz;

using PlcBase.Extensions.ServiceCollections;
using PlcBase.Extensions.Pipelines;
using PlcBase.Extensions.Builders;
using PlcBase;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Service collection
builder.Services.ConfigureService(builder.Configuration);

builder.Services.AddQuartz(q =>
{
    // Register the job, loading the schedule from configuration
    q.AddJob<ExampleJob>(opts => opts.WithIdentity("ExampleJob"));
    q.AddTrigger(
        opts =>
            opts.ForJob("ExampleJob")
                .WithIdentity("ExampleJob-trigger")
                .WithCronSchedule("0/5 * * * * ?")
    ); // This cron schedule runs every 5 seconds
});

// Builder
builder.ConfigureBuilder(builder.Configuration);

WebApplication app = builder.Build();

// HTTP request pipeline
app.ConfigurePipeline();

// Run web app
app.Run();
