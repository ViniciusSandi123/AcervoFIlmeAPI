using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AcervoFilmesAPI.Application.ViewModel;
using AcervoFilmesAPI.Domain.Interfaces;
using AcervoFilmesAPI.Domain.Model;
using Microsoft.Extensions.Logging;
using System.Net;

[ApiController]
[Route("api/v1/[controller]")]
public class AvaliacaoController : ControllerBase
{
    private readonly IAvaliacao _avaliacaoRepository;
    private readonly IFilme _filmeRepository;
    private readonly ILogger<AvaliacaoController> _logger;

    public AvaliacaoController(IAvaliacao avaliacaoRepository, IFilme filmeRepository, ILogger<AvaliacaoController> logger)
    {
        _avaliacaoRepository = avaliacaoRepository ?? throw new ArgumentNullException(nameof(avaliacaoRepository));
        _filmeRepository = filmeRepository ?? throw new ArgumentNullException(nameof(filmeRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    private IActionResult HandleError(Exception ex, string message = "Erro interno do servidor", HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    {
        _logger.LogError(ex, message);
        return StatusCode((int)statusCode, new { error = message, details = ex.Message });
    }

    [HttpPost]
    public IActionResult Add([FromBody] AvaliacaoViewModel avaliacaoViewModel)
    {
        if (avaliacaoViewModel == null)
        {
            _logger.LogWarning("AvaliacaoViewModel is null");
            return BadRequest("AvaliacaoViewModel is null");
        }

        var filme = _filmeRepository.GetById(avaliacaoViewModel.FilmeId);

        if (filme == null)
        {
            return BadRequest($"Filme com ID {avaliacaoViewModel.FilmeId} não encontrado.");
        }

        var avaliacao = new Avaliacao
        {
            FilmeId = avaliacaoViewModel.FilmeId,
            Nota = avaliacaoViewModel.Nota,
            Comentario = avaliacaoViewModel.Comentario,
            Apelido = avaliacaoViewModel.Apelido
        };

        try
        {
            _avaliacaoRepository.Add(avaliacao);
            return CreatedAtAction(nameof(GetById), new { id = avaliacao.Id }, avaliacao);
        }
        catch (Exception ex)
        {
            return HandleError(ex, "Erro ao adicionar avaliação");
        }
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var avaliacoes = _avaliacaoRepository.GetList();
            return Ok(avaliacoes);
        }
        catch (Exception ex)
        {
            return HandleError(ex, "Erro ao obter a lista de avaliações");
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var avaliacao = _avaliacaoRepository.GetById(id);
            if (avaliacao == null)
            {
                return NotFound("Avaliacao not found");
            }
            return Ok(avaliacao);
        }
        catch (Exception ex)
        {
            return HandleError(ex, "Erro ao obter a avaliação");
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] AvaliacaoViewModel avaliacaoViewModel)
    {
        if (avaliacaoViewModel == null)
        {
            return BadRequest("AvaliacaoViewModel is null");
        }

        if (id != avaliacaoViewModel.Id)
        {
            return BadRequest("ID mismatch");
        }

        var existingAvaliacao = _avaliacaoRepository.GetById(id);
        if (existingAvaliacao == null)
        {
            return NotFound("Avaliacao not found");
        }

        existingAvaliacao.Nota = avaliacaoViewModel.Nota;
        existingAvaliacao.Comentario = avaliacaoViewModel.Comentario;
        existingAvaliacao.Apelido = avaliacaoViewModel.Apelido;

        try
        {
            _avaliacaoRepository.Update(existingAvaliacao);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleError(ex, "Erro ao atualizar avaliação");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var avaliacao = _avaliacaoRepository.GetById(id);
            if (avaliacao == null)
            {
                return NotFound("Avaliacao not found");
            }

            _avaliacaoRepository.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleError(ex, "Erro ao excluir avaliação");
        }
    }
}
