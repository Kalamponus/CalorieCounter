using CalorieCounter.Api.Endpoints;
using Serilog;
using CalorieCounter.Application;
using CalorieCounter.Persistence;
using FluentValidation;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddProblemDetails();

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration.GetConnectionString("MainDB") ?? throw new Exception("Connection string 'MainDB' not found."));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

app.UseExceptionHandler();

app.MapUsersEndpoints();

app.Run();