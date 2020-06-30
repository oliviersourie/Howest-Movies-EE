using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Howest_Movies_EE_Web.ViewModels
{
    public class CreatePersonViewModel
    {
        [Required]
        public string ImdbId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Biography { get; set; }
        public IEnumerable<string> Movies { get; set; }

        public List<SelectListItem> RoleTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "actor", Text = "Actor" },
            new SelectListItem { Value = "director", Text = "Director" },
            new SelectListItem { Value = "crew", Text = "Crew" },
            new SelectListItem { Value = "other", Text = "Other"  },
        };
    }
}
