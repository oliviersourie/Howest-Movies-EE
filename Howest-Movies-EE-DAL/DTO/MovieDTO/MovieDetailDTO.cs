using System.Collections.Generic;
using Howest_Movies_EE_DAL.DTO.GenreDTO;
using Howest_Movies_EE_DAL.DTO.PersonDTO;

namespace Howest_Movies_EE_DAL.DTO.MovieDTO
{
    public class MovieDetailDTO
    {
        public long Id { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string CoverUrl { get; set; }
        public int Year { get; set; }
        public string OriginalAirDate { get; set; }
        public string Kind { get; set; }
        public decimal Rating { get; set; }
        public string Plot { get; set; }
        public string Country { get; set; }
        public int Top250Rank { get; set; }

        public IEnumerable<FullGenreDTO> GenreMovie { get; set; }
        public IEnumerable<FullPersonDTO> Actors { get; set; }


    }
}
