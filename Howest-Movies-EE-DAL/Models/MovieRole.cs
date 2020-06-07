using System;
using System.Collections.Generic;

namespace Howest_Movies_EE_DAL.Models
{
    public partial class MovieRole
    {
        public long MovieId { get; set; }
        public long PersonId { get; set; }
        public string Role { get; set; }

        public virtual Movies Movie { get; set; }
        public virtual Persons Person { get; set; }
    }
}
