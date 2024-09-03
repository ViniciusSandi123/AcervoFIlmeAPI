using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Prototipo2.Domain.AssociativeEntity;

namespace Prototipo2.Domain.Model
{
    public class Streaming
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "O nome do streaming não pode exceder 100 caracteres.")]
        public string Name { get; set; }
        public ICollection<FilmeStreaming> FilmeStreamings { get; set; }

        public Streaming(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
