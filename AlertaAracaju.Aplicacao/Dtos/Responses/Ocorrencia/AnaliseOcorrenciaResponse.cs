using AlertaAracaju.Dominio.Enums;

namespace AlertaAracaju.Aplicacao.Dtos.Responses.Ocorrencia;

public class AnaliseOcorrenciaResponse
{
    public string Titulo { get; set; } = string.Empty;
    public string Detalhes { get; set; } = string.Empty;
    public Categoria Categoria { get; set; } = Categoria.Outros;
    public Urgencia Urgencia { get; set; } = Urgencia.Media;
}