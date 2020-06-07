using Howest_Movies_EE_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Howest_Movies_EE_DAL.DTO
{
    public class StudentDetailDTO
    {
        public int Nr { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public string Geslacht { get; set; }
        public string Email { get; set; }
        public IEnumerable<CursusDTO> Cursussen { get; set; }
        public int AantalCursussen { get; set; }

    }
}
