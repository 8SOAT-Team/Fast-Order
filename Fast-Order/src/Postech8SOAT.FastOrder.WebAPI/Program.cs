using Postech8SOAT.FastOrder.Infra.IOC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registrando as dependências
builder.Services.AddAutoMapper(typeof(Program));
IConfiguration configuration = builder.Configuration;
builder.Services.ConfigureDI(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => { options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });

app.UseSwagger();

//Adicionar os endpoints

app.UseHttpsRedirection();
app.Run();

public partial class Program { }


