using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using AcervoFilmesAPI.Domain.Model;
using AcervoFilmesAPI.Domain.AssociativeEntity;
using AcervoFilmesAPI.Domain.Interfaces;

public class AvaliacaoRepository : IAvaliacao
{
    private readonly Context _context;
    private readonly IFilme _filmeRepository;

    public AvaliacaoRepository(Context context, IFilme filmeRepository)
    {
        _context = context;
        _filmeRepository = filmeRepository;
    }

    public void Add(Avaliacao avaliacao)
    {
        _context.Avaliacoes.Add(avaliacao);
        _context.SaveChanges();
        AtualizarMediaFilme(avaliacao.FilmeId);
    }

    public List<Avaliacao> GetList()
    {
        return _context.Avaliacoes
            .Include(a => a.Filme)
            .ToList();
    }

    public Avaliacao GetById(int id)
    {
        return _context.Avaliacoes
            .Include(a => a.Filme)
            .FirstOrDefault(a => a.Id == id);
    }

    public void Update(Avaliacao avaliacao)
    {
        var existingAvaliacao = _context.Avaliacoes
            .FirstOrDefault(a => a.Id == avaliacao.Id);

        if (existingAvaliacao != null)
        {
            existingAvaliacao.Nota = avaliacao.Nota;
            existingAvaliacao.Comentario = avaliacao.Comentario;
            existingAvaliacao.DataAvaliacao = avaliacao.DataAvaliacao;
            existingAvaliacao.Apelido = avaliacao.Apelido;

            _context.SaveChanges();
            AtualizarMediaFilme(existingAvaliacao.FilmeId);
        }
    }

    public void Delete(int id)
    {
        var avaliacao = _context.Avaliacoes.Find(id);
        if (avaliacao != null)
        {
            var filmeId = avaliacao.FilmeId;
            _context.Avaliacoes.Remove(avaliacao);
            _context.SaveChanges();
            AtualizarMediaFilme(filmeId);
        }
    }

    public int CalcularMediaNotas(int filmeId)
    {
        var notas = _context.Avaliacoes
            .Where(a => a.FilmeId == filmeId)
            .Select(a => a.Nota)
            .ToList();

        if (notas.Count == 0)
        {
            return 0; // Nenhuma nota disponível
        }

        var media = notas.Average();

        // Adapta a média para a escala 1-5
        if (media <= 1.5)
        {
            return 1;
        }
        else if (media <= 2.5)
        {
            return 2;
        }
        else if (media <= 3.5)
        {
            return 3;
        }
        else if (media <= 4.5)
        {
            return 4;
        }
        else
        {
            return 5;
        }
    }

    private void AtualizarMediaFilme(int filmeId)
    {
        var media = CalcularMediaNotas(filmeId);

        var filmesMedia = _context.FilmesMedia.SingleOrDefault(fm => fm.FilmeId == filmeId);
        if (filmesMedia == null)
        {
            _context.FilmesMedia.Add(new FilmesMedia
            {
                FilmeId = filmeId,
                Media = media
            });
        }
        else
        {
            filmesMedia.Media = media;
            _context.FilmesMedia.Update(filmesMedia);
        }

        _context.SaveChanges();
    }
}
