using AlertaAracaju.Dominio.Enums;
using System.Reflection.Metadata.Ecma335;

namespace AlertaAracaju.Dominio.Entidades;
public class Ocorrencia
{
    protected Ocorrencia()
    {
        Titulo = null!;
        Detalhes = null!;
        Localizacao = null!;
    }

    public Ocorrencia(int id, string titulo, string detalhes, StatusOcorrencia status,Categoria categoria, Urgencia urgencia, Coordenadas localizacao)
    {
        Id = id;
        Titulo = titulo;
        Detalhes = detalhes;
        DataEHora = DateTime.UtcNow;
        Status = status;
        Categoria = categoria;
        Urgencia = urgencia;
        Localizacao = localizacao;
    }

    // Propriedades
    public int Id { get; private set; }
    public string Titulo { get; private set; }
    public string Detalhes { get; private set; }
    public DateTime DataEHora { get; private set; } = DateTime.UtcNow;
    public Categoria Categoria { get; private set; }
    public StatusOcorrencia Status { get; private set; }
    public Urgencia Urgencia { get; private set; }
    public int Apoio { get; private set; } = 0;
    public Coordenadas Localizacao { get; private set; }
}

public class Coordenadas
{
    protected Coordenadas()
    { 
    }

    public Coordenadas(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
}