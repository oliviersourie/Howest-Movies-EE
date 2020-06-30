﻿using System;
using System.Collections.Generic;
using System.Text;
using Howest_Movies_EE_DAL.DTO.Movie;

namespace Howest_Movies_EE_DAL.DTO.Genre
{
    public class GenreDTO
    {
        public int Id { get; set; }
        public string ImdbName { get; set; }
        public string Name { get; set; }
        public IEnumerable<MovieDTO> Movies { get; set; }
    }
}