using AlertaAracaju.Dominio.Enums;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace AlertaAracaju.Dominio.Entidades;

// Classe onde é guardado todo que uma ocorrencia precisa.
public class Ocorrencia
{
    private static int _contadorId = 0;

    public Ocorrencia(int id ,string titulo, string detalhes, StatusOcorrencia status, Coordenadas localizacao)
    {
        _contadorId++;

        Id = _contadorId;
        Titulo = titulo;
        Detalhes = detalhes;
        DataEHora = DateTime.Now;
        Status = status;
        Localizacao = localizacao;
    }

    // Propriedades
    public int Id { get; private set; }

    public string Titulo { get; set; }
    [Required(ErrorMessage = "Os detalhes da ocorrência é obrigatório!")]
    public string Detalhes { get; set; }
    public DateTime DataEHora { get; set; } = DateTime.Now;

    // Propriedade computada para data formatada
    public string DataFormatada => DataEHora.ToString("dd/MM/yyyy HH:mm:ss");                     //OBS: Tirar Formatação de data da classe e trasnsferiar para um dto, mapeando no controller

    public StatusOcorrencia Status { get; set; }
    public Coordenadas Localizacao { get; set; }

}

// Classe que guarda a coordenada do local
public class Coordenadas
{
    public double Latitude { get; }
    public double Longitude { get; }

    public Coordenadas(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
