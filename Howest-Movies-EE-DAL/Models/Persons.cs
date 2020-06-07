using System;
using System.Collections.Generic;

namespace Howest_Movies_EE_DAL.Models
{
    public partial class Persons
    {
        public Persons()
        {
            MovieRole = new HashSet<MovieRole>();
        }

        public long Id { get; set; }
        public string ImdbId { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }

        public virtual ICollection<MovieRole> MovieRole { get; set; }
    }
}
