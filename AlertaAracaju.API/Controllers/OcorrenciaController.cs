using AlertaAracaju.Aplicacao.Dtos.Requests;
using AlertaAracaju.Dominio.Entidades;
using AlertaAracaju.Dominio.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace AlertaAracaju.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OcorrenciaController : ControllerBase
{
    private readonly IOcorrenciaRepository _repositorio;
    private IMapper _mapper;
    public OcorrenciaController(IOcorrenciaRepository repositorio, IMapper mapper)
    {
        _mapper = mapper;
        _repositorio = repositorio;
    }

    //POST
    [HttpPost]
    public IActionResult AdicionarOcorrencia([FromBody] CriarOcorrenciaRequest ocorrenciaRequest)
    {
        Ocorrencia ocorrencia = _mapper.Map<Ocorrencia>(ocorrenciaRequest);
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