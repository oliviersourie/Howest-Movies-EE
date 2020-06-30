﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mowest_Movies_EE_Web.ViewModels
{
    public class MovieDetailViewModel
    {
        public long Id { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string CoverUrl { get; set; }
        public int Year { get; set; }
        public string OriginalAirDate { get; set; }
        public string Kind { get; set; }
        public decimal Rating { get; set; }
        public string Plot { get; set; }
        public string Country { get; set; }
        public int Top250Rank { get; set; }

        public IEnumerable<GenreViewModel> GenreMovie { get; set; }
        public IEnumerable<PersonViewModel> Actors { get; set; }
    }
}
