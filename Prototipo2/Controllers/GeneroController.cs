using Microsoft.AspNetCore.Mvc;
using Prototipo2.Domain.Interfaces;
using Prototipo2.Domain.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;

[ApiController]
[Route("api/v1/[controller]")]
public class GeneroController : ControllerBase
{
    private readonly IGenero _generoRepository;
    private readonly ILogger<GeneroController> _logger;

    public GeneroController(IGenero generoRepository, ILogger<GeneroController> logger)
    {
        _generoRepository = generoRepository ?? throw new ArgumentNullException(nameof(generoRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    private IActionResult HandleError(Exception ex, string message = "Erro interno do servidor", HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    {
        _logger.LogError(ex, message);
        return StatusCode((int)statusCode, new { error = message, details = ex.Message });
    }

    [HttpGet]
    public IActionResult Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var generos = _generoRepository.GetList(page, pageSize);
            if (generos == null || !generos.Any())
            {
                return NoContent();
            }
            return Ok(generos);
        }
        catch (Exception ex)
        {
            return HandleError(ex, "Erro ao obter a lista de gêneros");
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var genero = _generoRepository.GetById(id);
            if (genero == null)
            {
                return NotFound();
            }
            return Ok(genero);
        }
        catch (Exception ex)
        {
            return HandleError(ex, "Erro ao obter o gênero");
        }
    }

    [HttpPost]
    public IActionResult Post([FromBody] Genero genero)
    {
        if (genero == null)
        {
            return BadRequest("Genero is null");
        }

        try
        {
            _generoRepository.Add(genero);
            return CreatedAtAction(nameof(GetById), new { id = genero.Id }, genero);
        }
        catch (Exception ex)
        {
            return HandleError(ex, "Erro ao adicionar gênero");
        }
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Genero genero)
    {
        if (genero == null || id != genero.Id)
        {
            return BadRequest("Genero is null or ID mismatch");
        }

        try
        {
            var existingGenero = _generoRepository.GetById(id);
            if (existingGenero == null)
            {
                return NotFound();
            }

            existingGenero.Name = genero.Name;
            _generoRepository.Update(existingGenero);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleError(ex, "Erro ao atualizar gênero");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var genero = _generoRepository.GetById(id);
            if (genero == null)
            {
                return NotFound();
            }

            _generoRepository.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleError(ex, "Erro ao excluir gênero");
        }
    }
}
