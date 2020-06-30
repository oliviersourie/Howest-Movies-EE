using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Howest_Movies_EE_Web.ViewModels
{
    public class UpdateMovieViewModel
    {
        [HiddenInput]
        public long Id { get; set; }
        [HiddenInput]
        public string ImdbId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string CoverUrl { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string OriginalAirDate { get; set; }
        [Required]
        public string Kind { get; set; }
        [Required]
        [Range(typeof(decimal), "0", "99")]
        public decimal Rating { get; set; }
        [Required]
        public string Plot { get; set; }
        [Required]
        [Range(typeof(int), "1", "250")]
        public int Top250Rank { get; set; }
    }
}
