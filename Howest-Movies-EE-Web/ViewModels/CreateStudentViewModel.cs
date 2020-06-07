using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Howest_Movies_EE_Web.ViewModels
{
    public class CreateStudentViewModel
    {
        [HiddenInput]
        public int Nr { get; set; }
        [Required]
        public string Voornaam { get; set; }
        [Required]
        public string Familienaam { get; set; }
        [Required]
        public string Geslacht { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string[] Geslachten { 
            get => new[] { "M", "V" };
        } 
    }
}
