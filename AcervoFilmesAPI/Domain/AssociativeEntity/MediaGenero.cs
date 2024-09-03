using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AcervoFilmesAPI.Domain.Model;

namespace AcervoFilmesAPI.Domain.AssociativeEntity
{
    public class MediaGenero
    {
        [Key]
        public int Id { get; set; }

        public int GeneroId { get; set; }
        public int QtdFilmes { get; set; }
        public int Media { get; set; }

        // Navegação para a entidade Genero
        [ForeignKey("GeneroId")]
        public virtual Genero Genero { get; set; }
    }
}
