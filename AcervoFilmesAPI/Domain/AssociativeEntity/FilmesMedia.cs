using AcervoFilmesAPI.Domain.Model;

namespace AcervoFilmesAPI.Domain.AssociativeEntity
{
    public class FilmesMedia
    {
        public int FilmeId { get; set; }
        public Filme Filme { get; set; }

        public int Media { get; set; }
    }
}
