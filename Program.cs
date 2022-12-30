using Monolithic.Middlewares;
using Monolithic.Extensions;
using Monolithic.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Service collection
builder.Services.ConfigureService(builder.Configuration);

// Logging
builder.Logging.ConfigureSerilog(builder.Configuration);

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerManager>();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
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