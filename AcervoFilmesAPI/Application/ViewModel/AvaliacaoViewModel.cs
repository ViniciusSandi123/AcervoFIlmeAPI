using System.ComponentModel.DataAnnotations;

namespace AcervoFilmesAPI.Application.ViewModel
{
    public class AvaliacaoViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int FilmeId { get; set; } 
        [Required]
        [Range(1, 5)]// as notas obrigatoriamente devem ser de 1 a 5
        public int Nota { get; set; }
        [StringLength(500, ErrorMessage = "O comentário não pode exceder 500 caracteres.")]
        public string Comentario { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "O apelido não pode exceder 100 caracteres.")]
        public string Apelido { get; set; } // Adicione o campo Apelido
    }
}
