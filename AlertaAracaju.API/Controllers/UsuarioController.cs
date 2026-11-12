using AlertaAracaju.Aplicacao.Dtos.Requests.Usuario;
using AlertaAracaju.Aplicacao.Dtos.Responses.Usuario;
using AlertaAracaju.Dominio.Entidades;
using AlertaAracaju.Dominio.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AlertaAracaju.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController(IUsuarioRepository repositorio, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioRequest requisicao)
    {
        var usuario = new Usuario(
            id: 0,
            nome: requisicao.Nome!,
            email: requisicao.Email!,
            senhaHash: requisicao.SenhaHash!
            );

        await repositorio.CriarUsuarioAsync(usuario);
        var resposta = mapper.Map<UsuarioResponse>(usuario);
        return CreatedAtAction(nameof(ListarUsuarioPorId), new { id = usuario.Id }, resposta);
    }

    [HttpGet]
    public async Task<IActionResult> ListarTodosUsuarios([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        skip = Math.Max(skip, 0);
        take = Math.Clamp(take, 1, 50);
        var usuario = await repositorio.ListarTodosAsync(skip, take);
        var resposta = mapper.Map<List<UsuarioResponse>>(usuario);
        return Ok(resposta);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ListarUsuarioPorId(int id)
    {
        var usuario = await repositorio.ListarPorIdAsync(id);
        if (usuario == null) return NotFound();
        var resposta = mapper.Map<UsuarioResponse>(usuario);
        return Ok(resposta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarUsuario(int id, [FromBody] AtualizarUsuarioRequest requisicao)
    {
        var usuario = await repositorio.ListarPorIdAsync(id);
        if (usuario == null) return NotFound();
        mapper.Map(requisicao, usuario);
        await repositorio.AtualizarUsuarioAsync(usuario);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarUsuario(int id)
    {
        var usuario = await repositorio.ListarPorIdAsync(id);
        if (usuario == null) return NotFound();
        await repositorio.DeletarUsuarioAsync(usuario);
        return NoContent();
    }
}