using System.ComponentModel.DataAnnotations;

namespace AcervoFilmesAPI.Application.ViewModel
{
    public class StreamingViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "O nome do streaming não pode exceder 100 caracteres.")]
        public string Name { get; set; }
    }
}
