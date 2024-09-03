using Microsoft.EntityFrameworkCore;
using AcervoFilmesAPI.Domain.Interfaces;
using AcervoFilmesAPI.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace AcervoFilmesAPI.Infrastructure.Repositories
{
    public class StreamingRepository : IStreaming
    {
        private readonly Context _context;

        public StreamingRepository(Context context)
        {
            _context = context;
        }

        public void Add(Streaming streaming)
        {
            _context.Streamings.Add(streaming);
            _context.SaveChanges();
        }

        public List<Streaming> GetList()
        {
            return _context.Streamings.ToList();
        }

        public Streaming GetById(int id)
        {
            return _context.Streamings.FirstOrDefault(s => s.Id == id);
        }

        public void Update(Streaming streaming)
        {
            var existeStreaming = _context.Streamings.Find(streaming.Id);
            if (existeStreaming == null)
            {
                throw new ArgumentException("O streaming não existe");
            }

            existeStreaming.Name = streaming.Name;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var streaming = _context.Streamings.Find(id);
            if (streaming != null)
            {
                _context.Streamings.Remove(streaming);
                _context.SaveChanges();
            }
        }
    }
}
