using Postech8SOAT.FastOrder.Infra.IOC;
using Postech8SOAT.FastOrder.WebAPI.Endpoints;
using Postech8SOAT.FastOrder.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registrando as dependências
builder.Services.AddAutoMapper(typeof(Program));
IConfiguration configuration = builder.Configuration;
builder.Services.ConfigureDI(configuration);

builder.Services.AddCors();
var app = builder.Build();

//Executar as migrações pendentes
MigracoesPendentes.ExecuteMigration(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => { options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });

app.UseSwagger();

//Adicionar os endpoints
app.AddEnPointProdutos();
app.AddEndpointClientes();

app.UseHttpsRedirection();
app.Run();

public partial class Program { }


