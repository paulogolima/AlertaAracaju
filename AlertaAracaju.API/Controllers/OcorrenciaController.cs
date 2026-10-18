
using AlertaAracaju.Dominio.Entidades;
using AlertaAracaju.Dominio.Interface;
using Microsoft.AspNetCore.Mvc;


namespace AlertaAracaju.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OcorrenciaController : ControllerBase
{
   
    private readonly IOcorrenciaRepository _repositorio;

    public OcorrenciaController(IOcorrenciaRepository repositorio)
    {
        _repositorio = repositorio;
    }

    [HttpPost]
    public void AdicionarOcorrencia([FromBody]Ocorrencia ocorrencia)
    {
        if(!string.IsNullOrEmpty(ocorrencia.Detalhes))
        {
            _repositorio.AdicionarOcorrencia(ocorrencia);
        }
        
    }

    [HttpGet]
    public IEnumerable<Ocorrencia> ListarTodasOcorrencias()
    {

        return _repositorio.ListarTodos();
    }

}
