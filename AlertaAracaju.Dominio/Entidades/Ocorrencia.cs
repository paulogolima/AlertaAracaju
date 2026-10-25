using AlertaAracaju.Dominio.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AlertaAracaju.Dominio.Entidades;

// Classe onde é guardado todo que uma ocorrencia precisa.
public class Ocorrencia
{
    //private static int _contadorId = 0;

    protected Ocorrencia()
    {
    }

    public Ocorrencia(int id, string titulo, string detalhes, StatusOcorrencia status, Coordenadas localizacao)
    {
        //_contadorId++;

        Id = id;
        Titulo = titulo;
        Detalhes = detalhes;
        DataEHora = DateTime.Now;
        Status = status;
        Localizacao = localizacao;
    }

    // Propriedades
    [Key]
    [Required]
    public int Id { get; private set; }
    public string Titulo { get; set; }

    [Required(ErrorMessage = "Os detalhes da ocorrência é obrigatório!")]
    public string Detalhes { get; set; }
    public DateTime DataEHora { get; set; } = DateTime.Now;
    public StatusOcorrencia Status { get; set; }
    public Coordenadas Localizacao { get; set; }

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
    }

    public double Latitude { get; set; }
    public double Longitude { get; set; }

}
