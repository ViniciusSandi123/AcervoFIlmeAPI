﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Prototipo2.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240902183524_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Prototipo2.Domain.Model.Avaliacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apelido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Comentario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataAvaliacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("FilmeId")
                        .HasColumnType("int");

                    b.Property<int>("Nota")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FilmeId");

                    b.ToTable("Avaliacoes");
                });

            modelBuilder.Entity("Prototipo2.Domain.Model.Filme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnoLancamento")
                        .HasColumnType("int");

                    b.Property<string>("ClassificacaoIndicativa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataAdicao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Diretor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duracao")
                        .HasColumnType("int");

                    b.Property<int>("GeneroId")
                        .HasColumnType("int");

                    b.Property<int>("MesLancamento")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GeneroId");

                    b.ToTable("Filmes");
                });

            modelBuilder.Entity("Prototipo2.Domain.Model.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Generos");
                });

            modelBuilder.Entity("Prototipo2.Domain.Model.MediaGenero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GeneroId")
                        .HasColumnType("int");

                    b.Property<int>("Media")
                        .HasColumnType("int");

                    b.Property<int>("QtdFilmes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GeneroId");

                    b.ToTable("MediaGeneros");
                });

            modelBuilder.Entity("Prototipo2.Domain.Model.MediaPeriodo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnoLancamento")
                        .HasColumnType("int");

                    b.Property<int>("Media")
                        .HasColumnType("int");

                    b.Property<int>("QtdFilmes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MediaPeriodos");
                });

            modelBuilder.Entity("Prototipo2.Domain.Model.Streaming", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Streamings");
                });

            modelBuilder.Entity("Prototipo2.Domain.Models.FilmeStreaming", b =>
                {
                    b.Property<int>("FilmeId")
                        .HasColumnType("int");

                    b.Property<int>("StreamingId")
                        .HasColumnType("int");

                    b.HasKey("FilmeId", "StreamingId");

                    b.HasIndex("StreamingId");

                    b.ToTable("FilmeStreamings");
                });

            modelBuilder.Entity("Prototipo2.Domain.Models.FilmesMedia", b =>
                {
                    b.Property<int>("FilmeId")
                        .HasColumnType("int");

                    b.Property<int>("Media")
                        .HasColumnType("int");

                    b.HasKey("FilmeId");

                    b.ToTable("FilmesMedia");
                });

            modelBuilder.Entity("Prototipo2.Domain.Model.Avaliacao", b =>
                {
                    b.HasOne("Prototipo2.Domain.Model.Filme", "Filme")
                        .WithMany("Avaliacoes")
                        .HasForeignKey("FilmeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Filme");
                });

            modelBuilder.Entity("Prototipo2.Domain.Model.Filme", b =>
                {
                    b.HasOne("Prototipo2.Domain.Model.Genero", "Genero")
                        .WithMany()
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genero");
                });

            modelBuilder.Entity("Prototipo2.Domain.Model.MediaGenero", b =>
                {
                    b.HasOne("Prototipo2.Domain.Model.Genero", "Genero")
                        .WithMany()
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genero");
                });

            modelBuilder.Entity("Prototipo2.Domain.Models.FilmeStreaming", b =>
                {
                    b.HasOne("Prototipo2.Domain.Model.Filme", "Filme")
                        .WithMany("FilmeStreamings")
                        .HasForeignKey("FilmeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Prototipo2.Domain.Model.Streaming", "Streaming")
                        .WithMany("FilmeStreamings")
                        .HasForeignKey("StreamingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Filme");

                    b.Navigation("Streaming");
                });

            modelBuilder.Entity("Prototipo2.Domain.Models.FilmesMedia", b =>
                {
                    b.HasOne("Prototipo2.Domain.Model.Filme", "Filme")
                        .WithOne()
                        .HasForeignKey("Prototipo2.Domain.Models.FilmesMedia", "FilmeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Filme");
                });

            modelBuilder.Entity("Prototipo2.Domain.Model.Filme", b =>
                {
                    b.Navigation("Avaliacoes");

                    b.Navigation("FilmeStreamings");
                });

            modelBuilder.Entity("Prototipo2.Domain.Model.Streaming", b =>
                {
                    b.Navigation("FilmeStreamings");
                });
#pragma warning restore 612, 618
        }
    }
}
