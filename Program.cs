using Monolithic.Extensions.ServiceCollections;
using Monolithic.Extensions.Builders;
using Monolithic.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Service collection
builder.Services.ConfigureService(builder.Configuration);

// Builder
builder.ConfigureBuilder(builder.Configuration);

var app = builder.Build();

// HTTP request pipeline
app.ConfigurePipeline();