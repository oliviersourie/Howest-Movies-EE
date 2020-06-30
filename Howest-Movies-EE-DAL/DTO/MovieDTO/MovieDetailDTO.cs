using System.Collections.Generic;
using Howest_Movies_EE_DAL.DTO.Genre;
using Howest_Movies_EE_DAL.DTO.Person;
using Howest_Movies_EE_DAL.DTO.Role;

namespace Howest_Movies_EE_DAL.DTO.Movie
{
    public class MovieDetailDTO: MovieDTO
    {
        public IEnumerable<PersonDTO> Persons { get; set; }
        public IEnumerable<GenreDTO> Genres { get; set; }
    }
}
