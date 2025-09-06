using InfoTrack.Api.Core;
using InfoTrack.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/********** RegisterServices - an extension method to register all the dependencies ******/
builder.Services.RegisterServices();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

app.UseMiddleware<ConcurrencyRequestLimitter>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
