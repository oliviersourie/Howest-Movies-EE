using System;
using System.Collections.Generic;

namespace Howest_Movies_EE_DAL.Models
{
    public partial class GenreMovie
    {
        public long MovieId { get; set; }
        public int GenreId { get; set; }

        public virtual Genres Genre { get; set; }
        public virtual Movies Movie { get; set; }
    }
}
