using System.Text.Json.Serialization;
using Calculadora.Api.Middleware;
using Calculadora.Application;
using Calculadora.Infrastructure;
using Calculadora.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

const string PoliticaCors = "Front";

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(opcoes => opcoes.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<DominioExceptionHandler>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var origensPermitidas = builder.Configuration.GetSection("Cors:Origens").Get<string[]>() ?? [];
builder.Services.AddCors(opcoes => opcoes.AddPolicy(PoliticaCors, politica => politica
    .WithOrigins(origensPermitidas)
    .AllowAnyHeader()
    .AllowAnyMethod()));

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    using var escopo = app.Services.CreateScope();
    await escopo.ServiceProvider.GetRequiredService<CalculadoraDbContext>().Database.MigrateAsync();
}

app.UseHttpsRedirection();
app.UseCors(PoliticaCors);
app.MapControllers();

app.Run();

public partial class Program;
