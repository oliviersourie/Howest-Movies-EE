using System;
using System.Collections.Generic;

namespace Howest_Movies_EE_DAL.Models
{
    public partial class Genres
    {
        public Genres()
        {
            GenreMovie = new HashSet<GenreMovie>();
        }

        public int Id { get; set; }
        public string ImdbName { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GenreMovie> GenreMovie { get; set; }
    }
}
