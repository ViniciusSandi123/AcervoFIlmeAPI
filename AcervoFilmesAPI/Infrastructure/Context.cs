using Microsoft.EntityFrameworkCore;
using AcervoFilmesAPI.Domain.AssociativeEntity;
using AcervoFilmesAPI.Domain.Model;

public class Context : DbContext
{
    public DbSet<Genero> Generos { get; set; }
    public DbSet<Streaming> Streamings { get; set; }
    public DbSet<Filme> Filmes { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }
    public DbSet<FilmeStreaming> FilmeStreamings { get; set; }
    public DbSet<FilmesMedia> FilmesMedia { get; set; }
    public DbSet<MediaPeriodo> MediaPeriodos { get; set; }
    public DbSet<MediaGenero> MediaGeneros { get; set; }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FilmeStreaming>()
            .HasKey(fs => new { fs.FilmeId, fs.StreamingId });

        modelBuilder.Entity<FilmeStreaming>()
            .HasOne(fs => fs.Filme)
            .WithMany(f => f.FilmeStreamings)
            .HasForeignKey(fs => fs.FilmeId);

        modelBuilder.Entity<FilmeStreaming>()
            .HasOne(fs => fs.Streaming)
            .WithMany(s => s.FilmeStreamings)
            .HasForeignKey(fs => fs.StreamingId);

        // Configuração da entidade FilmesMedia
        modelBuilder.Entity<FilmesMedia>()
            .HasKey(fm => fm.FilmeId);

        modelBuilder.Entity<FilmesMedia>()
            .HasOne(fm => fm.Filme)
            .WithOne()
            .HasForeignKey<FilmesMedia>(fm => fm.FilmeId);

        modelBuilder.Entity<FilmesMedia>()
            .Property(fm => fm.Media)
            .IsRequired();

        // Configuração da entidade MediaPeriodo
        modelBuilder.Entity<MediaPeriodo>()
            .HasKey(mp => mp.Id); // Define a chave primária

        // Configuração da entidade MediaGenero
        modelBuilder.Entity<MediaGenero>()
            .HasKey(mg => mg.Id); // Define a chave primária

        modelBuilder.Entity<MediaGenero>()
            .HasOne(mg => mg.Genero) // Definindo o relacionamento com Genero
            .WithMany() // Se você deseja que Genero não tenha navegação reversa, use WithMany()
            .HasForeignKey(mg => mg.GeneroId)
            .OnDelete(DeleteBehavior.Cascade); // Definindo a ação de exclusão em cascata, se necessário
    }
}
