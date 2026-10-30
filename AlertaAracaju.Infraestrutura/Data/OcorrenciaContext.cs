using AlertaAracaju.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace AlertaAracaju.Infraestrutura.Data;

public class OcorrenciaContext : DbContext
{
    public OcorrenciaContext(DbContextOptions<OcorrenciaContext> opts) : base(opts)
    {
    }

    public DbSet<Ocorrencia> Ocorrencias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Ensina ao banco que Ocorrencia "possui uma" propriedade complexa
        modelBuilder.Entity<Ocorrencia>().OwnsOne(o => o.Localizacao);
    }
}