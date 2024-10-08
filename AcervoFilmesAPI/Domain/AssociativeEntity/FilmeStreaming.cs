﻿using AcervoFilmesAPI.Domain.Model;

namespace AcervoFilmesAPI.Domain.AssociativeEntity
{
    public class FilmeStreaming
    {
        public int FilmeId { get; set; }
        public Filme Filme { get; set; }

        public int StreamingId { get; set; }
        public Streaming Streaming { get; set; }
    }
}
