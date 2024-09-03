using AcervoFilmesAPI.Domain.AssociativeEntity;
using AcervoFilmesAPI.Domain.Model;

namespace AcervoFilmesAPI.Domain.Interfaces
{
    public interface IFilme
    {
        Filme GetById(int id);
        void Add(Filme filme);
        void Update(Filme filme);
        void Delete(int id);
        void AddFilmeStreaming(FilmeStreaming filmeStreaming);
        void RemoveAllFilmeStreamings(int filmeId);
        public List<Filme> GetList();

        //metodo para listar o filme de algum ano especifico
        List<Filme> GetListByAnoLancamento(int anoLancamento);

        // Método para listar os filmes com base na média de nota
        List<Filme> GetListByMediaNota(int mediaNota);
        // Novo método para obter a média e a quantidade de filmes por ano
        MediaPeriodo GetMediaPeriodoByAno(int anoLancamento);
        void AtualizarMediaPeriodo(int anoLancamento, int qtdFilmes, int mediaPeriodo);
        void AtualizarMediaGenero(int generoId, int qtdFilmes, int mediaGenero);
    }
}
