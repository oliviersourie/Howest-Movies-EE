using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Howest_Movies_EE_DAL.DTO
{
    public class StudentDTO
    {
        public int Studnr { get; set; }
        public string Voornaam { get; set; }
        [StringLength(30)]
        public string Familienaam { get; set; }
        public DateTime? Geboortedatum { get; set; }
        [StringLength(1)]
        public string Geslacht { get; set; }
        public int? Betaald { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        public int? Boete { get; set; }
    }
}
