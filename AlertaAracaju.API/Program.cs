using AlertaAracaju.Aplicacao.Profiles;
using AlertaAracaju.Aplicacao.Servicos;
using AlertaAracaju.Dominio.Interface;
using AlertaAracaju.Infraestrutura.Data;
using AlertaAracaju.Infraestrutura.Repositorios;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("OcorrenciaConnection");
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseNpgsql(connectionString, b => b.MigrationsAssembly("AlertaAracaju.API")));

builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers().AddJsonOptions(opts =>
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false)));

builder.Services.AddProblemDetails();

builder.Services.AddOpenApi();

builder.Services.AddScoped<IOcorrenciaRepository, OcorrenciaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddHttpClient<IServicoIA, GeminiAIService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(5);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.Services.GetRequiredService<IMapper>()
        .ConfigurationProvider.AssertConfigurationIsValid();
}

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();

app.Run();