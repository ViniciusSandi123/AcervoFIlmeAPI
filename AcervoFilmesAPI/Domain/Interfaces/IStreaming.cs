using AcervoFilmesAPI.Domain.Model;

namespace AcervoFilmesAPI.Domain.Interfaces
{
    public interface IStreaming
    {
        //metodo para cadastrar um genero
        void Add(Streaming streaming);

        //metodo para listar generos
        List<Streaming> GetList();

        //metodo para buscar um streaming pelo id
        Streaming GetById(int id);

        //metodo para fazer uma alteração no streaming
        void Update(Streaming streaming);

        //metodo para deletar um streaming
        void Delete(int id);
    }
}

