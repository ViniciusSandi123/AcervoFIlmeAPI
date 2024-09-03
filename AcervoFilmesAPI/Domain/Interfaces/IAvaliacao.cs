using System.Collections.Generic;
using AcervoFilmesAPI.Domain.Model;

namespace AcervoFilmesAPI.Domain.Interfaces
{
    public interface IAvaliacao
    {

        //metodo para adicionar uma avaliacao
        void Add(Avaliacao avaliacao);

        //metodo para listar as avaliações
        List<Avaliacao> GetList();

        //metodo buscar uma avaliação em especifico pelo id
        Avaliacao GetById(int id);

        //metodo para fazer alteração em uma avaliação
        void Update(Avaliacao avaliacao);

        //metodo para excluir uma avaliação
        void Delete(int id);
        int CalcularMediaNotas(int filmeId);
    }
}
