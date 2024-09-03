using Prototipo2.Domain.Model;

namespace Prototipo2.Domain.AssociativeEntity
{
    public class FilmesMedia
    {
        public int FilmeId { get; set; }
        public Filme Filme { get; set; }

        public int Media { get; set; }
    }
}
