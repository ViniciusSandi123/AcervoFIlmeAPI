using System.ComponentModel.DataAnnotations;

namespace Prototipo2.Application.ViewModel
{
    public class GeneroViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "O nome do gênero não pode exceder 100 caracteres.")]
        public string Name { get; set; }

    }
}
