﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mowest_Movies_EE_Web.ViewModels;

namespace Howest_Movies_EE_Web.ViewModels
{
    public class UpdateGenreViewModel
    {
        [HiddenInput]
        public long Id { get; set; }
        [Required]
        public string ImdbName { get; set; }
        [Required]
        public string Name { get; set; }
        public IEnumerable<string> Movies { get; set; }
        public IEnumerable<MovieIdViewModel> MovieGenres { get; set; }

    }
}
