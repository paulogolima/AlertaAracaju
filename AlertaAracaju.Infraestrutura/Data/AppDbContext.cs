using AlertaAracaju.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace AlertaAracaju.Infraestrutura.Data;

public class AppDbContext(DbContextOptions<AppDbContext> opts) : DbContext(opts)
{
    public DbSet<Ocorrencia> Ocorrencias { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Ensina ao banco que Ocorrencia "possui uma" propriedade complexa
        modelBuilder.Entity<Ocorrencia>().OwnsOne(o => o.Localizacao);
    }

    // Converte os Enum em string
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<Enum>().HaveConversion<string>();
    }
}