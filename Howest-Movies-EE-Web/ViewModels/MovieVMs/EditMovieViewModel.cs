using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mowest_Movies_EE_Web.ViewModels;

namespace Howest_Movies_EE_Web.ViewModels
{
    public class EditMovieViewModel
    {
        public MovieDetailViewModel Movie { get; set; }
        public IEnumerable<GenreViewModel> Genres { get; set; }
        public IEnumerable<PersonViewModel> People { get; set; }
    }
}
