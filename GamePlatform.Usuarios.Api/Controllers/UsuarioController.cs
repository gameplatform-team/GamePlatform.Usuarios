using GamePlatform.Usuarios.Application.DTOs.Usuario;
using GamePlatform.Usuarios.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamePlatform.Usuarios.Api.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuarioController(IUsuarioService usuarioService, IUsuarioContextService usuarioContext) : ControllerBase
{
    private readonly IUsuarioService _usuarioService = usuarioService;
    private readonly IUsuarioContextService _usuarioContext = usuarioContext;

    /// <summary>
    /// Obtém os dados do usuário autenticado.
    /// </summary>
    /// <response code="200">Usuário encontrado com sucesso</response>
    /// <response code="404">Usuário não encontrado</response>
    [HttpGet("perfil")]
    [Authorize]
    public async Task<IActionResult> GetPerfil()
    {
        var id = _usuarioContext.GetUsuarioId();
        var usuario = await _usuarioService.ObterPorIdAsync(id);
        return usuario is null ? NotFound() : Ok(usuario);
    }

    /// <summary>
    /// Obtém os dados de um usuário específico pelo seu ID (somente administradores).
    /// </summary>
    /// <param name="id">ID do usuário</param>
    /// <response code="200">Usuário encontrado com sucesso</response>
    /// <response code="404">Usuário não encontrado</response>
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var usuario = await _usuarioService.ObterPorIdAsync(id);
        return usuario is null ? NotFound() : Ok(usuario);
    }

    /// <summary>
    /// Promove um usuário para administrador.
    /// </summary>
    /// <param name="id">Identificador único do usuário</param>
    /// <response code="204">Usuário promovido com sucesso</response>
    /// <response code="400">Erro na promoção (ex: usuário já é administrador ou não encontrado)</response>
    [HttpPut("{id:guid}/promover")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PromoverAsync(Guid id)
    {
        await _usuarioService.PromoverAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Lista todos os usuários cadastrados.
    /// </summary>
    /// <response code="200">Lista de usuários retornada com sucesso</response>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ObterTodosAsync()
    {
        var usuarios = await _usuarioService.ObterTodosAsync();
        return Ok(usuarios);
    }

    /// <summary>
    /// Atualiza os dados de um usuário específico pelo seu ID (somente administradores).
    /// </summary>
    /// <param name="id">ID do usuário</param>
    /// <param name="dto">Dados para atualização do usuário</param>
    /// <response code="204">Usuário atualizado com sucesso</response>
    /// <response code="400">Erro na atualização (ex: e-mail em uso ou usuário não encontrado)</response>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AtualizarById(Guid id, [FromBody] AtualizarUsuarioDto dto)
    {
        var resultado = await _usuarioService.AtualizarAsync(id, dto);
        return resultado.Sucesso ? NoContent() : BadRequest(resultado.Mensagem);
    }

    /// <summary>
    /// Exclui (desativa) o usuário especificado pelo ID.
    /// O usuário será marcado como inativo e um evento de exclusão será registrado.
    /// </summary>
    /// <param name="id">ID do usuário a ser excluído</param>
    /// <response code="204">Usuário excluído com sucesso</response>
    /// <response code="404">Usuário não encontrado</response>
    /// <response code="500">Erro ao excluir o usuário</response>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ExcluirAsync(Guid id)
    {
        try
        {
            await _usuarioService.ExcluirAsync(id);
            return NoContent();
        }
        catch (Exception ex) when (ex.Message.Contains("não encontrado"))
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
