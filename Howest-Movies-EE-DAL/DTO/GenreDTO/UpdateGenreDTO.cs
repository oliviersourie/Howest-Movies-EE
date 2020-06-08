﻿using System;
using System.Collections.Generic;
using System.Text;
using Howest_Movies_EE_DAL.DTO.MovieDTO;

namespace Howest_Movies_EE_DAL.DTO.GenreDTO
{
    public class UpdateGenreDTO
    {
        public int Id { get; set; }
        public string ImdbName { get; set; }
        public string Name { get; set; }
        public IEnumerable<MovieIdDTO> Movies { get; set; }
    }
}
