using AlertaAracaju.Aplicacao.Profiles;
using AlertaAracaju.Aplicacao.Servicos;
using AlertaAracaju.Dominio.Interface;
using AlertaAracaju.Infraestrutura.Data;
using AlertaAracaju.Infraestrutura.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("OcorrenciaConnection");
builder.Services.AddDbContext<OcorrenciaContext>(opts =>
    opts.UseNpgsql(connectionString, b => b.MigrationsAssembly("AlertaAracaju.API")));

builder.Services.AddAutoMapper(cfg => { }, typeof(OcorrenciaProfile).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddProblemDetails();

builder.Services.AddOpenApi();
builder.Services.AddScoped<IOcorrenciaRepository, OcorrenciaRepository>();

builder.Services.AddHttpClient<IServicoIA, GeminiAIService>(client =>
{
    client.Timeout = TimeSpan.FromSeconds(5);
});

var app = builder.Build();

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
