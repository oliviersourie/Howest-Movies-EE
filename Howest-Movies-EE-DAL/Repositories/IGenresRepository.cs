using Howest_Movies_EE_DAL.DTO.Genre;
using System.Linq;

namespace Howest_Movies_EE_DAL.Repositories
{
    public interface IGenresRepository
    {
        GenreDTO Create(GenreDTO genreDTO);
        GenreDTO Delete(int id);
        IQueryable<GenreDTO> GetAll();
        GenreDTO GetById(int id);
        GenreDTO Update(GenreDTO updateDTO);
    }
}