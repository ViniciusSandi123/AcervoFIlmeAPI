using System.ComponentModel.DataAnnotations;

namespace Prototipo2.Domain.Model
{
    public class Genero
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "O nome do gênero não pode exceder 100 caracteres.")]
        public string Name { get; set; }

        public Genero(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}

