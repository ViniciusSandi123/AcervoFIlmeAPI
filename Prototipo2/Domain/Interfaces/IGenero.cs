using Prototipo2.Domain.Model;

namespace Prototipo2.Domain.Interfaces
{
    public interface IGenero
    {
        // Método para cadastrar um gênero
        void Add(Genero genero);

        // Método para listar todos os gêneros
        List<Genero> GetList(int pageNumber, int pageQuantity);

        // Método para buscar gênero específico pelo id
        Genero GetById(int id);

        // Método para fazer a alteração em um gênero
        void Update(Genero genero);

        // Método para deletar um gênero
        void Delete(int id);
    }
}
