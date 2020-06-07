using System;
using System.Collections.Generic;

namespace Howest_Movies_EE_DAL.Models
{
    public partial class Movies
    {
        public Movies()
        {
            GenreMovie = new HashSet<GenreMovie>();
            MovieRole = new HashSet<MovieRole>();
        }

        public long Id { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string CoverUrl { get; set; }
        public int Year { get; set; }
        public string OriginalAirDate { get; set; }
        public string Kind { get; set; }
        public decimal Rating { get; set; }
        public string Plot { get; set; }
        public int Top250Rank { get; set; }

        public virtual ICollection<GenreMovie> GenreMovie { get; set; }
        public virtual ICollection<MovieRole> MovieRole { get; set; }
    }
}
