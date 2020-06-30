using Howest_Movies_EE_DAL.DTO.Movie;
using System.Collections.Generic;
using System.Linq;

namespace Howest_Movies_EE_DAL.Repositories
{
    public interface IMoviesRepository
    {
        MovieDTO Create(MovieDTO movieDTO);
        MovieDTO Delete(int id);
        IQueryable<MovieDTO> GetAll();
        IEnumerable<MovieDetailDTO> GetAllMoviesDetailed();
        MovieDTO GetById(int id);
        MovieDetailDTO GetMovieDetailsById(long id);
        MovieDTO SoftDelete(int id);
        MovieDTO Update(MovieDTO updateDTO);
    }
}