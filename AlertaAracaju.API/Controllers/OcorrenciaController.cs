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

        if (!Enum.TryParse<StatusOcorrencia>(analise.Status, ignoreCase: true, out var status))
        {
            status = StatusOcorrencia.Pendente;
        }

        if (!Enum.TryParse<Categoria>(analise.Categoria, ignoreCase: true, out var categoria))
        {
            categoria = Categoria.Outros;
        }

        if (!Enum.TryParse<Urgencia>(analise.Urgencia, ignoreCase: true, out var urgencia))
        {
            urgencia = Urgencia.Media;
        }

        var ocorrencia = new Ocorrencia(
            id: 0,
            titulo: analise.Titulo,
            detalhes: analise.Detalhes,
            status: status,
            categoria: categoria,
            urgencia: urgencia,
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

    [HttpPatch("{id}")]
    public async Task<IActionResult> AtualizarOcorrenciaParcial(int id, JsonPatchDocument<AtualizarOcorrenciaPatchRequest> patch)
    {
        if (patch == null)
        {
            return BadRequest("O documento de patch não pode ser nulo.");
        }

        var ocorrencia = await repositorio.ListarPorIdAsync(id);
        if (ocorrencia == null) return NotFound();
        var ocorrenciaParaAtualizar = new AtualizarOcorrenciaPatchRequest();

        try
        {
            patch.ApplyTo(ocorrenciaParaAtualizar, ModelState);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest($"Operação de patch inválida: {ex.Message}");
        }

        if (!TryValidateModel(ocorrenciaParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        mapper.Map(ocorrenciaParaAtualizar, ocorrencia);
        await repositorio.AtualizarOcorrenciaParcialAsync(ocorrencia);
        return NoContent();
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> AtualizarStatus(int id, [FromBody] AtualizarStatusRequest requisicao)
    {
        var ocorrencia = await repositorio.ListarPorIdAsync(id);
        if (ocorrencia == null) return NotFound();

        if (!Enum.TryParse<StatusOcorrencia>(requisicao.Status, ignoreCase: true, out var novoStatus))
        {
            return BadRequest("Status inválido");
        }

        var ocorrenciaAtualizada = new Ocorrencia(
            id: ocorrencia.Id,
            titulo: ocorrencia.Titulo,
            detalhes: ocorrencia.Detalhes,
            status: novoStatus,
            categoria: ocorrencia.Categoria,
            urgencia: ocorrencia.Urgencia,
            localizacao: ocorrencia.Localizacao
        );

        await repositorio.AtualizarOcorrenciaAsync(ocorrenciaAtualizada);
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