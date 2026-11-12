using AlertaAracaju.Aplicacao.Dtos.Requests.Ocorrencia;
using AlertaAracaju.Aplicacao.Dtos.Responses.Ocorrencia;

using AlertaAracaju.Aplicacao.Servicos;
using AlertaAracaju.Dominio.Entidades;
using AlertaAracaju.Dominio.Enums;
using AlertaAracaju.Dominio.Interface;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AlertaAracaju.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OcorrenciaController(IOcorrenciaRepository repositorio, IMapper mapper, IServicoIA servicoIA) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CriarOcorrencia([FromBody] CriarOcorrenciaRequest requisicao)
    {
        var analise = await servicoIA.AnalisarOcorrenciaAsync(requisicao.Titulo!, requisicao.Detalhes!);

        var ocorrencia = new Ocorrencia(
            id: 0,
            titulo: analise.Titulo,
            detalhes: analise.Detalhes,
            status: StatusOcorrencia.Pendente,
            categoria: analise.Categoria,
            urgencia: analise.Urgencia,
            localizacao: mapper.Map<Coordenadas>(requisicao.Localizacao!)
        );

        await repositorio.CriarOcorrenciaAsync(ocorrencia);
        var resposta = mapper.Map<OcorrenciaResponse>(ocorrencia);
        return CreatedAtAction(nameof(ListarOcorrenciaPorId), new { id = ocorrencia.Id }, resposta);
    }

    [HttpGet]
    public async Task<IActionResult> ListarTodasOcorrencias([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        skip = Math.Max(skip, 0);
        take = Math.Clamp(take, 1, 50);
        var ocorrencias = await repositorio.ListarTodosAsync(skip, take);
        var resposta = mapper.Map<List<OcorrenciaResponse>>(ocorrencias);
        return Ok(resposta);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ListarOcorrenciaPorId(int id)
    {
        var ocorrencia = await repositorio.ListarPorIdAsync(id);
        if (ocorrencia == null) return NotFound();
        var resposta = mapper.Map<OcorrenciaResponse>(ocorrencia);
        return Ok(resposta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarOcorrencia(int id, [FromBody] AtualizarOcorrenciaRequest requisicao)
    {
        var ocorrencia = await repositorio.ListarPorIdAsync(id);
        if (ocorrencia == null) return NotFound();
        mapper.Map(requisicao, ocorrencia);
        await repositorio.AtualizarOcorrenciaAsync(ocorrencia);
        return NoContent();
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> AtualizarStatus(int id, [FromBody] AtualizarStatusRequest requisicao)
    {
        var ocorrencia = await repositorio.ListarPorIdAsync(id);
        if (ocorrencia == null) return NotFound();

        ocorrencia.AlterarStatus(requisicao.Status!.Value);
        await repositorio.AtualizarOcorrenciaAsync(ocorrencia);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarOcorrencia(int id)
    {
        var ocorrencia = await repositorio.ListarPorIdAsync(id);
        if (ocorrencia == null) return NotFound();
        await repositorio.DeletarOcorrenciaAsync(ocorrencia);
        return NoContent();
    }
}