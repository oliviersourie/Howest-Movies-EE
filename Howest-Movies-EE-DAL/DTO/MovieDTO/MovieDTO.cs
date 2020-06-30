﻿using Howest_Movies_EE_DAL.Extensions;

namespace Howest_Movies_EE_DAL.DTO.Movie
{
    public class MovieDTO
    {
        public long Id { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string CoverUrl { get; set; }
        public int Year { get; set; }
        public string Kind { get; set; }
        public decimal Rating { get; set; }
        public string OriginalAirDate { get; set; }
        public string Plot { get; set; }
        public int Top250Rank { get; set; }
        public string Country
        {
            get
            {
                return OriginalAirDate.GetCountryFromOriginalAirDate();
            }
        }
    }
}