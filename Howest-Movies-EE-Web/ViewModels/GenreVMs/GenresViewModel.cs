using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mowest_Movies_EE_Web.ViewModels;

namespace Howest_Movies_EE_Web.ViewModels
{
    public class GenresViewModel
    {
        public IEnumerable<GenreViewModel> AllGenres { get; set; }
    }
}
