using AlertaAracaju.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace AlertaAracaju.Infraestrutura.Data;

public class OcorrenciaContext : DbContext
{
    public OcorrenciaContext(DbContextOptions<OcorrenciaContext> opts) : base(opts)
    {
        
    }

    public DbSet<Ocorrencia> Ocorrencias { get; set; }
}
