using System;
using System.Collections.Generic;
using System.Text;

namespace Howest_Movies_EE_DAL.DTO.PersonDTO
{
    public class UpdatePersonDTO
    {

        public long Id { get; set; }
        public string ImdbId { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
    }
}
