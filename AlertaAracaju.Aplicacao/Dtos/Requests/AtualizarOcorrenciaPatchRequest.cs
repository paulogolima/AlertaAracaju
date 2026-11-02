namespace AlertaAracaju.Aplicacao.Dtos.Requests;

public class AtualizarOcorrenciaPatchRequest
{
    public string? Titulo { get; set; }
    public string? Detalhes { get; set; }
    public CoordenadasRequest? Localizacao { get; set; }
}