using Microsoft.AspNetCore.Mvc;
using Prototipo2.Application.ViewModel;
using Prototipo2.Domain.Interfaces;
using Prototipo2.Domain.Model;

[ApiController]
[Route("api/v1/Streaming")]
public class StreamingController : ControllerBase
{
    private readonly IStreaming _streamingRepository;

    public StreamingController(IStreaming streamingRepository)
    {
        _streamingRepository = streamingRepository ?? throw new ArgumentNullException(nameof(streamingRepository));
    }

    [HttpPost]
    public IActionResult Add([FromBody] StreamingViewModel streamingViewModel)
    {
        if (streamingViewModel == null)
        {
            return BadRequest("Streaming is null");
        }

        var streaming = new Streaming(streamingViewModel.Name);

        try
        {
            _streamingRepository.Add(streaming);
            return CreatedAtAction(nameof(GetById), new { id = streaming.Id }, streaming);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpGet]
    public IActionResult Get()
    {
        var streamings = _streamingRepository.GetList();
        return Ok(streamings);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var streaming = _streamingRepository.GetById(id);
        if (streaming == null)
        {
            return NotFound("Streaming não encontrado");
        }
        return Ok(streaming);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] StreamingViewModel streamingViewModel)
    {
        if (streamingViewModel == null)
        {
            return BadRequest("StreamingViewModel é nulo");
        }

        // Log os valores recebidos
        Console.WriteLine($"Received ID from URL: {id}, StreamingViewModel ID: {streamingViewModel.Id}");

        if (id != streamingViewModel.Id)
        {
            return BadRequest("ID mismatch");
        }

        var streaming = new Streaming(streamingViewModel.Name)
        {
            Id = streamingViewModel.Id
        };

        try
        {
            _streamingRepository.Update(streaming);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }

        return NoContent();
    }


    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var streaming = _streamingRepository.GetById(id);
        if (streaming == null)
        {
            return NotFound("Streaming não encontrado");
        }

        _streamingRepository.Delete(id);
        return NoContent();
    }
}