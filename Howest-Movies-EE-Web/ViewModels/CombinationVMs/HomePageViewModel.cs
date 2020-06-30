using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mowest_Movies_EE_Web.ViewModels
{
    public class HomePageViewModel
    {
        public List<MovieViewModel> AllMovies { get; set; }
        public MovieViewModel RandomMovie { get; set; }
        public string SortItem { get; set; }
        public List<MovieViewModel> FavMovies { get; set; }
        public string SortDirection { get; set; }
        public List<SelectListItem> SortProperties { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "TITLE", Text = "Movie title" },
            new SelectListItem { Value = "RATING", Text = "IMDB rating" },
             new SelectListItem { Value = "YEAR", Text = "Release year" },
             new SelectListItem { Value = "IMDBID", Text = "ImDB"},
        };
        
    }
}
