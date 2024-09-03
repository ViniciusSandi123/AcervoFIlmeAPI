using Microsoft.AspNetCore.Mvc;
using Prototipo2.Domain.AssociativeEntity;
using Prototipo2.Domain.Interfaces;
using Prototipo2.Domain.Model;

[ApiController]
[Route("api/[controller]")]
public class FilmeController : ControllerBase
{
    private readonly IFilme _filmeRepository;
    private readonly IAvaliacao _avaliacaoRepository;

    public FilmeController(IFilme filmeRepository, IAvaliacao avaliacaoRepository)
    {
        _filmeRepository = filmeRepository ?? throw new ArgumentNullException(nameof(filmeRepository));
        _avaliacaoRepository = avaliacaoRepository ?? throw new ArgumentNullException(nameof(avaliacaoRepository));
    }

    [HttpGet]
    public ActionResult<IEnumerable<Filme>> Get()
    {
        try
        {
            var filmes = _filmeRepository.GetList();
            if (filmes == null || !filmes.Any())
            {
                return NoContent();
            }
            return Ok(filmes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter a lista de filmes: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Filme> Get(int id)
    {
        try
        {
            var filme = _filmeRepository.GetById(id);
            if (filme == null)
            {
                return NotFound();
            }
            return Ok(filme);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter o filme: {ex.Message}");
        }
    }

    [HttpGet("ano/{anoLancamento}")]
    public ActionResult<IEnumerable<Filme>> GetByAnoLancamento(int anoLancamento)
    {
        try
        {
            var filmes = _filmeRepository.GetListByAnoLancamento(anoLancamento);
            if (filmes == null || !filmes.Any())
            {
                return NoContent();
            }
            return Ok(filmes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter filmes por ano de lançamento: {ex.Message}");
        }
    }

    [HttpGet("filmes-com-media/{mediaNota}")]
    public ActionResult<IEnumerable<Filme>> GetByMediaNota(int mediaNota)
    {
        try
        {
            var filmes = _filmeRepository.GetListByMediaNota(mediaNota);
            if (filmes == null || !filmes.Any())
            {
                return NoContent();
            }
            return Ok(filmes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter filmes por média de nota: {ex.Message}");
        }
    }

    [HttpPost]
    public IActionResult Post([FromBody] FilmeViewModel filmeViewModel)
    {
        try
        {
            if (filmeViewModel == null)
            {
                return BadRequest("Dados do filme inválidos.");
            }

            var filme = new Filme
            {
                Titulo = filmeViewModel.Titulo,
                Descricao = filmeViewModel.Descricao,
                AnoLancamento = filmeViewModel.AnoLancamento,
                MesLancamento = filmeViewModel.MesLancamento,
                Duracao = filmeViewModel.Duracao,
                GeneroId = filmeViewModel.GeneroId,
                Diretor = filmeViewModel.Diretor,
                ClassificacaoIndicativa = filmeViewModel.ClassificacaoIndicativa,
                FilmeStreamings = filmeViewModel.StreamingIds.Select(id => new FilmeStreaming { StreamingId = id }).ToList()
            };

            _filmeRepository.Add(filme);
            return CreatedAtAction(nameof(Get), new { id = filme.Id }, filme);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar o filme: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] FilmeViewModel filmeViewModel)
    {
        try
        {
            if (filmeViewModel == null)
            {
                return BadRequest("Dados do filme inválidos.");
            }

            var filmeExistente = _filmeRepository.GetById(id);
            if (filmeExistente == null)
            {
                return NotFound();
            }

            // Atualiza os dados do filme
            filmeExistente.Titulo = filmeViewModel.Titulo;
            filmeExistente.Descricao = filmeViewModel.Descricao;
            filmeExistente.AnoLancamento = filmeViewModel.AnoLancamento;
            filmeExistente.MesLancamento = filmeViewModel.MesLancamento;
            filmeExistente.Duracao = filmeViewModel.Duracao;
            filmeExistente.GeneroId = filmeViewModel.GeneroId;
            filmeExistente.Diretor = filmeViewModel.Diretor;
            filmeExistente.ClassificacaoIndicativa = filmeViewModel.ClassificacaoIndicativa;

            // Remove os streamings antigos
            _filmeRepository.RemoveAllFilmeStreamings(id);

            // Adiciona os novos streamings
            filmeExistente.FilmeStreamings = filmeViewModel.StreamingIds.Select(id => new FilmeStreaming { StreamingId = id }).ToList();

            _filmeRepository.Update(filmeExistente);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao atualizar o filme: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var filmeExistente = _filmeRepository.GetById(id);
            if (filmeExistente == null)
            {
                return NotFound();
            }

            _filmeRepository.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao excluir o filme: {ex.Message}");
        }
    }

    [HttpGet("media-periodo/{ano}")]
    public IActionResult GetMediaPeriodo(int ano)
    {
        try
        {
            var mediaPeriodo = _filmeRepository.GetMediaPeriodoByAno(ano);
            if (mediaPeriodo == null)
            {
                return NotFound();
            }

            _filmeRepository.AtualizarMediaPeriodo(ano, mediaPeriodo.QtdFilmes, mediaPeriodo.Media);

            var resultado = new
            {
                AnoLancamento = ano,
                QtdFilmes = mediaPeriodo.QtdFilmes,
                MediaPeriodo = mediaPeriodo.Media
            };

            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao calcular a média do período: {ex.Message}");
        }
    }

    [HttpGet("{id}/streamings-disponiveis")]
    public IActionResult GetStreamingsDisponiveis(int id)
    {
        try
        {
            var filme = _filmeRepository.GetById(id);
            if (filme == null)
            {
                return NotFound();
            }

            var streamingIds = filme.FilmeStreamings
                .Select(fs => fs.StreamingId)
                .ToList();

            return Ok(streamingIds);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter os streamings disponíveis do filme: {ex.Message}");
        }
    }

    [HttpGet("media-filme/{filmeId}")]
    public IActionResult GetMedia(int filmeId)
    {
        try
        {
            var media = _avaliacaoRepository.CalcularMediaNotas(filmeId);

            if (media == 0)
            {
                return NotFound("Nenhuma avaliação encontrada para o filme.");
            }

            return Ok(new { FilmeId = filmeId, Media = media });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao calcular a média das avaliações: {ex.Message}");
        }
    }
}
