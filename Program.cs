using PlcBase.Extensions.ServiceCollections;
using PlcBase.Extensions.Pipelines;
using PlcBase.Extensions.Builders;

var builder = WebApplication.CreateBuilder(args);

// Service collection
builder.Services.ConfigureService(builder.Configuration);

// Builder
builder.ConfigureBuilder(builder.Configuration);

var app = builder.Build();

// HTTP request pipeline
app.ConfigurePipeline();

// Run web app
app.Run();