using Polferov.SwaggerEnumsAsStrings;
using Postech8SOAT.FastOrder.Infra.IOC;
using Postech8SOAT.FastOrder.WebAPI.Endpoints;
using Postech8SOAT.FastOrder.WebAPI.Logs;
using Postech8SOAT.FastOrder.WebAPI.Middlewares;
using Postech8SOAT.FastOrder.WebAPI.Services;
using Posttech8SOAT.FastOrder.Infra.Env;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;

DotNetEnv.Env.TraversePath().Load();

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
builder.Services.ConfigureDI();

var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
{
    MaxDepth = 16,
    ReferenceHandler = ReferenceHandler.IgnoreCycles,
};

jsonOptions.Converters.Add(new JsonStringEnumConverter());

builder.Services.AddSingleton(jsonOptions);

builder.Services.ConfigureHttpJsonOptions(options => {
    options.SerializerOptions.ReferenceHandler = jsonOptions.ReferenceHandler;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.MaxDepth = jsonOptions.MaxDepth;
});


builder.Services.AddCors();

builder.Services.AddSingleton<CleanArch.UseCase.Logging.ILogger, ConsoleLogger>();

var app = builder.Build();

app.ConfigureExceptionHandler();

//Executar as migrações pendentes
if (EnvConfig.RunMigrationsOnStart)
{
    await MigracoesPendentes.ExecuteMigrationAsync(app);
}

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


