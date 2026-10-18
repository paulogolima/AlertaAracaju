using AlertaAracaju.Dominio.Interface;
using AlertaAracaju.Infraestrutura.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirHTML", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500") // O endereço exato do seu Live Server
              .AllowAnyHeader()
              .AllowAnyMethod(); // Permite GET, POST, etc.
    });
});
builder.Services.AddSingleton<IOcorrenciaRepository, OcorrenciaRepository>();

var app = builder.Build();

app.UseCors("PermitirHTML"); // Liga a regra que criamos lá em cima!
app.UseAuthorization();
app.MapControllers();
app.Run();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
