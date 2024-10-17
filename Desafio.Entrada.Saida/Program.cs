using Desafio.Entrada.Saida.Dominio.Interfaces;
using Desafio.entrada.saida.Dominio;
using Desafio.Entrada.Saida.Servico;
using Desafio.Entrada.Saida.Infraestrutura.Repositorios.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmbalagemService, EmbalagemService>();
builder.Services.AddScoped<IRepositorioCaixa, RepositorioCaixa>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
