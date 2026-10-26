using AlertaAracaju.Aplicacao.Profiles;
using AlertaAracaju.Dominio.Interface;
using AlertaAracaju.Infraestrutura.Data;
using AlertaAracaju.Infraestrutura.Repositorios;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("OcorrenciaConnection");
builder.Services.AddDbContext<OcorrenciaContext>(opts => opts.UseNpgsql(connectionString, b => b.MigrationsAssembly("AlertaAracaju.API")));

builder.Services.AddAutoMapper(cfg => { }, typeof(OcorrenciaProfile).Assembly);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IOcorrenciaRepository, OcorrenciaRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();