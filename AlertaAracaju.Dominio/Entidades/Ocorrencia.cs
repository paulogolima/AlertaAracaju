using AlertaAracaju.Dominio.Enums;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AlertaAracaju.Dominio.Entidades;

// Classe onde é guardado todo que uma ocorrencia precisa.
public class Ocorrencia
{
    protected Ocorrencia()
    {
    }

    public Ocorrencia(int id, string titulo, string detalhes, StatusOcorrencia status, Coordenadas localizacao)
    {
        Id = id;
        Titulo = titulo;
        Detalhes = detalhes;
        DataEHora = DateTime.UtcNow;
        Status = status;
        Localizacao = localizacao;
    }

    // Propriedades
    public int Id { get; private set; }
    public string Titulo { get; private set; }
    public string Detalhes { get; private set; }
    public DateTime DataEHora { get; private set; }
    public Categoria Categoria { get; private set; }
    public StatusOcorrencia Status { get; private set; }
    public Urgencia Urgencia { get; private set; }
    public Coordenadas Localizacao { get; private set; }
}

// Classe que guarda a coordenada do local
[Owned]
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