using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using AcervoFilmesAPI.Domain.AssociativeEntity;

namespace AcervoFilmesAPI.Domain.Model
{
    public class Filme
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(200, ErrorMessage = "O título não pode exceder 200 caracteres.")]
        public string Titulo { get; set; }
        [StringLength(1000, ErrorMessage = "A descrição não pode exceder 1000 caracteres.")]
        public string Descricao { get; set; }
        [Range(1900, 2100)] // para ganrantir que vai ter um ano de lançamento coerente
        public int AnoLancamento { get; set; }
        [Range(1, 12)] // Meses entre 1 e 12
        public int MesLancamento { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A duração deve ser positiva.")]
        public int Duracao { get; set; } // em minutos
        [Required(ErrorMessage = "O gênero é obrigatório.")]
        public int GeneroId { get; set; }
        public Genero Genero { get; set; }

        public ICollection<FilmeStreaming> FilmeStreamings { get; set; }
        [StringLength(200, ErrorMessage = "O nome do diretor não pode exceder 200 caracteres.")]
        public string Diretor { get; set; }
        [StringLength(50, ErrorMessage = "A classificação indicativa não pode exceder 50 caracteres.")]
        public string ClassificacaoIndicativa { get; set; }
        public DateTime DataAdicao { get; set; }
        public ICollection<Avaliacao> Avaliacoes { get; set; }
        public Filme()
        {
            DataAdicao = DateTime.UtcNow;
        }
    }
}
