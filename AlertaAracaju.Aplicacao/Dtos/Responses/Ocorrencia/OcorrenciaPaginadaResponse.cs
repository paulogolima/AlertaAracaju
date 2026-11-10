using AlertaAracaju.Aplicacao.Dtos.Responses.Usuario;

namespace AlertaAracaju.Aplicacao.Dtos.Responses.Ocorrencia;

public class OcorrenciaPaginadaResponse
{
    public List<UsuarioResponse> Data { get; set; } = new();
    public int Total { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
    public int TotalPages { get; set; }
}
