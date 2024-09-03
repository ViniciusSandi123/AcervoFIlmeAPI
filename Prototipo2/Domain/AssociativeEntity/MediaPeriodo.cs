﻿using System.ComponentModel.DataAnnotations;

namespace Prototipo2.Domain.AssociativeEntity
{
    public class MediaPeriodo
    {
        [Key]
        public int Id { get; set; }
        public int AnoLancamento { get; set; }
        public int QtdFilmes { get; set; }
        public int Media { get; set; }
    }
}
