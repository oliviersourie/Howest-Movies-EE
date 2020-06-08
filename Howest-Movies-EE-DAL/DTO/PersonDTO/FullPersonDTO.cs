using System;
using System.Collections.Generic;
using System.Text;
using Howest_Movies_EE_DAL.DTO.MovieDTO;

namespace Howest_Movies_EE_DAL.DTO.PersonDTO
{
    public class FullPersonDTO
    {
        public long Id { get; set; }
        public string ImdbId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Biography { get; set; }
        public IEnumerable<MovieIdDTO> MoviePeople { get; set; }
    }
}
