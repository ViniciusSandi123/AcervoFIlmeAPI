using Microsoft.EntityFrameworkCore;
using AcervoFilmesAPI.Domain.AssociativeEntity;
using AcervoFilmesAPI.Domain.Model;
using AcervoFilmesAPI.Domain.Interfaces;

public class FilmeRepository : IFilme
{
    private readonly Context _context;
    private readonly DbSet<Filme> _filmes;
    private readonly DbSet<FilmeStreaming> _filmeStreamings;

    public FilmeRepository(Context context)
    {
        _context = context;
        _filmes = context.Set<Filme>();
        _filmeStreamings = context.Set<FilmeStreaming>();
    }

    public void Add(Filme filme)
    {
        _filmes.Add(filme);
        _context.SaveChanges();
    }

    public List<Filme> List => _filmes.Include(f => f.Genero)
                      .Include(f => f.FilmeStreamings)
                      .ThenInclude(fs => fs.Streaming)
                      .ToList();

    public Filme GetById(int id)
    {
        return _filmes.Include(f => f.Genero)
                      .Include(f => f.FilmeStreamings)
                      .ThenInclude(fs => fs.Streaming)
                      .FirstOrDefault(f => f.Id == id);
    }

    public void Update(Filme filme)
    {
        _filmes.Update(filme);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var filme = _filmes.Find(id);
        if (filme != null)
        {
            _filmes.Remove(filme);
            _context.SaveChanges();
        }
    }

    public void AddFilmeStreaming(FilmeStreaming filmeStreaming)
    {
        _filmeStreamings.Add(filmeStreaming);
        _context.SaveChanges();
    }

    public void RemoveAllFilmeStreamings(int filmeId)
    {
        var filmeStreamings = _filmeStreamings.Where(fs => fs.FilmeId == filmeId).ToList();
        _filmeStreamings.RemoveRange(filmeStreamings);
        _context.SaveChanges();
    }

    public List<Filme> GetList()
    {
        return _filmes.Include(f => f.Genero)
                      .Include(f => f.FilmeStreamings)
                      .ThenInclude(fs => fs.Streaming)
                      .ToList();
    }

    public List<Filme> GetListByAnoLancamento(int anoLancamento)
    {
        var filmes = _filmes
            .Where(f => f.AnoLancamento == anoLancamento)
            .Include(f => f.Genero)
            .Include(f => f.FilmeStreamings)
            .ThenInclude(fs => fs.Streaming)
            .ToList();
        return filmes;
    }

    public List<Filme> GetListByMediaNota(int mediaNota)
    {
        var filmesIds = _context.FilmesMedia
            .Where(fm => fm.Media == mediaNota)
            .Select(fm => fm.FilmeId)
            .ToList();

        var filmes = _context.Filmes
            .Include(f => f.Genero)
            .Include(f => f.FilmeStreamings)
            .ThenInclude(fs => fs.Streaming)
            .Where(f => filmesIds.Contains(f.Id))
            .ToList();

        return filmes;
    }

    public MediaPeriodo GetMediaPeriodoByAno(int anoLancamento)
    {
        var filmes = _filmes
            .Where(f => f.AnoLancamento == anoLancamento)
            .ToList();

        if (filmes.Count == 0)
        {
            return null;
        }

        var qtdFilmes = filmes.Count;
        var media = filmes.Average(f => _context.Avaliacoes
            .Where(a => a.FilmeId == f.Id)
            .Average(a => a.Nota));

        var mediaArredondada = ArredondarNota(media);

        return new MediaPeriodo
        {
            AnoLancamento = anoLancamento,
            QtdFilmes = qtdFilmes,
            Media = mediaArredondada
        };
    }

    private int ArredondarNota(double nota)
    {
        if (nota <= 1.5) return 1;
        if (nota <= 2.5) return 2;
        if (nota <= 3.5) return 3;
        if (nota <= 4.5) return 4;
        return 5;
    }

    public void AtualizarMediaPeriodo(int anoLancamento, int qtdFilmes, int mediaPeriodo)
    {
        var mediaPeriodoExistente = _context.MediaPeriodos
            .FirstOrDefault(mp => mp.AnoLancamento == anoLancamento);

        if (mediaPeriodoExistente != null)
        {
            mediaPeriodoExistente.QtdFilmes = qtdFilmes;
            mediaPeriodoExistente.Media = mediaPeriodo;
            _context.MediaPeriodos.Update(mediaPeriodoExistente);
        }
        else
        {
            var novaMediaPeriodo = new MediaPeriodo
            {
                AnoLancamento = anoLancamento,
                QtdFilmes = qtdFilmes,
                Media = mediaPeriodo
            };
            _context.MediaPeriodos.Add(novaMediaPeriodo);
        }

        _context.SaveChanges();
    }

    public void AtualizarMediaGenero(int generoId, int qtdFilmes, int mediaGenero)
    {
        var mediaGeneroExistente = _context.MediaGeneros
            .FirstOrDefault(mg => mg.GeneroId == generoId);

        if (mediaGeneroExistente != null)
        {
            mediaGeneroExistente.QtdFilmes = qtdFilmes;
            mediaGeneroExistente.Media = mediaGenero;
            _context.MediaGeneros.Update(mediaGeneroExistente);
        }
        else
        {
            var novaMediaGenero = new MediaGenero
            {
                GeneroId = generoId,
                QtdFilmes = qtdFilmes,
                Media = mediaGenero
            };
            _context.MediaGeneros.Add(novaMediaGenero);
        }

        _context.SaveChanges();
    }
}
