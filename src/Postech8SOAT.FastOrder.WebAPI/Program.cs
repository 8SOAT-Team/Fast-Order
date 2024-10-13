using Microsoft.AspNetCore.Http.Json;
using Polferov.SwaggerEnumsAsStrings;
using Postech8SOAT.FastOrder.Infra.IOC;
using Postech8SOAT.FastOrder.WebAPI.Endpoints;
using Postech8SOAT.FastOrder.WebAPI.Logs;
using Postech8SOAT.FastOrder.WebAPI.Middlewares;
using Postech8SOAT.FastOrder.WebAPI.Services;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddEnumsAsStringsFilter();
});

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

//Registrando as dependências
IConfiguration configuration = builder.Configuration.AddEnvironmentVariables().Build();

builder.Services.ConfigureDI(configuration);

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddCors();

builder.Services.AddSingleton<CleanArch.UseCase.Logging.ILogger, ConsoleLogger>();

var app = builder.Build();

app.ConfigureExceptionHandler();

//Executar as migrações pendentes
var shouldRunMigrations = bool.Parse(configuration["RunMigrationsOnStart"] ?? "false");

if (shouldRunMigrations)
{
    MigracoesPendentes.ExecuteMigration(app);
}

// Configure the HTTP request pipeline.

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Fast Order v1");
    options.RoutePrefix = string.Empty;
});

app.UseCors(options => { options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });

//Adicionar os endpoints
app.AddEndPointProdutos();
app.AddEndpointClientes();
app.AddEndpointPedidos();
app.AddEndpointPagamentos();
app.AddEndpointMigrate();
app.AddEndpointWebhook();

app.UseHttpsRedirection();

app.Run();

public partial class Program { }


