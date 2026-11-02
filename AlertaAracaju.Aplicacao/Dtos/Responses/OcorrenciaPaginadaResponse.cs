namespace AlertaAracaju.Aplicacao.Dtos.Responses;

public class OcorrenciaPaginadaResponse
{
    public List<OcorrenciaResponse> Data { get; set; } = new();
    public int Total { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
    public int TotalPages { get; set; }
}
