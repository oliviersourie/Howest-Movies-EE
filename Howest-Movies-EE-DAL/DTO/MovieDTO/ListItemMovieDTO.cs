using System;
using System.Collections.Generic;
using System.Text;
using Howest_Movies_EE_DAL.Extensions;

namespace Howest_Movies_EE_DAL.DTO.MovieDTO
{
    public class ListItemMovieDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string CoverUrl { get; set; }
        public int Year { get; set; }
        public decimal Rating { get; set; }
        public string OriginalAirDate { get; set; }
        public string Country
        {
            get
            {
                return OriginalAirDate.GetCountryFromOriginalAirDate();
            }
        }
    }
}
