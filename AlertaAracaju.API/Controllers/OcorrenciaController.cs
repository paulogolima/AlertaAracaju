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

    //POST
    [HttpPost]
    public IActionResult AdicionarOcorrencia([FromBody] Ocorrencia ocorrencia)
    {
        _repositorio.AdicionarOcorrencia(ocorrencia);
        return CreatedAtAction(nameof(ListarOcorrenciasPorId), new { id = ocorrencia.Id }, ocorrencia);
    }

    //GET
    [HttpGet]
    public IEnumerable<Ocorrencia> ListarTodasOcorrencias([FromQuery] int skip = 0, [FromQuery] int take = 5)
    {
        return _repositorio.ListarTodos(skip, take);
    }

    [HttpGet("{id}")]
    public IActionResult ListarOcorrenciasPorId(int id)
    {
        var ocorrencia = _repositorio.ListarPorId(id);
        if (ocorrencia == null) return NotFound();
        return Ok(ocorrencia);
    }
}