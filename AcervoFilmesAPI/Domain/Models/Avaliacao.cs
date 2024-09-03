using System;
using System.ComponentModel.DataAnnotations;

namespace AcervoFilmesAPI.Domain.Model
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public string Apelido { get; set; }
        public int FilmeId { get; set; }
        public Filme Filme { get; set; }
        public int Nota { get; set; } // avaliação vai ser de 1 a 5 utilizando somente inteiros

        public string Comentario { get; set; }

        public DateTime DataAvaliacao { get; set; }

        public Avaliacao()
        {
            DataAvaliacao = DateTime.UtcNow;
        }
    }
}
