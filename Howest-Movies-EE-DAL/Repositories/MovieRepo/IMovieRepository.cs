using Howest_Movies_EE_DAL.DTO.MovieDTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Howest_Movies_EE_DAL.Repositories
{
    public interface IMovieRepository
    {
        IQueryable<ListItemMovieDTO> All(bool desc = false, string cat = "TITLE");
        FullMovieDTO GetRandom();
        MovieDetailDTO GetMovie(int id);
        SmallMovieDTO Delete(int id);
        SmallMovieDTO SoftDelete(int id);
        FullMovieDTO Update(FullMovieDTO movieDTO);
        FullMovieDTO Create(CreateMovieDTO movieDTO);
        IQueryable<ListItemMovieDTO> Search(String searchString);

        IEnumerable<dynamic> MoviesToDynamic();
    }
}
