using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Howest_Movies_EE_DAL.DTO;
using Howest_Movies_EE_DAL.DTO.GenreDTO;

namespace Howest_Movies_EE_DAL.Repositories
{
    public interface IGenreRepository
    {
        IQueryable<FullGenreDTO> All();
        FullGenreDTO GetGenreById(int id);
        FullGenreDTO Create(CreateGenreDTO genreDTO);
        FullGenreDTO Update(UpdateGenreDTO genreDTO);
        SmallGenreDTO Delete(int id);
    }
}
