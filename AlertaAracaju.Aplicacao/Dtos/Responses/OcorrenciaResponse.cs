namespace AlertaAracaju.Aplicacao.Dtos.Responses;

public class OcorrenciaResponse
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public string Detalhes { get; set; } = null!;
    public DateTime DataEHora { get; set; }
    public string Categoria { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string Urgencia { get; set; } = null!;
    public int Apoio { get; set; }
    public CoordenadasResponse Localizacao { get; set; } = null!;
}
