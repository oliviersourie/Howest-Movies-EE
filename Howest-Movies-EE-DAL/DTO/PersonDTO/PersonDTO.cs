using Howest_Movies_EE_DAL.DTO.Movie;
using System.Collections.Generic;

namespace Howest_Movies_EE_DAL.DTO.Person
{
    public class PersonDTO
    {
        public long Id { get; set; }
        public string ImdbId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Biography { get; set; }
        public IEnumerable<MovieDTO> Movies { get; set; }
    }
}
