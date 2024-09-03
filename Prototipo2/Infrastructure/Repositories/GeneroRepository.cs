using Microsoft.EntityFrameworkCore;
using Prototipo2.Domain.Interfaces;
using Prototipo2.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

public class GeneroRepository : IGenero
{
    private readonly Context _context;

    public GeneroRepository(Context context)
    {
        _context = context;
    }

    public void Add(Genero genero)
    {
        try
        {
            _context.Generos.Add(genero);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            // Adicione um log ou capture o erro de forma adequada
            throw new ApplicationException("An error occurred while adding the genero.", ex);
        }
    }

    public List<Genero> GetList(int pageNumber, int pageQuantity)
    {
        if (pageNumber < 1 || pageQuantity < 1)
        {
            throw new ArgumentException("Page number and page quantity must be greater than 0.");
        }

        return _context.Generos
                       .Skip((pageNumber - 1) * pageQuantity)
                       .Take(pageQuantity)
                       .ToList();
    }

    public Genero GetById(int id)
    {
        return _context.Generos.FirstOrDefault(g => g.Id == id);
    }

    public void Update(Genero genero)
    {
        var existingGenero = _context.Generos.FirstOrDefault(g => g.Id == genero.Id);

        if (existingGenero == null)
        {
            throw new ArgumentException("O gênero não existe");
        }

        existingGenero.Name = genero.Name;

        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var genero = _context.Generos.Find(id);
        if (genero != null)
        {
            _context.Generos.Remove(genero);
            _context.SaveChanges();
        }
    }
}
