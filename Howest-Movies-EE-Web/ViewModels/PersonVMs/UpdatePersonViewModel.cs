using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Howest_Movies_EE_Web.ViewModels
{
    public class UpdatePersonViewModel
    {
        [HiddenInput]
        public long Id { get; set; }
        [Required]
        public string ImdbId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Biography { get; set; }
        public IEnumerable<string> Movies { get; set; }
        public IEnumerable<MovieIdViewModel> MoviePeople { get; set; }

        public List<SelectListItem> RoleTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "actor", Text = "Actor"},
            new SelectListItem { Value = "director", Text = "Director"},
            new SelectListItem { Value = "crew", Text = "Crew" },
            new SelectListItem { Value = "other", Text = "Other"  },
        };

        public UpdatePersonViewModel()
        {
            RoleTypes.Where(v => v.Value.Equals(Role)).FirstOrDefault(v => v.Selected);
        }
    }
}
