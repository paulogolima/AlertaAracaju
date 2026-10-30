using AlertaAracaju.Aplicacao.Dtos.Requests;
using AlertaAracaju.Dominio.Entidades;
using AlertaAracaju.Dominio.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AlertaAracaju.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OcorrenciaController(IOcorrenciaRepository repositorio, IMapper mapper) : ControllerBase
{
    //POST
    [HttpPost]
    public async Task<IActionResult> CriarOcorrencia([FromBody] CriarOcorrenciaRequest ocorrenciaRequest)
    {
        Ocorrencia ocorrencia = mapper.Map<Ocorrencia>(ocorrenciaRequest);
        await repositorio.CriarOcorrenciaAsync(ocorrencia);
        return CreatedAtAction(nameof(ListarOcorrenciasPorId), new { id = ocorrencia.Id }, ocorrencia);
    }

    //GET
    [HttpGet]
    public async Task<IActionResult> ListarTodasOcorrencias([FromQuery] int skip = 0, [FromQuery] int take = 5)
    {
        skip = Math.Max(skip, 0);
        take = Math.Clamp(take, 1, 50);
        var ocorrencias = await repositorio.ListarTodosAsync(skip, take);
        if(ocorrencias == null) return NotFound();
        return Ok(ocorrencias);
        
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> ListarOcorrenciasPorId(int id)
    {
        var ocorrencia = await repositorio.ListarPorIdAsync(id);
        if (ocorrencia == null) return NotFound();
        return Ok(ocorrencia);
    }
}